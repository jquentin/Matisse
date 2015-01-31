using UnityEngine;
using System.Collections;

public class StartOnTap : MonoBehaviour {

	public GameObject character;

	private bool hasStarted = false;
	private float origTimeScale;

	void Start () {
		GetComponent<CameraDragger>().enabled = false;
		GetComponent<CameraSizer>().enabled = false;
		origTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		character.SetActive(false);
	}

	void Update () {
		if (hasStarted)
			return;
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			hasStarted = true;
			Time.timeScale = origTimeScale;
			GetComponent<CameraDragger>().enabled = true;
			GetComponent<CameraSizer>().enabled = true;
			character.SetActive(true);
		}
	}
}
