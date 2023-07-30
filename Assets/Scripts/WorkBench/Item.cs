using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public int amount;
    public ItemType itemType;

    public enum ItemType
    {
        Material,
        Component,
        Pen
    }
    
    public ItemType GetItemType()
    {
        if (id >= 0 && id < 5)
        {
            return ItemType.Component;
        }
        if (id >= 5 && id < 10)
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
            case 2: return ItemAssets.Instance.plasticSprite;
            case 3: return ItemAssets.Instance.metalSprite;
            case 4: return ItemAssets.Instance.featherSprite;
            case 5: return ItemAssets.Instance.woodBodySprite;
            case 6: return ItemAssets.Instance.plasticBodySprite;
            case 7: return ItemAssets.Instance.metalBodySprite;
            case 8: return ItemAssets.Instance.feltTipSprite;
        }
    }

}