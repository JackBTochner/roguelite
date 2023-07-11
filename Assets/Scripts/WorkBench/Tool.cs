using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Tool : DragDrop
{
    public string itemTag = "Item";
    private Vector3 _initialPosition;

    protected virtual void Start()
    {
        _initialPosition = transform.position;
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        CanvasGroup.alpha = .6f;
        if (GetItemsUnderneath().Count > 0)
        {
            CanvasGroup.alpha = 1f;
        }
    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        List<Collider2D> components = GetItemsUnderneath();
        if (CanCreateItem(components)) 
        {
            CreateItem(components);
        }
        transform.position = _initialPosition;
    }
    
    public List<Collider2D> GetItemsUnderneath()
    {
        // Create a new list to store colliders with the "Item" tag
        List<Collider2D> itemColliders = new List<Collider2D>();
        
        // Get all the colliders of the sprite's children with the specified tag
        Collider2D[] colliders = Physics2D.OverlapBoxAll(RectTransform.position, RectTransform.sizeDelta, 0f);

        // Count the number of colliders with the specified tag
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(itemTag))
            {
                itemColliders.Add(collider);
            }
        }
        return itemColliders;
    }
    
    public void CreateItem(List<Collider2D> itemColliders)
    {
        // Search for item to be created
        int itemID = 1;

        // Create the item
        ItemCreationManager.Instance.CreateWorkBenchItem(RectTransform.position, itemID);

        // Remove the components
        foreach (Collider2D item in itemColliders)
        {
            Destroy(item.GameObject());
        }
    }
    
    public bool CanCreateItem(List<Collider2D> itemColliders)
    {
        // Count the number of colliders with the specified tag
        foreach (Collider2D item in itemColliders)
        {
            // Check if the items can create an item
            if (item.)
        }

        if (itemColliders.Count > 0)
        {
            return true;
        }
        return false;
    }
    
    
}