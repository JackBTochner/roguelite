using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBenchManager : MonoBehaviour
{
    public GameObject workBenchUI;
    public GameObject recipeManager;
    public GameObject itemCreationManager;
    public GameObject workBenchManager;
    public GameObject inventoryManager;

    public void CloseWorkBenchUI()
    {
        workBenchUI.SetActive(false);
        recipeManager.SetActive(false);
        itemCreationManager.SetActive(false);
        workBenchManager.SetActive(false);
        inventoryManager.SetActive(false);
    }
    
}