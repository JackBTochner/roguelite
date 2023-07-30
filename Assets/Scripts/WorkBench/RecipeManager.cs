using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;
    private Dictionary<int, List<Recipe>> _recipes;
    
    private void Awake()
    {
        Instance = this;
        CreateRecipes();
    }

    private void CreateRecipes()
    {
        _recipes = new Dictionary<int, List<Recipe>>();
        TextAsset jsonFile = Resources.Load<TextAsset>("recipes");
        RecipeDataWrapper wrapper = JsonUtility.FromJson<RecipeDataWrapper>(jsonFile.text);
        List<Recipe> recipeData = wrapper.recipes;
        foreach (Recipe recipe in recipeData)
        {
            foreach (int id in recipe.ingredientIDs)
            {
                if (_recipes.ContainsKey(id))
                {
                    List<Recipe> recipes = _recipes[id];
                    recipes.Add(recipe);
                    _recipes[id] = recipes;
                }
                else
                {
                    _recipes.Add(id, new List<Recipe>(){recipe});
                }
            }
        }
    }

    public bool CheckCraftableItemID(List<int> ingredientsIDs)
    {
        // Retrieves the ingredient
        int ingredientID = ingredientsIDs[0];
        
        // Checks if the item is in the recipe
        if (!_recipes.ContainsKey(ingredientID))
        {
            return false;
        }
        
        // Checks if all the ingredients can create the item
        List<Recipe> recipes = _recipes[ingredientID];
        foreach (Recipe recipe in recipes)
        {
            bool status = recipe.ingredientIDs.SequenceEqual(ingredientsIDs);
            if (status)
            {
                return true;
            }
        }
        return false;
    }
    
    public int GetCraftableItemID(List<int> ingredientsIDs)
    {
        // Retrieves the ingredient
        int ingredientID = ingredientsIDs[0];
        // Checks if the item is in the recipe
        if (_recipes.ContainsKey(ingredientID))
        {
            List<Recipe> recipes = _recipes[ingredientID];
            foreach (Recipe recipe in recipes)
            {
                bool status = recipe.ingredientIDs.SequenceEqual(ingredientsIDs);
                if (status)
                {
                    // Return creatable item ID
                    return recipe.resultItemID;
                }
            }
        }
        // Error
        return -1;
    }
    
}

[System.Serializable]
public class RecipeDataWrapper
{
    public List<Recipe> recipes;
}