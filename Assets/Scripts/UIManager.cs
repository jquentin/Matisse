using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private static UIManager _instance;
	public static UIManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<UIManager>();
			}
			return _instance;
		}
	}

	public GameObject introText;
	public GameObject endText;
	public Clickable viewButton;
	public Clickable menuButton;

	void Awake()
	{
		StateManager.instance.OnStateChanged += HandleStateChanged;
		WeedCleanCheck.instance.OnComplete += HandleComplete;
		viewButton.OnClick += HandleOnClickViewButton;
		menuButton.OnClick += HandleOnClickMenuButton;
	}

	void Start ()
	{
		endText.SetActive(false);
		ScaleTweenable.Get(introText).TweenScaleFrom(1.5f, 0.1f, 2f, 0f, iTween.EaseType.easeInQuad);
	}

	void HandleStateChanged(StateManager.State state)
	{
		if (state == StateManager.State.View)
		{
			viewButton.gameObject.SetActive(false);
			menuButton.gameObject.SetActive(true);
		}
		else if (state == StateManager.State.Play)
		{
			viewButton.gameObject.SetActive(true);
			menuButton.gameObject.SetActive(false);
			introText.SetActive(false);
		}
	}
	
	void HandleOnClickViewButton()
	{
		StateManager.instance.SetState(StateManager.State.View, 2f);
	}
	
	void HandleOnClickMenuButton()
	{
		Application.LoadLevel("title");
	}

	void HandleComplete()
	{
		endText.SetActive(true);
	}

}
