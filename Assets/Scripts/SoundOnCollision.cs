using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundOnCollision : MonoBehaviour {

	public List<AudioClip> sndCollision;
	private AudioSource source;
	public float volumeFactor = 1f;
	private bool hasPlayed = false;
	public float timer = 1f;

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	void OnCollisionEnter (Collision collision) 
	{
		if (!hasPlayed)
		{
			SoundOnCollision soc = collision.collider.GetComponent<SoundOnCollision>();
			if (soc != null)
				soc.SetHasPlayed();
			source.volume = collision.relativeVelocity.magnitude * volumeFactor;
			source.PlayOneShotControlled(sndCollision);
		}
	}

	public void SetHasPlayed()
	{
		hasPlayed = true;
		Invoke("UnPlay", timer);
	}

	void UnPlay()
	{
		hasPlayed = false;
	}

}
