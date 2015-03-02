using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraDragger : MonoBehaviour {
	
	private static readonly float demiWidth = Screen.width / 2;
	private static readonly float demiHeight = Screen.height / 2;
	private static readonly float ratio = (float)Screen.width / (float)Screen.height;

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

	private bool _cameraSizerChecked = false;
	private CameraSizer _cameraSizer;
	private CameraSizer cameraSizer
	{
		get
		{
			if (_cameraSizer == null && !_cameraSizerChecked)
			{
				_cameraSizer = GetComponent<CameraSizer>();
				_cameraSizerChecked = true;
			}
			return _cameraSizer;
		}
	}
	
	void LateUpdate()
	{
		Vector3 v = camera.WorldToScreenPoint(target.position);
		float dx = v.x - demiWidth;
		float signX = (dx >= 0f) ? 1f : -1f;
		float dxRatio = Mathf.Abs (dx / demiWidth) - minDist;
		if(dxRatio > 0f)
		{
			float moveX = dxRatio / (1f - minDist) * maxSpeed;
			float newX = transform.position.x + signX * moveX;
			newX = Mathf.Clamp(newX, bounds.xMin + camera.orthographicSize * ratio, bounds.xMax - camera.orthographicSize * ratio);
			Vector3 pos = transform.position;
			pos.x = newX;
			transform.position = pos;
		}
		float dy = v.y - demiHeight;
		float signY = (dy >= 0f) ? 1f : -1f;
		float dyRatio = Mathf.Abs (dy / demiHeight) - minDist;
		if(dyRatio > 0f)
		{
			float moveY = dyRatio / (1f - minDist) * maxSpeed;
			float newY = transform.position.y + signY * moveY;
			newY = Mathf.Clamp(newY, bounds.yMin + camera.orthographicSize, bounds.yMax - camera.orthographicSize);
			Vector3 pos = transform.position;
			pos.y = newY;
			transform.position = pos;
		}
	}
	
	void OnDrawGizmos () 
	{
		Gizmos.color = new Color(1f, 1f, 1f, 1f);
		Gizmos.DrawLine(new Vector3(bounds.xMin, bounds.yMin, 0f), new Vector3(bounds.xMax, bounds.yMin, 0f));
		Gizmos.DrawLine(new Vector3(bounds.xMax, bounds.yMin, 0f), new Vector3(bounds.xMax, bounds.yMax, 0f));
		Gizmos.DrawLine(new Vector3(bounds.xMax, bounds.yMax, 0f), new Vector3(bounds.xMin, bounds.yMax, 0f));
		Gizmos.DrawLine(new Vector3(bounds.xMin, bounds.yMax, 0f), new Vector3(bounds.xMin, bounds.yMin, 0f));
	}

}
