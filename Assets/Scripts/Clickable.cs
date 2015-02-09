using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

	public delegate void OnClickHandler();
	public OnClickHandler OnClick;

	void OnMouseDown () 
	{
		if (OnClick != null)
			OnClick();
	}

}
