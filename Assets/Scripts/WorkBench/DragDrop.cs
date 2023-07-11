/*
 * DragDrop.cs
 *
 * This script enables dragging and dropping of UI elements.
 * It implements the necessary interfaces from the UnityEngine.EventSystems namespace.
 * 
 * Attach this script to the UI element you want to make draggable.
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    
    // References to the GameObject's own RectTransform and CanvasGroup components
    protected RectTransform RectTransform;
    protected CanvasGroup CanvasGroup;
 
    protected virtual void Awake()
    {
        // Get references to the required components
        RectTransform = GetComponent<RectTransform>();
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // Reduce the alpha (transparency) of the element and disable raycasts
        CanvasGroup.alpha = .6f;
        CanvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // Update the anchored position of the RectTransform based on the drag delta
        RectTransform.anchoredPosition += eventData.delta;
    }
    
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // Restore the alpha and enable raycasts
        CanvasGroup.alpha = 1f;
        CanvasGroup.blocksRaycasts = true;
    }

}