using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Manager responsible for sending reaction event data and session summaries to the backend server.
/// </summary>
public class NetworkManager : MonoBehaviour
{
    private readonly string _reactionEventsEndpoint = "https://localhost:5001/api/reaction/events";
    private readonly string _reactionSummaryEndpoint = "https://localhost:5001/api/reaction/summary";

    public void SendReactionEvents(List<ReactionEvent> reactionEvents)
    {
        var json = JsonHelper.ToJson(reactionEvents.ToArray(), true);
        Debug.Log(json);
        StartCoroutine(PostData(json, _reactionEventsEndpoint));
    }

    public void SendSummary(DataSummary summary)
    {
        var json = JsonUtility.ToJson(summary);
        Debug.Log(json);
        StartCoroutine(PostData(json, _reactionSummaryEndpoint));
    }



    private static IEnumerator PostData(string data, string url)
    {
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error sending data to {url}: {www.error}");
            }
            else
            {
                Debug.Log($"Successfully sent data to {url}: {www.downloadHandler.text}");
            }
        }
    }

}
