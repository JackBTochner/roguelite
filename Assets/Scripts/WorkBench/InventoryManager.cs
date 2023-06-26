using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items;
    public RectTransform itemContent;
    public GameObject inventoryItem;
    public RectTransform spritesList;
    public GameObject itemPrefab;
    public RectTransform workBenchPanel;
    public RectTransform inventoryPanel;

    private void Awake()
    {
        Instance = this;
        CreateList();
        ListItems();
    }
    
    public void CreateList()
    {
        Item item1 = ScriptableObject.CreateInstance<Item>();
        item1.id = 1;
        item1.amount = 2;

        Item item2 = ScriptableObject.CreateInstance<Item>();
        item2.id = 2;
        item2.amount = 2;

        Item item3 = ScriptableObject.CreateInstance<Item>();
        item3.id = 3;
        item3.amount = 2;

        // Add the items to the list
        items.Add(item1);
        items.Add(item2);
        items.Add(item3);
    }

    public void Add(Item item)
    {
        bool itemInInventory = false;
        foreach (var invItem in items)
        {
            if (invItem.id == item.id)
            {
                invItem.amount += 1;
                itemInInventory = true;
                break;
            }
        }
        if (!itemInInventory)
        {
            item.amount = 1;
            items.Add(item);
        }
        ListItems();
    }
    
    public void Remove(Item item)
    {
        item.amount -= 1; 
        if (item.amount == 0)
        {
            items.Remove(item);
        }
        
        ListItems();
    }

    private void ListItems()
    {   
        // Removes content
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        // Adds content
        foreach (var item in items)
        {
            var invItem = Instantiate(inventoryItem, itemContent);
            var itemIcon = invItem.transform.Find("ItemIcon").GetComponent<Image>();
            var itemAmount = invItem.transform.Find("ItemAmount").GetComponent<Text>();
            
            itemIcon.sprite = item.GetSprite();
            if (item.amount != 1)
            {
                itemAmount.text = item.amount.ToString();
            }

            // Create itemObject
            GameObject itemObj = Instantiate(itemPrefab, invItem.transform);
            var itemImage = itemObj.transform.Find("ItemImage").GetComponent<Image>();
            itemImage.sprite = item.GetSprite();
            itemObj.GetComponent<ItemController>().SetReferences(item, spritesList, workBenchPanel, inventoryPanel);
            itemObj.tag = "Item";
            
            itemObj.transform.position = invItem.transform.position;
            Debug.Log(itemAmount.text);
        }
    }
    
    

}
