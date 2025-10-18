using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager for handling UI interactions, specifically closing the main UI and notifying when the start button is pressed.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static Action onStartButtonPressed;

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    void Awake()
    {
        mainCanvas = transform.GetComponent<Canvas>();
        graphicRaycaster = transform.GetComponent<GraphicRaycaster>();
    }

    public void CloseUI()
    {
        mainCanvas.enabled = false;
        graphicRaycaster.enabled = false;
        onStartButtonPressed?.Invoke();
    }
}
