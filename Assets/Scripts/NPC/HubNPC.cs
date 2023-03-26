using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(DialogueSystemTrigger))]
public class HubNPC : MonoBehaviour
{
    public int ActorID;
    private DialogueSystemTrigger _dialogueSystemTrigger;
    
    void Start()
    {
        _dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
    }
    public void OnConversationEnd()
    {
        Debug.Log(_dialogueSystemTrigger.conversation + "CONVERSATION ENDED");
        List<string> playedConversations = SerializerUtility.StringToList(DialogueLua.GetVariable("PlayedConversations").asString);
        playedConversations.Add(_dialogueSystemTrigger.conversation);
        DialogueLua.SetVariable("PlayedConversations", SerializerUtility.ListToString(playedConversations));
    }

    public void AssignConversation(string conversationTitle)
    {
        if (_dialogueSystemTrigger == null)
        { 
            _dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
        }
        _dialogueSystemTrigger.conversation = conversationTitle;
    }
}
