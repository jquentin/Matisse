using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraDragger : MonoBehaviour {
	
	private static readonly float demiWidth = Screen.width / 2;
	private static readonly float demiHeight = Screen.height / 2;
	private static readonly float ratio = (float)Screen.width / (float)Screen.height;

	public bool targetPlayer = false;
	public Transform target;
	
	public bool xBounded = true;
	public bool yBounded = true;
	public Bounds bounds;

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
	
	private Vector2 _lastDelta = Vector2.zero;
	public Vector3 lastDelta
	{
		get
		{
			return _lastDelta;
		}
	}

	void Awake()
	{
		if (targetPlayer)
			target = GameObject.FindWithTag("Player").transform;
	}
	
	void Update()
	{
		_lastDelta = Vector2.zero;
		Vector3 v = camera.WorldToScreenPoint(target.position);
		
		float demiWidth = Screen.width / 2;
		float dx = v.x - demiWidth;
		float signX = (dx >= 0f) ? 1f : -1f;
		float dxRatio = Mathf.Abs (dx / demiWidth) - minDist;
		if(dxRatio > 0f)
		{
			float moveX = dxRatio / (1f - minDist) * maxSpeed;
			float newX = transform.position.x + signX * moveX;
			if (xBounded)
				newX = Mathf.Clamp(newX, bounds.min.x + camera.orthographicSize * ratio, bounds.max.x - camera.orthographicSize * ratio);
			Vector3 pos = transform.position;
			_lastDelta = new Vector2(newX - pos.x, _lastDelta.y);
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
			if (yBounded)
				newY = Mathf.Clamp(newY, bounds.min.y + camera.orthographicSize, bounds.max.y - camera.orthographicSize);
			Vector3 pos = transform.position;
			_lastDelta = new Vector2(_lastDelta.x, newY - pos.y);
			pos.y = newY;
			transform.position = pos;
		}
	}
	
	
	void OnDrawGizmos () 
	{
		Gizmos.color = new Color(1f, 1f, 1f, 1f);
		if (yBounded)
		{
			Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.min.y, 0f), new Vector3(bounds.max.x, bounds.min.y, 0f));
			Gizmos.DrawLine(new Vector3(bounds.max.x, bounds.max.y, 0f), new Vector3(bounds.min.x, bounds.max.y, 0f));
		}
		if (xBounded)
		{
			Gizmos.DrawLine(new Vector3(bounds.max.x, bounds.min.y, 0f), new Vector3(bounds.max.x, bounds.max.y, 0f));
			Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.max.y, 0f), new Vector3(bounds.min.x, bounds.min.y, 0f));
		}
	}
	
}
