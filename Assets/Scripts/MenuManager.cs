using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private string sceneToLoad = "";
	AsyncOperation op;

	void Start()
	{
		Time.timeScale = 1f;
	}

	public void GoToScene(string scene) 
	{
		sceneToLoad = scene;
		Invoke("LoadScene", 1f);
		op = Application.LoadLevelAsync(sceneToLoad);
		op.allowSceneActivation = false;
//		Application.LoadLevel(scene);
	}

	void LoadScene()
	{
		op.allowSceneActivation = true;
	}

}
