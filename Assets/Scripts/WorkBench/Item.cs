using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public int amount;
    public ItemType itemType;

    public Item(int id, string itemName, int amount, ItemType itemType)
    {
        this.id = id;
        this.amount = amount;
        this.itemType = itemType;
    }

    public enum ItemType
    {
        Material,
        Component,
        Pen
    }
    
    public ItemType GetItemType(int id)
    {
        if (id >= 0 && id < 5)
        {
            return ItemType.Component;
        }
        if (id >= 6 && id < 10)
        {
            return ItemType.Material;
        }
        if (id >= 10 && id < 15)
        {
            return ItemType.Pen;
        }

        return ItemType.Component;
    }

    public Sprite GetSprite()
    {
        switch (id)
        {
            default:
            case 1: return ItemAssets.Instance.woodSprite;
            case 2: return ItemAssets.Instance.featherSprite;
            case 3: return ItemAssets.Instance.metalSprite;
        }
    }

}