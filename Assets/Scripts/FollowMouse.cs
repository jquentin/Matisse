using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
	
	public float approxDistance = 0.1f;
	public float speed = 1f;
	private Vector3 target;
	private bool keepTarget = false;
	public bool handleScaleOrientation = false;

	void OnCollisionEnter(Collision collision)
	{
		keepTarget = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	void Update () 
	{
		if (StartOnTap.instance.transitioning)
			return;
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBPLAYER
		if(Input.GetMouseButton(0))
		{
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			target = worldPoint;
			float dir = 0f;
			if(worldPoint.x > transform.position.x)
				dir = 1f;
			else if(worldPoint.x < transform.position.x)
				dir = -1f;
			if (handleScaleOrientation && dir != 0f)
			{
				Vector3 scale = transform.localScale;
				scale.x = dir;
				transform.localScale = scale;
			}
			GetComponent<Rigidbody>().velocity = (worldPoint - transform.position) * speed;
//			iTween.MoveUpdate(gameObject, iTween.Hash(
//				"position", worldPoint,
//				"speed", speed));
			SendMessage("Move", SendMessageOptions.DontRequireReceiver);
		}
		else if (keepTarget)
		{
			float dir = 0f;
			if(target.x > transform.position.x)
				dir = 1f;
			else if(target.x < transform.position.x)
				dir = -1f;
			if (handleScaleOrientation && dir != 0f)
			{
				Vector3 scale = transform.localScale;
				scale.x = dir;
				transform.localScale = scale;
			}
			GetComponent<Rigidbody>().velocity = (target - transform.position) * speed;
//			iTween.MoveUpdate(gameObject, iTween.Hash(
//				"position", target,
//				"speed", speed));
			SendMessage("Move", SendMessageOptions.DontRequireReceiver);
			
			if ((transform.position - target).magnitude < approxDistance)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				keepTarget = false;
				SendMessage("Idle", SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			keepTarget = true;
		}
		else
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
#else

		if(Input.touchCount > 0)
		{
			if(Input.touches[0].phase == TouchPhase.Ended)
			{
				keepTarget = true;
			}
			else
			{
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, -Camera.main.transform.position.z));
				target = worldPoint;
				float dir = 0f;
				if(worldPoint.x > transform.position.x)
					dir = 1f;
				else if(worldPoint.x < transform.position.x)
					dir = -1f;
				if (handleScaleOrientation && dir != 0f)
				{
					Vector3 scale = transform.localScale;
					scale.x = dir;
					transform.localScale = scale;
				}
				iTween.MoveUpdate(gameObject, iTween.Hash(
					"position", worldPoint,
					"speed", speed));
				SendMessage("Move", SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (keepTarget)
		{
			float dir = 0f;
			if(target.x > transform.position.x)
				dir = 1f;
			else if(target.x < transform.position.x)
				dir = -1f;
			if (handleScaleOrientation && dir != 0f)
			{
				Vector3 scale = transform.localScale;
				scale.x = dir;
				transform.localScale = scale;
			}
			iTween.MoveUpdate(gameObject, iTween.Hash(
				"position", target,
				"speed", speed));
			SendMessage("Move", SendMessageOptions.DontRequireReceiver);
			
			if (transform.position == target)
			{
				keepTarget = false;
				SendMessage("Idle", SendMessageOptions.DontRequireReceiver);
			}
		}
#endif
	}
}
