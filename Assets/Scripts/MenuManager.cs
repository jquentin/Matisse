using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private string sceneToLoad = "";

	void Start()
	{
		Time.timeScale = 1f;
	}

	public void GoToScene(string scene) 
	{
		sceneToLoad = scene;
		Invoke("LoadScene", 0.5f);
//		Application.LoadLevel(scene);
	}

	void LoadScene()
	{
		Application.LoadLevel(sceneToLoad);
	}

}
