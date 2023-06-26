/*
 * ItemController.cs
 *
 * This script extends the functionality of the DragDrop script and controls the behavior of an item in the game.
 * It allows the item to be dragged and dropped, and performs specific actions based on the drag operation.
 * 
 * Attach this script to the GameObject representing an item in the game.
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : DragDrop
{
    // References to other game objects and components
    public Transform spriteList;            // The list of item sprites
    public RectTransform workBenchPanel;    // The workbench panel
    public RectTransform inventoryPanel;    // The inventory panel
    public Item item;                       // The item associated with this controller
    
    // Method to set references to the other game objects and components
    public void SetReferences(Item refItem, Transform refSpritesList, RectTransform reWorkBenchPanel, RectTransform refInventoryPanel)
    {
        spriteList = refSpritesList;
        item = refItem;
        workBenchPanel = reWorkBenchPanel;
        inventoryPanel = refInventoryPanel;
    }

    // Method to destroy the item GameObject
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Override of the OnBeginDrag method from the base class
    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Set the item as the last sibling in the hierarchy to ensure it appears on top
        transform.SetAsLastSibling();
        
        // If the item is not in the sprite list, move it to the sprite list and remove it from the inventory
        if (transform.parent != spriteList)
        {
            transform.SetParent(spriteList);
            InventoryManager.Instance.Remove(item);
        }
        // Call the base OnBeginDrag method to handle default behavior
        base.OnBeginDrag(eventData);
    }

    // Override of the OnEndDrag method from the base class
    public override void OnEndDrag(PointerEventData eventData)
    {
        // Call the base OnEndDrag method to handle default behavior
        base.OnEndDrag(eventData);
        
        // Move the item back to a valid position in the center of the workbench panel
        if (!RectTransformUtility.RectangleContainsScreenPoint(workBenchPanel, eventData.position))
        {
            // The sprite is outside the panel, so move it back to a valid position
            transform.position = workBenchPanel.rect.center;
        }
        
        // Check if the item is inside the inventory panel
        if (RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, eventData.position))
        {
            // Destroy the item GameObject and add the item back to the inventory
            DestroySelf();
            InventoryManager.Instance.Add(item);
        }
    }
}
