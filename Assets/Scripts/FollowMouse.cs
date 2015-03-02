using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	public bool moving = false;

	public float approxDistance = 0.1f;
	public float speed = 1f;
	private Vector3 target;
	private bool keepTarget = false;

	void OnCollisionEnter(Collision collision)
	{
		StopShip();
	}
	void OnCollisionStay(Collision collision)
	{
		StopShip();
	}

	void OnDisabled()
	{
		StopShip();
	}

	void Update () 
	{
#if UNITY_EDITOR
		if(Input.GetMouseButton(0))
		{
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			HeadTo(worldPoint);
			target = worldPoint;
		}
		else if (keepTarget)
		{
			HeadTo(target);
			CheckIfTargetReached();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			keepTarget = true;
		}
		else
		{
			StopShip();
		}
#else

		if(Input.touchCount > 1)
			StopShip();
		else if(Input.touchCount == 1)
		{
			if(Input.touches[0].phase == TouchPhase.Ended)
			{
				keepTarget = true;
			}
			else
			{
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, -Camera.main.transform.position.z));
				HeadTo(worldPoint);
				target = worldPoint;
			}
		}
		else if (keepTarget)
		{
			HeadTo(target);
			CheckIfTargetReached();
		}
		else
		{
			StopShip();
		}
#endif
	}

	void CheckIfTargetReached()
	{
		if (HasReachedTarget())
		{
			StopShip();
		}
	}

	bool HasReachedTarget()
	{
		return (transform.position - target).magnitude < approxDistance;
	}

	void StopShip()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		keepTarget = false;
		SendMessage("Idle", SendMessageOptions.DontRequireReceiver);
		moving = false;
	}

	void HeadTo(Vector3 tgt)
	{
		float dir = Mathf.Sign(tgt.x - transform.position.x);
		if (dir != 0f)
		{
			Vector3 scale = transform.localScale;
			scale.x = dir * Mathf.Abs(scale.x);
			transform.localScale = scale;
		}
		GetComponent<Rigidbody>().velocity = (tgt - transform.position) * speed;
		SendMessage("Move", SendMessageOptions.DontRequireReceiver);
		moving = true;
	}

}
