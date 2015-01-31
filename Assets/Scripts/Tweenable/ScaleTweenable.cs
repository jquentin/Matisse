using UnityEngine;
using System.Collections;

public class ScaleTweenable : GettableComponent <ScaleTweenable> {

	public float scale { get; private set; }
	
	void Awake()
	{
		if (transform.localScale.x != transform.localScale.y)
			Debug.LogWarning("Non uniform scale on ScaleTweenable object");
		scale = transform.localScale.x;
	}
	
	public void TweenScaleFrom(float from, float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		transform.localScale = Vector3.one * from;
		iTween.ScaleTo(gameObject, iTween.Hash(
			"scale", Vector3.one * to,
			"time", time,
			"delay", delay,
			"easetype", easeType));
	}
	
	public void TweenScale(float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		iTween.ScaleTo(gameObject, iTween.Hash(
			"scale", Vector3.one * to,
			"time", time,
			"delay", delay,
			"easetype", easeType));
	}
}
