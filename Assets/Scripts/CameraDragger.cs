using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraDragger : MonoBehaviour {

	public Transform target;

	public Rect bounds;
	//in ratio of the screen
	public float minDist;
	public float maxSpeed;

	private Camera _camera;
	private Camera camera
	{
		get
		{
			if (_camera == null)
			{
				_camera = GetComponent<Camera>();
			}
			return _camera;
		}
	}
	
	void Update()
	{
		Vector3 v = camera.WorldToScreenPoint(target.position);
		float demiWidth = Screen.width / 2;
		float dx = v.x - demiWidth;
		float signX = (dx >= 0f) ? 1f : -1f;
		float dxRatio = Mathf.Abs (dx / demiWidth) - minDist;
		if(dxRatio > 0f)
		{
			float moveX = dxRatio / (1f - minDist) * maxSpeed;
			float newX = transform.position.x + signX * moveX;
			newX = Mathf.Clamp(newX, bounds.xMin, bounds.xMax);
			Vector3 pos = transform.position;
			pos.x = newX;
			transform.position = pos;
		}
		float demiHeight = Screen.height / 2;
		float dy = v.y - demiHeight;
		float signY = (dy >= 0f) ? 1f : -1f;
		float dyRatio = Mathf.Abs (dy / demiHeight) - minDist;
		if(dyRatio > 0f)
		{
			float moveY = dyRatio / (1f - minDist) * maxSpeed;
			float newY = transform.position.y + signY * moveY;
			newY = Mathf.Clamp(newY, bounds.yMin, bounds.yMax);
			Vector3 pos = transform.position;
			pos.y = newY;
			transform.position = pos;
		}
	}

}
