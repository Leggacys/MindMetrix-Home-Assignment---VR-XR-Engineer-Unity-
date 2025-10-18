using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Summary statistics of a session's reaction events.
/// </summary>
public class DataSummary
{
    public string sessionId;
    public string timestamp;
    public int stimuliCount;
    public int hits;
    public double hitAccuracy;
    public int averageReactionTime;

    /// <summary>
    /// Creates a DataSummary from a list of ReactionEvents.
    /// </summary>
    /// <param name="eventsList">List of reaction events.</param>
    /// <param name="sessionId">Unique identifier for the session.</param>
    /// <returns></returns>
    public static DataSummary FromEvents(List<ReactionEvent> eventsList, string sessionId)
    {
        int count = eventsList.Count;
        int hits = 0;
        int sumMs = 0;
        int nMs = 0;

        foreach (var e in eventsList)
        {
            if (e.hit) hits++;
            if (e.reactionTimeMs.HasValue)
            {
                sumMs += e.reactionTimeMs.Value;
                nMs++;
            }
        }

        return new DataSummary
        {
            sessionId = sessionId,
            timestamp = DateTimeOffset.UtcNow.ToString("o"),
            stimuliCount = count,
            hits = hits,
            hitAccuracy = count > 0 ? (double)hits / count : 0.0,
            averageReactionTime = nMs > 0 ? Mathf.RoundToInt((float)sumMs / nMs) : -1
        };
    }
}
