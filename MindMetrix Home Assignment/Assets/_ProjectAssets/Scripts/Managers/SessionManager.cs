using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages the overall session flow, coordinating stimulus presentation and data handling.
/// </summary>
public class SessionManager : MonoBehaviour
{
    [SerializeField] private StimulusManager stimulusManager;
    [SerializeField] private NetworkManager networkManager;

    void OnEnable()
    {
        UIManager.onStartButtonPressed += StartSession;
        StimulusManager.onSessionComplete += OnTrialsFinished;
    }

    void OnDisable()
    {
        UIManager.onStartButtonPressed -= StartSession;
        StimulusManager.onSessionComplete -= OnTrialsFinished;
    }


    public void StartSession()
    {
        stimulusManager.StartTrials();
    }

    public void OnTrialsFinished(List<ReactionEvent> events)
    {
        var summary = DataSummary.FromEvents(events, events.Count > 0 ? events[0].sessionId : "unknown");
        networkManager.SendReactionEvents(events);
        networkManager.SendSummary(summary);
    }
}
