using UnityEngine;

/// <summary>
/// Stimulus that makes the object blink by toggling its MeshRenderer on execution and stop.
/// </summary>
public class Blinking : StimulusBase
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void Execute()
    {
        meshRenderer.enabled = false;
    }

    public override void Stop()
    {
        meshRenderer.enabled = true;
    }
}
