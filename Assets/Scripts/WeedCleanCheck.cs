using UnityEngine;
using System.Collections.Generic;

public class WeedCleanCheck : MonoBehaviour {

	public GameObject character;

	public List<Weed> weeds;
//	public List<List<Weed>> weedsByColor;
	Dictionary<Weed.Color, List<Weed>> weedsByColor;
	public float maxDist = 1f;

	void Start()
	{
		weedsByColor = new Dictionary<Weed.Color, List<Weed>>();
		foreach(Weed w in weeds)
		{
			if(weedsByColor.ContainsKey(w.color))
			   weedsByColor[w.color].Add(w);
			else
				weedsByColor.Add(w.color, new List<Weed>(new Weed[]{w}));
		}
	}

	void Update ()
	{
		foreach(List<Weed> c in weedsByColor.Values)
		{
			if(ExistsTooFar(c))
				return;
		}
		Success();
	}

	bool ExistsTooFar(List<Weed> weeds)
	{
		for (int i = 0 ; i < weeds.Count - 1 ; i++)
		{
			for (int j = i + 1 ; j < weeds.Count ; j++)
			{
				if ((weeds[i].transform.position - weeds[j].transform.position).magnitude > maxDist)
					return true;
			}
		}
		return false;
	}

	void Success()
	{
		character.GetComponent<FollowMouse>().enabled = false;
		Camera.main.GetComponent<CameraDragger>().enabled = false;
		Camera.main.GetComponent<CameraSizer>().enabled = false;
		PositionTweenable.Get(Camera.main.gameObject).TweenXYPosition(Vector2.zero,3f);
		iTween.ValueTo(gameObject, iTween.Hash(
			"from", Camera.main.orthographicSize,
			"to", 5f,
			"time", 2f,
			"onupdate", "UpdateCameraSize"));
	}

	void UpdateCameraSize(float value)
	{
		Camera.main.orthographicSize = value;
	}
}
