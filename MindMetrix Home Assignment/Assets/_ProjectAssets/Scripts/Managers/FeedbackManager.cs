using UnityEngine;

public enum FeedbackType
{
    Good,
    TooSlow,
    Miss
}
/// <summary>
/// Manager for providing visual and auditory feedback based on user performance.
/// </summary>
public class FeedbackManager : MonoBehaviour
{

    [Header("Colors")]
    [SerializeField] private Color missColor = Color.red;
    [SerializeField] private Color toSlowColor = Color.yellow;
    [SerializeField] private Color goodColor = Color.green;

    [SerializeField] private ParticleSystem feedbackParticleSystem;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip goodAudioClip;
    [SerializeField] private AudioClip tooSlowAudioClip;
    [SerializeField] private AudioClip missAudioClip;


    void OnEnable()
    {
        StimulusManager.onTrialEnd += FeedBack;
    }

    void OnDisable()
    {
        StimulusManager.onTrialEnd -= FeedBack;
    }

    public void FeedBack(FeedbackType type)
    {
        switch (type)
        {
            case FeedbackType.Good:
                var feedbackParticleSystemMainModule = feedbackParticleSystem.main;
                feedbackParticleSystemMainModule.startColor = goodColor;
                feedbackParticleSystem.Play();
                audioSource.PlayOneShot(goodAudioClip);
                break;

            case FeedbackType.TooSlow:
                feedbackParticleSystemMainModule = feedbackParticleSystem.main;
                feedbackParticleSystemMainModule.startColor = toSlowColor;
                feedbackParticleSystem.Play();
                audioSource.PlayOneShot(tooSlowAudioClip);
                break;

            case FeedbackType.Miss:
                feedbackParticleSystemMainModule = feedbackParticleSystem.main;
                feedbackParticleSystemMainModule.startColor = missColor;
                feedbackParticleSystem.Play();
                audioSource.PlayOneShot(missAudioClip);
                break;
        }
    }
}
