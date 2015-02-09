using UnityEngine;
using System.Collections;

public class PositionTweenable : GettableComponent <PositionTweenable> {
	
	private static readonly string TWEEN_NAME = "PositionTweenable";

	public bool ignoreTimeScale = false;

	public void SetXPosition(float to)
	{
		Vector3 pos = transform.position;
		pos.x = to;
		transform.position = pos;
	}
	
	public void SetYPosition(float to)
	{
		Vector3 pos = transform.position;
		pos.y = to;
		transform.position = pos;
	}

	public void StopTween()
	{
		iTween.StopByName(gameObject, TWEEN_NAME);
	}

	public void TweenPositionFrom(Vector3 from, Vector3 to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		transform.position = from;
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}
	
	public void TweenPosition(Vector3 to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}
	
	public void TweenXYPositionFrom(Vector2 from, Vector2 to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		transform.position = new Vector3(from.x, from.y, transform.position.z);
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", new Vector3(to.x, to.y, transform.position.z),
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}
	
	public void TweenXYPosition(Vector2 to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", new Vector3(to.x, to.y, transform.position.z),
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}

	public void TweenXPositionFrom(float from, float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		transform.position = new Vector3(from, transform.position.y, transform.position.z);
		iTween.MoveTo(gameObject, iTween.Hash(
			"x", to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}
	
	public void TweenXPosition(float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		iTween.MoveTo(gameObject, iTween.Hash(
			"x", to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}

	public void TweenYPositionFrom(float from, float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		transform.position = new Vector3(transform.position.x, from, transform.position.z);
		iTween.MoveTo(gameObject, iTween.Hash(
			"y", to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}
	
	public void TweenYPosition(float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		StopTween();
		iTween.MoveTo(gameObject, iTween.Hash(
			"y", to,
			"time", time,
			"delay", delay,
			"ignoretimescale", ignoreTimeScale,
			"name", TWEEN_NAME,
			"easetype", easeType));
	}
}
