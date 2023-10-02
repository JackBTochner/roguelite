using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractCanvasManager : MonoBehaviour
{
    public GameObject interactPrompt;
    public TextMeshProUGUI interactPromptText;

    [SerializeField]
    private TransformAnchor _interactCanvasTransformAnchor;
    
    void Awake()
    {
        _interactCanvasTransformAnchor.Provide(this.transform);
    }

    public void EnableInteractPrompt(string text)
    {
        // show ui
        interactPrompt.SetActive(true);
        interactPromptText.text = text;
    }
    public void DisableInteractPrompt() 
    {
        // hide UI
        interactPrompt.SetActive(false);
    }
}
