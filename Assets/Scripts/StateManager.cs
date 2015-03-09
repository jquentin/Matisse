using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	private static StateManager _instance;
	public static StateManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<StateManager>();
			}
			return _instance;
		}
	}

	public enum State { View, Play }
	public State currentState { get; private set; }
	public delegate void OnStateChangedHandler(State state);
	public OnStateChangedHandler OnStateChanged;

	public void SetState(State value, float timeCamTransition = 6f, bool forced = false)
	{
		if (!forced && (value == currentState || transitioning || gameEnded))
			return;
		if (value == State.View)
		{
			character.GetComponent<FollowMouse>().enabled = false;
			GetComponent<CameraDragger>().enabled = false;
			GetComponent<CameraSizer>().enabled = false;
			Time.timeScale = 0f;
			character.SetActive(false);
			PositionTweenable.Get(gameObject).TweenXYPosition(Vector2.zero, timeCamTransition, 0f, iTween.EaseType.easeInOutCubic);
			SizeTweenable.Get(gameObject).TweenSize(sizeCamView, timeCamTransition, 0f, iTween.EaseType.easeInOutCubic);
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
		}
		if (OnStateChanged != null)
			OnStateChanged(value);
		currentState = value;
		transitioning = true;
		timeStopTransitioning = Time.realtimeSinceStartup + timeTransitioning;
	}

	public float sizeCamView = 7.2f;
	private GameObject character;
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

	private bool gameEnded = false;

	void Awake()
	{
		origTimeScale = Time.timeScale;
		WeedCleanCheck.instance.OnComplete += EndGame;
		character = GameObject.FindWithTag("Player");
	}

	void Start () 
	{
		SetState(State.View, 0f, true);
	}

	void Update () 
	{
		if (Time.realtimeSinceStartup >= timeStopTransitioning)
			transitioning = false;

		if (currentState == State.View && (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)))
		{
			SetState(State.Play);
		}
		if (gameEnded && (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
		{
			Application.LoadLevel("title");
		}
	}

	public void EndGame()
	{
		SetState(StateManager.State.View);
		gameEnded = true;
	}

}
