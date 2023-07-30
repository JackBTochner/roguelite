using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite woodSprite;
    public Sprite plasticSprite;
    public Sprite metalSprite;
    public Sprite featherSprite;
    public Sprite woodBodySprite;
    public Sprite plasticBodySprite;
    public Sprite metalBodySprite;
    public Sprite feltTipSprite;
    
    
}
