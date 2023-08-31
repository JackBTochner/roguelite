using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class LeapingWisp : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default;
    public float jumpHeight = 20.0f;
    public float airTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Jump());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            Vector3 direction = playerTransformAnchor.Value.position - transform.position;
            direction.y = 0; // This line ensures that the enemy only rotates around the Y-axis
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    IEnumerator Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.useGravity = false;

        float upTime = 0.5f; // time to reach the top
        float downTime = 0.5f; // time to reach the ground
        float groundTime = 2.0f; // time to stick on the ground
        float startY = transform.position.y + 3;
        float topY = startY + jumpHeight;
        float pushForce = 50.0f;
        float pushRadius = 5.0f;

        while (true)
        {
            // Jump up
            float timer = 0;
            while (timer < upTime)
            {
                float t = timer / upTime;
                float newY = Mathf.Lerp(startY, topY, t);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                timer += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, topY, transform.position.z);

            // Stay in the air
            yield return new WaitForSeconds(airTime);

            // Drop down
            timer = 0;
            while (timer < downTime)
            {
                float t = timer / downTime;
                float newY = Mathf.Lerp(topY, startY, t);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                timer += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);

            if (rb != null) rb.useGravity = true;

            // Stick on the ground
            yield return new WaitForSeconds(groundTime);

            if (rb != null) rb.useGravity = false;
        }
    }


}
