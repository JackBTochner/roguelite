using UnityEngine;

public class CameraShake : MonoBehaviour {
    
	public float intensity;
	public float duration;

	Vector3 startPos;

	void Start()
    {
		startPos = transform.position;
    }

	void FixedUpdate () {

		if (duration >= 0)
		{
			transform.localPosition += Random.insideUnitSphere * intensity;
			
			duration -= Time.deltaTime;
		}
		transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 2);
	}
}