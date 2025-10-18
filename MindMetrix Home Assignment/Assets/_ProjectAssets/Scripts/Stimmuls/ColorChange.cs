using UnityEngine;

/// <summary>
/// Stimulus that changes the color of the object's material when executed and reverts it when stopped.
/// </summary>
public class ColorChange : StimulusBase
{
    [SerializeField] private Renderer ren;

    [SerializeField] private Color idleColor = Color.white;
    [SerializeField] private Color activeColor = Color.red;

    void Awake()
    {
        ren = GetComponent<Renderer>();
        ren.materials[0].color = idleColor;
    }

    public override void Execute()
    {
        ren.materials[0].color = activeColor;
    }

    public override void Stop()
    {
        ren.materials[0].color = idleColor;
    }
}
