using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class SlimeDamageManager : MonoBehaviour
{
    // Damage
    public static int slimeDamage = 1;
    // Damage once in 0.5 second
    public static float damagetime = 0.5f;
    // Time calculate 
    public static float timenow = 0;
    // We use static because variables and methods in this class are shared across all instances(All the Slime) of the SlimeDamage class.
    // Takes a parameter of type PlayerCharacter.
    public static void ApplyDamage(PlayerCharacter playerCharacter)
    {
        //Time.time is the time from start of the scene.
        //If the Time is bigger than the timenow it will run this function
        if(Time.time > timenow)
        {
            // takedamage to the player
            playerCharacter.TakeDamage(slimeDamage);
            // Set timenow = to from the start of the scene + damagetime.
            // Next if statement will check is timenow bigger than this? if not which mean the 0.5 second is still not gone.
            timenow = Time.time + damagetime;
        }
        
    }
}
