using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraSizer : MonoBehaviour {
	
	public bool targetPlayer = false;
	public Rigidbody target;
	public float sizeMin = 0.8f;
	public float sizeMax = 1.3f;
	public float speedMax = 1f;
	public float speedMin = 0.1f;
	public float speedCameraOut = 1f;
	public float speedCameraIn = 1f;

	private Camera camera;

	void Awake()
	{
		camera = GetComponent<Camera>();
		if (targetPlayer)
			target = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
	}

	void LateUpdate () 
	{
		float targetSize = Mathf.Max (sizeMin, Mathf.Min( sizeMax, (target.velocity.magnitude - speedMin) / (speedMax - speedMin) * (sizeMax - sizeMin) + sizeMin));
		bool zoomIn = targetSize < camera.orthographicSize; 
		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, Time.deltaTime * (zoomIn ? speedCameraIn : speedCameraOut));
	}
}
