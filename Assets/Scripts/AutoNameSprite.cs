using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class AutoNameSprite : MonoBehaviour {
	
	#if UNITY_EDITOR

	void Update()
	{
		if (Application.isEditor)
		{
			Sprite sprite = GetComponent<SpriteRenderer>().sprite;
			if (sprite != null)
				name = sprite.name;
		}
	}
	#endif

}
