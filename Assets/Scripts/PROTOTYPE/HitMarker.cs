using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour
{
    public float damage;

    public Text text;
    public Text text2;

    public void Initialise(float damage)
    {
        text.text = damage.ToString();
        text2.text = damage.ToString();
    }
}
