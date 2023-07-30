using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public int resultItemID;
    public List<int> ingredientIDs;

    // Constructor for Recipe class
    public Recipe(int resultID, List<int> ingredients)
    {
        resultItemID = resultID;
        ingredientIDs = ingredients;
    }
}
