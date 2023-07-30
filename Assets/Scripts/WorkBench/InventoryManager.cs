using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public List<Item> items;
    public RectTransform itemContent;
    public GameObject inventoryItem;

    public static InventoryManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        // Ensure only one instance of the InventoryManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateList();
        ListItems();
        Debug.Log("created");
    }

    public void CreateList()
    {
        Item item1 = ScriptableObject.CreateInstance<Item>();
        item1.id = 1;
        item1.amount = 1;
        item1.amount = 1;

        Item item2 = ScriptableObject.CreateInstance<Item>();
        item2.id = 2;
        item2.amount = 2;

        Item item3 = ScriptableObject.CreateInstance<Item>();
        item3.id = 3;
        item3.amount = 2;
        
        Item item4 = ScriptableObject.CreateInstance<Item>();
        item4.id = 4;
        item4.amount = 2;
        
        Item item5 = ScriptableObject.CreateInstance<Item>();
        item5.id = 5;
        item5.amount = 2;
        
        Item item6 = ScriptableObject.CreateInstance<Item>();
        item6.id = 6;
        item6.amount = 2;
        
        Item item7 = ScriptableObject.CreateInstance<Item>();
        item7.id = 7;
        item7.amount = 2;
        
        Item item8 = ScriptableObject.CreateInstance<Item>();
        item8.id = 8;
        item8.amount = 2;
        
        // Add the items to the list
        items.Add(item1);
        items.Add(item2);
        items.Add(item3);
        items.Add(item4);
        items.Add(item5);
        items.Add(item6);
        items.Add(item7);
        items.Add(item8);
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
            
            // Create the Item Image as an interactable object
            ItemCreationManager.Instance.CreateInventoryItem(invItem.transform.position, invItem.transform, item);
        }
    }
    
    

}
