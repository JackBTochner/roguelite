using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(DialogueSystemEvents))]
public class DialogueManagerAnchorProvider : MonoBehaviour
{
    [SerializeField]
    private DialogueSystemEvents _dialogueSystemEvents;
    [SerializeField]
    private DialogueSystemEventsAnchor _dialogueSystemEventsAnchor = default;
    
    void OnEnable()
    {
        _dialogueSystemEvents = GetComponent<DialogueSystemEvents>();
        _dialogueSystemEventsAnchor.Provide(_dialogueSystemEvents);
    }
}
