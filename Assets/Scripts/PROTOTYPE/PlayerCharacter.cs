using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float critChance;
    public float critMultiplier;
    
    public GameObject playerGFX;
    public GameObject playerDigGFX;

    public void PlayerToggleDig(bool isDigging)
    {
        Debug.Log("YEAHAYAH");
        if (isDigging)
        {
            playerGFX.SetActive(true);
            playerDigGFX.SetActive(false);
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        }
        else
        {
            playerGFX.SetActive(false);
            playerDigGFX.SetActive(true);
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        }
    }
}
