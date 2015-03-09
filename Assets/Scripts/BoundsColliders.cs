using UnityEngine;
using System.Collections;

public class BoundsColliders : MonoBehaviour {
	
	public Bounds bounds;
	public float thickness = 1f;
	public GameObject boundsRenderer;

	void Start () 
	{
		float halfThickness = thickness * 0.5f;
		BoxCollider top = gameObject.AddComponent<BoxCollider>();
		top.center = new Vector3(bounds.center.x, bounds.max.y + halfThickness);
		top.size = new Vector3(bounds.size.x + thickness * 2f, thickness);
		BoxCollider bottom = gameObject.AddComponent<BoxCollider>();
		bottom.center = new Vector3(bounds.center.x, bounds.min.y - halfThickness);
		bottom.size = new Vector3(bounds.size.x + thickness * 2f, thickness);
		BoxCollider left = gameObject.AddComponent<BoxCollider>();
		left.center = new Vector3(bounds.min.x - halfThickness, bounds.center.y);
		left.size = new Vector3(thickness, bounds.size.y);
		BoxCollider right = gameObject.AddComponent<BoxCollider>();
		right.center = new Vector3(bounds.max.x + halfThickness, bounds.center.y);
		right.size = new Vector3(thickness, bounds.size.y);
		if (boundsRenderer != null)
		{
			GameObject topRenderer = Instantiate<GameObject>(boundsRenderer); 
			topRenderer.transform.position = top.center;
			topRenderer.transform.localScale = top.size;
			GameObject bottomRenderer = Instantiate<GameObject>(boundsRenderer); 
			bottomRenderer.transform.position = bottom.center;
			bottomRenderer.transform.localScale = bottom.size;
			GameObject leftRenderer = Instantiate<GameObject>(boundsRenderer); 
			leftRenderer.transform.position = left.center;
			leftRenderer.transform.localScale = left.size;
			GameObject rightRenderer = Instantiate<GameObject>(boundsRenderer); 
			rightRenderer.transform.position = right.center;
			rightRenderer.transform.localScale = right.size;
		}
	}
	
	void OnDrawGizmos () 
	{
		Gizmos.color = new Color(1f, 0f, 0f, 1f);
		Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.min.y, 0f), new Vector3(bounds.max.x, bounds.min.y, 0f));
		Gizmos.DrawLine(new Vector3(bounds.max.x, bounds.max.y, 0f), new Vector3(bounds.min.x, bounds.max.y, 0f));
		Gizmos.DrawLine(new Vector3(bounds.max.x, bounds.min.y, 0f), new Vector3(bounds.max.x, bounds.max.y, 0f));
		Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.max.y, 0f), new Vector3(bounds.min.x, bounds.min.y, 0f));
	}

}
