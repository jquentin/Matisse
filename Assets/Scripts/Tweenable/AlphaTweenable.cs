using UnityEngine;
using System.Collections;

public class AlphaTweenable : GettableComponent<AlphaTweenable> {
	
	public bool ignoreTimeScale = false;

	private SpriteRenderer[] spriteRenderers;
	private MeshRenderer[] meshRenderers;
	private Collider[] _colliders = null;
	private Collider[] colliders
	{
		get
		{
			if (_colliders == null)
				_colliders = GetComponentsInChildren<Collider>();
			return _colliders;
		}
	}
	public float alpha { get; private set; }
	public bool disableColliders = false;

	void Awake()
	{
		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		meshRenderers = GetComponentsInChildren<MeshRenderer>();
		if (spriteRenderers.Length > 0)
			alpha = spriteRenderers[0].color.a;
		else if (meshRenderers.Length > 0)
			alpha = meshRenderers[0].material.color.a;
	}

	public void UpdateAlpha(float value)
	{
		foreach(SpriteRenderer r in spriteRenderers)
			r.color = new Color(r.color.r, r.color.g, r.color.b, value);
		foreach(MeshRenderer r2 in meshRenderers)
			r2.material.color = new Color(r2.material.color.r, r2.material.color.g, r2.material.color.b, value);
		if (disableColliders)
		{
			foreach(Collider c in GetComponentsInChildren<Collider>())
				c.enabled = (value != 0f);
		}
		alpha = value;
	}

	public void TweenAlphaFrom(float from, float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		iTween.ValueTo(gameObject, iTween.Hash(
			"from", from,
			"to", to,
			"time", time,
			"delay", delay,
			"onupdate", "UpdateAlpha",
			"ignoretimescale", ignoreTimeScale,
			"easetype", easeType));
	}
	
	public void TweenAlpha(float to, float time, float delay = 0f, iTween.EaseType easeType = iTween.EaseType.easeOutExpo)
	{
		iTween.ValueTo(gameObject, iTween.Hash(
			"from", alpha,
			"to", to,
			"time", time,
			"delay", delay,
			"onupdate", "UpdateAlpha",
			"ignoretimescale", ignoreTimeScale,
			"easetype", easeType));
	}
}
