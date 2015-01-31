using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EventBasedCollider : MonoBehaviour {

	public delegate void TriggerEvent(Collider other);

	public TriggerEvent onTriggerEnter;
	public TriggerEvent onTriggerExit;
	
	public delegate void CollisionEvent(Collision collision);

	public CollisionEvent onCollisionEnter;
	public CollisionEvent onCollisionExit;
	
	private int nbTriggering = 0;
	private int nbColliding = 0;

	public bool isTriggering
	{
		get
		{
			return (nbTriggering > 0);
		}
	}

	public bool isColliding
	{
		get
		{
			return (nbColliding > 0);
        }
    }

	void OnTriggerEnter(Collider other)
	{
		nbTriggering++;
		if (onTriggerEnter != null)
			onTriggerEnter(other);
	}

	void OnTriggerExit(Collider other)
	{
		nbTriggering--;
		if (onTriggerExit != null)
			onTriggerExit(other);
	}

	void OnCollisionEnter(Collision collision)
	{
		nbColliding++;
		if (onCollisionEnter != null)
			onCollisionEnter(collision);
	}

	void OnCollisionExit(Collision collision)
	{
		nbColliding--;
		if (onCollisionExit != null)
			onCollisionExit(collision);
	}

}
