using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;


public enum StimulusType
{
    Color,
    Move,
    Blink,
}

/// <summary>
/// Manages the stimulus order executions and user responses during trials.
/// </summary>

public class StimulusManager : MonoBehaviour
{
    public static Action<List<ReactionEvent>> onSessionComplete;
    public static Action<FeedbackType> onTrialEnd;

    [Header("Config")]
    [SerializeField] private KeyCode responseKey = KeyCode.Space;
    [SerializeField] private float minDelayBeforeStimulus = 0.8f;
    [SerializeField] private float maxDelayBeforeStimulus = 2.0f;
    [SerializeField] private float responseTimeout = 2.5f;
    [SerializeField] private List<GameObject> stimulusObject;
    [SerializeField] private int trialCount = 10;

    private StimulusBase stimulus;

    private List<ReactionEvent> _eventsList = new List<ReactionEvent>();

    /// <summary>
    /// Starts the trial session.
    /// </summary>
    public void StartTrials()
    {
        StartCoroutine(SessionRoutine(trialCount));
    }

    /// <summary>
    /// Runs the session routine for a specified number of trials.
    /// </summary>
    /// <param name="trials">The number of trials to run.</param>
    /// <returns></returns>

    private IEnumerator SessionRoutine(int trials)
    {
        string sessionId = GenerateSessionId();

        yield return new WaitForSeconds(1);

        for (int i = 0; i < trials; i++)
        {
            yield return StartCoroutine(RunTrial(sessionId, i));
        }

        onSessionComplete?.Invoke(_eventsList);
    }


    /// <summary>
    /// Runs a single reaction trial:
    /// 1. Waits for a random delay (jitter),
    /// 2. Executes a random stimulus,
    /// 3. Monitors user input (key or click),
    /// 4. Calculates reaction time and triggers feedback,
    /// 5. Records the trial event data.
    /// </summary>
    /// <param name="sessionId">Unique identifier of the active session.</param>
    /// <param name="trialIndex">Zero-based index of the current trial within the session.</param>
    /// <returns>Coroutine that manages the full trial lifecycle.</returns>
    private IEnumerator RunTrial(string sessionId, int trialIndex)
    {
        float jitter = UnityEngine.Random.Range(minDelayBeforeStimulus, maxDelayBeforeStimulus);
        yield return new WaitForSeconds(jitter);

        stimulus = stimulusObject[UnityEngine.Random.Range(0, stimulusObject.Count)].GetComponent<StimulusBase>();
        stimulus.Execute();

        double tOn = Time.realtimeSinceStartupAsDouble;

        bool responded = false;
        double tResponse = 0.0;
        float elapsed = 0f;
        string responseType = ""; 
        
        while (elapsed < responseTimeout && !responded)
        {
            if (Input.GetKeyDown(responseKey))
            {
                responded = true;
                tResponse = Time.realtimeSinceStartupAsDouble;
                responseType = "key";
            }

            if (Input.GetMouseButtonDown(0))
            {
                responded = true;
                tResponse = Time.realtimeSinceStartupAsDouble;
                responseType = "click";
            }

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        int responseTime = responded ? (int)Math.Round((tResponse - tOn) * 1000.0) : -1;
        if (responseTime == -1 || responseTime > 700)
        {
            onTrialEnd?.Invoke(FeedbackType.Miss);
        }
        else if (responseTime < 400)
        {
            onTrialEnd?.Invoke(FeedbackType.Good);
        }
        else
        {
            onTrialEnd?.Invoke(FeedbackType.TooSlow);
        }

        stimulus.Stop();

        var ev = new ReactionEvent
        {
            sessionId = sessionId,
            trialIndex = trialIndex,
            stimulusType = stimulus.GetType().Name,
            stimulusOnUtc = DateTimeOffset.UtcNow,
            stimulusOnSinceStartup = tOn,
            responded = responded,
            responseType = responseType,
            responseSinceStartup = responded ? tResponse : (double?)null,
            reactionTimeMs = responseTime,
            hit = responded,
        };

        _eventsList.Add(ev);

        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// Generates a unique session ID.
    /// </summary>
    /// <returns></returns>
    private string GenerateSessionId()
    {
        return $"mmx-{DateTimeOffset.UtcNow:yyyyMMdd-HHmmss}-{UnityEngine.Random.Range(1000, 9999)}";
    }


}
