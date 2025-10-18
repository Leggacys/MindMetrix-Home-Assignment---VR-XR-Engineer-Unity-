using System;

/// <summary>
/// Data structure representing a single reaction event during a session.
/// </summary>
[Serializable]
public class ReactionEvent
{
    public string sessionId;
    public int trialIndex;
    public string stimulusType;
    public DateTimeOffset stimulusOnUtc;
    public double stimulusOnSinceStartup;
    public bool responded;
    public string responseType;
    public double? responseSinceStartup;
    public int? reactionTimeMs;
    public bool hit;
}
