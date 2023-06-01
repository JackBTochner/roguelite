using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProjectileMarker : MonoBehaviour
{
    public List<Image> markerIcons = new List<Image>();
    public Sprite defaultSprite = default;

    void Start()
    {
        ClearIcons();
    }

    public void AddIconAt(Sprite sprite, int index)
    {
        if(index >= markerIcons.Count)
            return;
        markerIcons[index].sprite = (sprite) ? sprite : defaultSprite;
        markerIcons[index].gameObject.SetActive(true);
    }
    public void RemoveIcon(int index)
    {
        if(index >= markerIcons.Count)
            return;
        markerIcons[index].sprite = defaultSprite;
        markerIcons[index].gameObject.SetActive(false);
    }
    
    public void ClearIcons()
    {
        for (int i = 0; i < markerIcons.Count; i++)
        {
            RemoveIcon(i);
        }
    }
}
