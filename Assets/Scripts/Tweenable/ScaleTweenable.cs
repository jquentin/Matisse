using UnityEngine;
using System.Collections;

public class ScaleTweenable : GettableComponent <ScaleTweenable> {
	
	public bool ignoreTimeScale = false;
	public float defaultTime = 1f;
	public float defaultDelay = 0f;
	public iTween.EaseType defaultEaseType = iTween.EaseType.easeOutExpo;

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
			"ignoretimescale", ignoreTimeScale,
			"easetype", easeType));
	}
	
	public void TweenScale(float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		iTween.ScaleTo(gameObject, iTween.Hash(
			"scale", Vector3.one * to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"easetype", easeType));
	}

	public void TweenScale(float to)
	{
		iTween.ScaleTo(gameObject, iTween.Hash(
			"scale", Vector3.one * to,
			"time", defaultTime,
			"delay", defaultDelay,
			"ignoretimescale", ignoreTimeScale,
			"easetype", defaultEaseType));
	}
}
