using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HubNPCManager : MonoBehaviour
{
    [SerializeField]
    private RunManager runManager = default;

    [SerializeField]
    private DialogueDatabase dialogueDatabase = default;

    public HubNPC[] Actors;

    // TODO: Fix list difference between Conversation.title and Conversation.
    // Done originally because Conversation.title needed for field checking, whereas Conversation class needed for Conversation.id.
    private List<Conversation> allConversations =>
        dialogueDatabase.conversations;

    private List<Conversation> validConversations = new List<Conversation>();

    private List<string> discardedConversations = new List<string>();

    private List<string> playedConversations = new List<string>();

    private List<Conversation>
        superPriorityConversations = new List<Conversation>();

    Dictionary<string, int> NPCInteractionCounts =>
        SerializerUtility
            .StringToDictionary(DialogueLua
                .GetVariable("NPCInteractionCounts")
                .asString);

    [SerializeField]
    private RunManagerAnchor _runManagerAnchor;

    private void Start()
    {
        // DEBUG_PLAYGROUND_DELETE_LATER();
        if (_runManagerAnchor.isSet)
        {
            runManager = _runManagerAnchor.Value;
        }

        ActivateNPCs();
    }

    private void DEBUG_PLAYGROUND_DELETE_LATER()
    {
        Debug.Log(DialogueManager.masterDatabase.GetConversation(12).fields);
        Debug.Log(DialogueLua.GetConversationField(13, "Title"));

        string description =
            DialogueLua.GetConversationField(12, "Description").asString;
        List<string> requiredConversationTitles =
            SerializerUtility
                .StringToList(DialogueLua
                    .GetConversationField(12, "RequiredConversations")
                    .asString);
        Debug.Log(requiredConversationTitles[0]);

        // Debug.Log(requiredConversationIDs[1]);
        Debug.Log(DialogueLua.GetConversationField(12, "Conversant").asString);
        Debug.Log(DialogueLua.GetConversationField(12, "Title").asString);
        Debug.Log(SerializerUtility.ListToString(requiredConversationTitles));
        Debug.Log (description);
    }

    public void ActivateNPCs()
    {
        DeactivateNPCs();
        SortConversations();
        Dictionary<int, string> conversationToActorIDs =
            new Dictionary<int, string>();

        if (validConversations.Any())
        {
            foreach (var conv in validConversations)
            {
                int actorID =
                    DialogueLua
                        .GetConversationField(conv.id, "Conversant")
                        .asInt;
                if (!conversationToActorIDs.ContainsKey(actorID))
                {
                    conversationToActorIDs.Add(actorID, conv.Title);
                }
            }
            Debug.Log(conversationToActorIDs.Count);
            foreach (var kvp in conversationToActorIDs) {
                Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
            }
        }
        foreach (var actor in Actors)
        {
            if (conversationToActorIDs.ContainsKey(actor.ActorID))
            {
                actor.gameObject.SetActive(true);
                actor.AssignConversation(conversationToActorIDs[actor.ActorID]);
            }
        }
    }

    public void DeactivateNPCs()
    {
        foreach (var actor in Actors)
        {
            actor.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Runs only on returning from a run or starting a new game.
    /// Should not have to run if reloading a save -- there NPC locations + conversations should have been serialised beforehand and just loaded in.
    /// </summary>
    private void SortConversations()
    {
        // Empty lists of valid conversations

        superPriorityConversations.Clear();
        validConversations.Clear();

        // Get discarded conversations

        discardedConversations =
            SerializerUtility
                .StringToList(DialogueLua
                    .GetVariable("DiscardedConversations")
                    .asString);

        // Get Played Conversations
        playedConversations =
            SerializerUtility
                .StringToList(DialogueLua
                    .GetVariable("PlayedConversations")
                    .asString);

        // Populate validConversations and superPriorityConversations lists
        foreach (var conversation in allConversations)
        {
            if (ConversationIsValid(conversation.id))
            {
                /*
                if(conversation.LookupBool("SuperPriority"))
                    superPriorityConversations.Add(conversation);
                else
                */
                validConversations.Add (conversation);
            }
        }

        // If SuperPriority conversations is populated, sort that by priority, return
        // Sort validConversations list by priority
        if (validConversations.Count > 1)
        {
            validConversations =
                validConversations
                    .OrderBy(c => c.LookupInt("Priority"))
                    .ToList();
        }
    }

    //TODO: Add saving & loading of discarded conversations and playedconversations.
    // TODO: remove excess Debug.Log, move each conditional into its own try/catch, clean entire function.
    private bool ConversationIsValid(int id)
    {
        bool isValid = true;

        ConversationFields fields = new ConversationFields(id);
        string conversant =
            DialogueLua.GetConversationField(id, "Conversant").asString;

        // Check that conversation has not been discarded
        if (
            discardedConversations
                .Contains(DialogueLua
                    .GetConversationField(id, "Title")
                    .asString)
        ) isValid = false;

        //        Debug.Log("NotDiscarded");
        // Check that PlayOnce & HasBeenPlayed
        if (fields.PlayOnce && fields.HasBeenPlayed) isValid = false;

        //        Debug.Log("Passed PlayOnce+HasBeenPlayed Check");
        // Check RequiredMinNPCInteractions
        if (NPCInteractionCounts.ContainsKey(conversant))
        {
            if (
                NPCInteractionCounts[conversant] <
                fields.RequiredMinNPCInteractions
            ) isValid = false;
        }

        //        Debug.Log("Passed RequiredMinNPCInteractions Check");
        // Check RequiresRunCleared
        if (
            !DialogueLua.GetVariable("RunCleared").asBool &&
            fields.RequiresRunCleared
        ) isValid = false;

        //        Debug.Log("Passed RequiresRunCleared Check");
        // Check RequiresRunNotCleared
        if (
            DialogueLua.GetVariable("RunCleared").asBool &&
            fields.RequiresRunNotCleared
        ) isValid = false;

        //        Debug.Log("Passed RequiresRunNotCleared Check");
        // Check RequiresThisRunCleared
        if (
            !DialogueLua.GetVariable("LastRunCleared").asBool &&
            fields.RequiresLastRunCleared
        ) isValid = false;

        //        Debug.Log("Passed RequiresThisRunCleared Check");
        // Check RequiresThisRunNotCleared
        if (
            DialogueLua.GetVariable("LastRunCleared").asBool &&
            fields.RequiresLastRunCleared
        ) isValid = false;

        //        Debug.Log("Passed LastRunCleared Check");
        // Check RequiredConversations
        // A lot of these below require there to not be duplicate entries in database lists.
        if (fields.RequiredConversations.Any())
        {
            List<string> intersect =
                fields
                    .RequiredConversations
                    .Intersect(playedConversations)
                    .ToList();
            Debug
                .Log("RequiredConversations n PlayedConversations :" +
                intersect);
            if (!fields.RequiredConversations.SequenceEqual(intersect))
            {
                Debug
                    .Log(!fields
                        .RequiredConversations
                        .SequenceEqual(intersect));
                isValid = false;
            }
        }

        //        Debug.Log("Passed RequiredConversations Check");
        // Check RequiredFalseConversations
        if (fields.RequiredFalseConversations.Any())
        {
            List<string> intersectConversations =
                fields
                    .RequiredFalseConversations
                    .Intersect(playedConversations)
                    .ToList();
            Debug
                .Log("RequiredFalseConversations n PlayedConversations :" +
                intersectConversations);
            if (intersectConversations.Any())
            {
                isValid = false;
            }
        }

        //        Debug.Log("Passed RequiredFalseConversations Check");
        // Check RequiredEncounters
        if (fields.RequiredEncounters.Any())
        {
            List<string> intersect =
                fields
                    .RequiredEncounters
                    .Intersect(runManager.VisitedEncounters)
                    .ToList();
            Debug.Log("RequiredEncounters n VisitedEncounters :" + intersect);
            if (!fields.RequiredEncounters.SequenceEqual(intersect))
            {
                Debug.Log(!fields.RequiredEncounters.SequenceEqual(intersect));
                isValid = false;
            }
        }

        //        Debug.Log("Passed RequiredEncounters Check");
        // Check RequiredEncountersThisRun
        // TODO: ADD A CHECK TO MAKE SURE WE ARE NOT IN THE HUB SCENE???
        // TODO: ACTUALLY MAYBE WE ONLY RUN THIS ON SPECIFIC FIELD BASED SPAWNER
        if (fields.RequiredEncountersThisRun.Any())
        {
            if (runManager.currentRun == null) isValid = false;

            List<string> intersect =
                fields
                    .RequiredEncountersThisRun
                    .Intersect(runManager.currentRun.VisitedEncounters)
                    .ToList();
            if (!fields.RequiredEncountersThisRun.SequenceEqual(intersect))
            {
                Debug
                    .Log("RequiredEncountersThisRun n currentRun.VisitedEncounters :" +
                    intersect);
                isValid = false;
            }
        }

        //        Debug.Log("Passed RequiredEncountersThisRun Check");
        // Check RequiredEncountersLastRun
        if (fields.RequiredEncountersLastRun.Any())
        {
            if (runManager.lastRun == null) isValid = false;

            List<string> intersect =
                fields
                    .RequiredEncountersLastRun
                    .Intersect(runManager.lastRun.VisitedEncounters)
                    .ToList();
            if (!fields.RequiredEncountersLastRun.SequenceEqual(intersect))
            {
                Debug
                    .Log("RequiredEncountersLastRun n lastRun.VisitedEncounters :" +
                    intersect);
                isValid = false;
            }
        }

        //        Debug.Log("Passed RequiredEncountersLastRun Check");
        // Check RequiredAnyEncounters
        if (fields.RequiredEncounters.Any())
        {
            List<string> intersect =
                fields
                    .RequiredEncounters
                    .Intersect(runManager.VisitedEncounters)
                    .ToList();
            Debug
                .Log("RequiredAnyEncounters n VisitedEncounters :" + intersect);
            if (intersect?.Any() != true)
            {
                isValid = false;
            }
        }

        //        Debug.Log("Passed RequiredAnyEncounters Check");
        // Check RequiredAnyEncountersThisRun
        if (fields.RequiredAnyEncountersThisRun.Any())
        {
            if (runManager.currentRun == null) isValid = false;

            List<string> intersect =
                fields
                    .RequiredAnyEncountersThisRun
                    .Intersect(runManager.currentRun.VisitedEncounters)
                    .ToList();
            Debug
                .Log("RequiredAnyEncountersThisRun n currentRun.VisitedEncounters :" +
                intersect);
            if (intersect?.Any() != true) isValid = false;
        }

        //        Debug.Log("Passed RequiredAnyEncountersThisRun Check");
        // Check RequiredAnyEncountersLastRun
        if (fields.RequiredAnyEncountersLastRun.Any())
        {
            if (runManager.lastRun == null) isValid = false;

            List<string> intersect =
                fields
                    .RequiredAnyEncountersLastRun
                    .Intersect(runManager.lastRun.VisitedEncounters)
                    .ToList();
            Debug
                .Log("RequiredAnyEncountersLastRun n currentRun.VisitedEncounters :" +
                intersect);
            if (intersect?.Any() != true) isValid = false;
        }

        //        Debug.Log("Passed RequiredAnyEncountersLastRun Check");
        return isValid;
    }
}

public struct ConversationFields
{
    private int id;

    public ConversationFields(int id)
    {
        this.id = id;
    }

    public bool SuperPriority =>
        DialogueLua.GetConversationField(id, "SuperPriority").asBool;

    public int Priority =>
        DialogueLua.GetConversationField(id, "Priority").asInt;

    public bool PlayOnce =>
        DialogueLua.GetConversationField(id, "PlayOnce").asBool;

    public int RequiredMinNPCInteractions =>
        DialogueLua
            .GetConversationField(id, "RequiredMinNPCInteractions")
            .asInt;

    public bool RequiresRunCleared =>
        DialogueLua.GetConversationField(id, "RequiresRunCleared").asBool;

    public bool RequiresRunNotCleared =>
        DialogueLua.GetConversationField(id, "RequiresRunNotCleared").asBool;

    public bool RequiresLastRunCleared =>
        DialogueLua.GetConversationField(id, "RequiresLastRunCleared").asBool;

    public bool RequiresLastRunNotCleared =>
        DialogueLua
            .GetConversationField(id, "RequiresLastRunNotCleared")
            .asBool;

    public List<string> RequiredConversations =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredConversations")
                .asString);

    public List<string> RequiredFalseConversations =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredFalseConversations")
                .asString);

    public List<string> RequiredEncounters =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredEncounters")
                .asString);

    public List<string> RequiredEncountersLastRun =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredEncountersLastRun")
                .asString);

    public List<string> RequiredEncountersThisRun =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredEncountersThisRun")
                .asString);

    public List<string> RequiredAnyEncounters =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredAnyEncounters")
                .asString);

    public List<string> RequiredAnyEncountersLastRun =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredAnyEncountersLastRun")
                .asString);

    public List<string> RequiredAnyEncountersThisRun =>
        SerializerUtility
            .StringToList(DialogueLua
                .GetConversationField(id, "RequiredAnyEncountersThisRun")
                .asString);

    public bool HasBeenPlayed =>
        DialogueLua.GetConversationField(id, "HasBeenPlayed").asBool;
}
