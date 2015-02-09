﻿using UnityEngine;
using System.Collections;

public class StartOnTap : MonoBehaviour {

	private static StartOnTap _instance;
	public static StartOnTap instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<StartOnTap>();
			}
			return _instance;
		}
	}

	public enum State { View, Play }
	public State currentState { get; private set; }

	public void SetState(State value, float timeCamTransition = 6f, bool forced = false)
	{
		if (!forced && (value == currentState || transitioning))
			return;
		if (value == State.View)
		{
			character.GetComponent<FollowMouse>().enabled = false;
			GetComponent<CameraDragger>().enabled = false;
			GetComponent<CameraSizer>().enabled = false;
			Time.timeScale = 0f;
			character.SetActive(false);
			PositionTweenable.Get(gameObject).TweenXYPosition(Vector2.zero, timeCamTransition, 0f, iTween.EaseType.easeInOutCubic);
			SizeTweenable.Get(gameObject).TweenSize(7.2f, timeCamTransition, 0f, iTween.EaseType.easeInOutCubic);
			viewButton.gameObject.SetActive(false);
		}
		else if (value == State.Play)
		{
			Time.timeScale = origTimeScale;
			SizeTweenable.Get(gameObject).StopTween();
			PositionTweenable.Get(gameObject).StopTween();
			GetComponent<CameraDragger>().enabled = true;
			GetComponent<CameraSizer>().enabled = true;
			character.SetActive(true);
			character.GetComponent<FollowMouse>().enabled = true;
			viewButton.gameObject.SetActive(true);
			introText.SetActive(false);
		}
		currentState = value;
		transitioning = true;
		timeStopTransitioning = Time.realtimeSinceStartup + timeTransitioning;
	}

	public GameObject character;
	public Clickable viewButton;
	private bool _transitioning = false;
	public bool transitioning
	{
		get
		{
			return _transitioning;
		}
		set
		{
			_transitioning = value;
		}
	}
	private float timeStopTransitioning = float.MaxValue;
	public float timeTransitioning = 0.1f;

	private float origTimeScale;

	public GameObject introText;

	void Awake()
	{
		origTimeScale = Time.timeScale;
		viewButton.OnClick += HandleOnClickViewButton;
	}

	void Start () 
	{
		SetState(State.View, 0f, true);
		ScaleTweenable.Get(introText).TweenScaleFrom(1.5f, 0.1f, 2f, 0f, iTween.EaseType.easeInQuad);
	}

	void Update () 
	{
		if (Time.realtimeSinceStartup >= timeStopTransitioning)
			transitioning = false;

		if (currentState == State.View && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
		{
			SetState(State.Play);
		}
	}

	void HandleOnClickViewButton()
	{
		if (currentState == State.Play)
		{
			SetState(State.View, 3f);
		}
	}

}
