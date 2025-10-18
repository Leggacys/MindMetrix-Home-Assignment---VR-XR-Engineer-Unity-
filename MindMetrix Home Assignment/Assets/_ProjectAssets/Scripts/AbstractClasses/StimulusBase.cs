using UnityEngine;

/// <summary>
/// Abstract base class for all stimulus types used in the reaction task.
/// Provides a common interface for executing and stopping stimuli,
/// ensuring consistent behavior across different implementations (e.g., visual, auditory).
/// </summary>
public abstract class StimulusBase : MonoBehaviour
{

    public StimulusType stimulusType;

    public abstract void Execute();

    public abstract void Stop();
}
