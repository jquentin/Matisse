using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelList : MonoBehaviour {

	private LevelList _instance;
	public LevelList instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<LevelList>();
			return _instance;
		}
	}

	private List<GameObject> _buttons;
	public List<GameObject> buttons
	{
		get
		{
			if (_buttons == null)
				_buttons = transform.FindChildrenGameObjects("", false);
			return _buttons;
		}
	}

	public void Validate (GameObject button) 
	{
		foreach ( GameObject b in buttons)
			if (b != button)
				b.GetComponent<ScaleTweenable>().TweenScale(0f, 1f);
		button.GetComponent<ScaleTweenable>().Punch(0.3f, 0.7f);
	}

}
