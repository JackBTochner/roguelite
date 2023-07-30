using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Drill : Tool
{
    public override void CreateItem(List<Collider2D> itemColliders)
    {
        // Creating a list for storing the ingredient IDs
        var ingredientIDs = new List<int>();
        
        // Adding the IDs to the list
        foreach (Collider2D item in itemColliders)
        {
            ingredientIDs.Add(item.GameObject().GetComponent<ItemController>().item.id);
        }
        
        // Check if item can be crafted
        if (RecipeManager.Instance.CheckCraftableItemID(ingredientIDs))
        {
            int craftableItemID = RecipeManager.Instance.GetCraftableItemID(ingredientIDs);
            // Create the item
            ItemCreationManager.Instance.CreateWorkBenchItem(RectTransform.position, craftableItemID);
            
            // Remove the components
            foreach (Collider2D item in itemColliders)
            {
                Destroy(item.GameObject());
            }
        }
        else
        {
            // Display Error or Do Nothing
        }
    }
}