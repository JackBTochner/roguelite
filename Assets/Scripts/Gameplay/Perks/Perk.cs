using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perk : ScriptableObject
{
    public string perkName = "PerkName";
    [TextArea(15,20)]
    public string perkDescription = "PerkDescription";
    public UpgradeTier upgradeTier;

    // [HideInInspector] public AbilityManager abilityManager;

    public GameObject perkButtonPrefab;
    public GameObject perkButtonObject;
    [HideInInspector] public Button perkButton;
    [HideInInspector] public Text perkButtonText;

    public Sprite icon;
    public GameObject model;

    public Perk nextTier;

    // public PerkIconSlot iconSlot;
}

public enum UpgradeTier {
    Tier1,
    Tier2,
    Tier3
}