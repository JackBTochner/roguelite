using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour
{
    public float damage;

    public Text text;
    public Text text2;
    public Transform rotHandler;

    public Rigidbody rb;

    public void OnObjectSpawn()
    {
        text.text = damage.ToString();
        text2.text = damage.ToString();


        //make the hit marker go in a random direction
        //this was a lot easier in 2D

        //int rot = Random.Range(-90, 90);
        //rotHandler.eulerAngles = new Vector3(0, 0, rot);
        //int force = Random.Range(400, 650);
        //rb.AddForce(rotHandler.up * force);
    }
}
