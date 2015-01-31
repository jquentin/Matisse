using UnityEngine;
using System.Collections;

public class OrientedToDirection : MonoBehaviour {

	public float speed = 8f;
	public float minMove = 0.01f;
	private Vector3 _lastPos;
	private Quaternion lookRot = Quaternion.LookRotation(Vector3.right);
	
	Transform mTrans;
	
	void Awake ()
	{
		mTrans = transform;
	}

	void OnEnable()
	{
		_lastPos = mTrans.position;
	}
	
	void FixedUpdate ()
	{
		Vector3 dir = mTrans.position - _lastPos;
		float mag = dir.magnitude;

		if (mag > minMove)
		{
			lookRot = Quaternion.LookRotation(dir);
		}
		else
		{
//			print ("immobile " + mag + " " + _lastPos + " " + mTrans.position);
			//lookRot = Quaternion.LookRotation(Vector3.right);
		}
		mTrans.rotation = Quaternion.Slerp(mTrans.rotation, lookRot, Mathf.Clamp01(speed * Time.deltaTime));
		_lastPos = mTrans.position;
	}
}
