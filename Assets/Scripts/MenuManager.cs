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
#if UNITY_PRO_LICENSE
		op = Application.LoadLevelAsync(scene);
		op.allowSceneActivation = false;
#else
		sceneToLoad = scene;
#endif
		Invoke("LoadScene", 1f);
	}

	void LoadScene()
	{
#if UNITY_PRO_LICENSE
		op.allowSceneActivation = true;
#else
		Application.LoadLevel(sceneToLoad);
#endif
	}

}
