using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(DialogueSystemTrigger))]
public class HubNPC : MonoBehaviour
{
    public int ActorID;
    public DialogueSystemTrigger dialogueSystemTrigger;
    public string interactPrompt = "Talk";
    public Interactor currentInteractor;

    public bool isInteracting = false;
    public bool canInteract = true;

    public GameObject interactExclamationMark;

    [Header("Listening on")]
    [SerializeField] private PlayerMovementAnchor _playerMovementAnchor = default;
    
    void Start()
    {
        dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
    }

    public void AssignConversation(string conversationTitle)
    {
        if (dialogueSystemTrigger == null)
        { 
            dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
        }
        dialogueSystemTrigger.conversation = conversationTitle;
        canInteract = true;
        interactExclamationMark.SetActive(true);
    }

    public void StartConversation(Interactor interactor)
    {
        dialogueSystemTrigger.OnUse();
        isInteracting = true;
        interactExclamationMark.SetActive(false);
        if (_playerMovementAnchor.Value != null)
        {
            _playerMovementAnchor.Value.allowMovement = false;
            _playerMovementAnchor.Value.allowRotation = false;
        }
    }

    public void OnConversationEnd()
    {
        if (_playerMovementAnchor.Value != null)
        {
            _playerMovementAnchor.Value.allowMovement = true;
            _playerMovementAnchor.Value.allowRotation = true;
        }
        List<string> playedConversations = SerializerUtility.StringToList(DialogueLua.GetVariable("PlayedConversations").asString);
        playedConversations.Add(dialogueSystemTrigger.conversation);
        DialogueLua.SetVariable("PlayedConversations", SerializerUtility.ListToString(playedConversations));
        isInteracting = false;
        if (currentInteractor != null)
        {
            currentInteractor.ClearCurrentTarget();
        }
        canInteract = false;

    }
}
