using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenWorkBench : MonoBehaviour
{
    public GameObject workBenchUI;
    public GameObject recipeManager;
    public GameObject itemCreationManager;
    public GameObject workBenchManager;
    public GameObject inventoryManager;
    

    void OnTriggerEnter(Collider other)
    {
        // If the player interacts with the WorkBench
        if (other.CompareTag("Player"))
        {
            // Displays WorkBench UI
            workBenchUI.SetActive(true);
            recipeManager.SetActive(true);
            itemCreationManager.SetActive(true);
            workBenchManager.SetActive(true);
            inventoryManager.SetActive(true);
        }
    }
}

