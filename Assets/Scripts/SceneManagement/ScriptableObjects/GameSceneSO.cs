using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// A ScriptableObject template which contains necessary data that is common in all game scenes (Locations, Managers, Menus, (music?)), and acts as an addressable identifier.
/// </summary>
[CreateAssetMenu(fileName = "NewGameplayScene", menuName = "Scene Data/Gameplay")]
public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReference sceneReference;

    /// <summary>
    /// Used by the SceneSelector tool to discern what type of scene it needs to load.
    /// </summary>
    public enum GameSceneType
    { 
		//Playable scenes
		Location, //SceneSelector tool will also load PersistentManagers and Gameplay
		Menu, //SceneSelector tool will also load Gameplay

		//Special scenes
		Initialisation,
		PersistentManagers,
		Gameplay,

		//Work in progress scenes that don't need to be played
		Editor,
    }
}
