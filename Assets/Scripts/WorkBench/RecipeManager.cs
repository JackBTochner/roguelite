using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    // Creates a Dictionary of the recipes for the creation of items.
    private Dictionary<int, List<int>> recipes = new Dictionary<int, List<int>>();
    
    public void AddRecipe(int itemID, List<int> ingredientsID)
    {
        // Adds the item and the ingredients required to create it.
        recipes.Add(itemID, ingredientsID);
    }
    
    public bool CanCraftItem(int itemID, List<int> ingredientsID)
    {
        // Checks if the item is in the recipe
        if (recipes.ContainsKey(itemID))
        {
            List<int> requiredIngredientsID = recipes[itemID];
            foreach (int ingredientID in requiredIngredientsID)
            {
                if (!ingredientsID.Contains(ingredientID))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}