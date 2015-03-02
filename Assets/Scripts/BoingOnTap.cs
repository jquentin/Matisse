using UnityEngine;
using System.Collections;

public class BoingOnTap : MonoBehaviour {

	public AudioClip boingSnd;
	private bool isPlaying = false;

	void Awake()
	{
		if (GetComponent<Collider>() == null && GetComponent<Collider2D>() == null)
			Debug.LogWarning("BoingOnTap without a collider");
	}
	
	IEnumerator OnMouseDown ()
	{
		if (!isPlaying)
		{
			isPlaying = true;
			GetComponent<AudioSource>().PlayOneShotControlled(boingSnd);
			iTween.PunchScale(gameObject, iTween.Hash(
				"amount", transform.localScale * 1.3f,
				"time", 1f));
			yield return new WaitForSeconds(1.1f);
			isPlaying = false;
		}
	}

}
