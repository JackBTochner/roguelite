using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreationManager : MonoBehaviour
{
    public static ItemCreationManager Instance;
    public RectTransform spritesList;
    public GameObject itemPrefab;
    public RectTransform workBenchPanel;
    public RectTransform inventoryPanel;
    
    private void Awake()
    {
        Instance = this;
    }

    public void CreateInventoryItem(Vector3 position, Transform parent, Item item)
    {
        GameObject itemObj = Instantiate(itemPrefab, position, Quaternion.identity, parent);
        var itemImage = itemObj.transform.Find("ItemImage").GetComponent<Image>();
        itemImage.sprite = item.GetSprite();
        itemObj.GetComponent<ItemController>().SetReferences(item, spritesList, workBenchPanel, inventoryPanel);
        itemObj.tag = "Item";
    }
    
    public void CreateWorkBenchItem(Vector3 position, int itemID)
    {
        GameObject itemObj = Instantiate(itemPrefab, position, Quaternion.identity, spritesList);
        var itemImage = itemObj.transform.Find("ItemImage").GetComponent<Image>();
        
        Item item = ScriptableObject.CreateInstance<Item>();
        item.id = itemID;
        
        itemImage.sprite = item.GetSprite();
        itemObj.GetComponent<ItemController>().SetReferences(item, spritesList, workBenchPanel, inventoryPanel);
        itemObj.tag = "Item";
    }
}
