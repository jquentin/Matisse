using UnityEngine;
using System.Collections.Generic;

public class WeedCleanCheck : MonoBehaviour {

	public GameObject character;

	public List<Weed> weeds = new List<Weed>();
	Dictionary<Weed.Color, List<Weed>> weedsByColor;
	public float maxDist = 1f;
	private bool hasSucceed = false;

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
		if (hasSucceed)
			return;
		foreach(List<Weed> c in weedsByColor.Values)
		{
			if(ExistsTooFar(c))
				return;
		}
		Success();
	}

	bool ExistsTooFar(List<Weed> weeds)
	{
		for (int i = 0 ; i < weeds.Count ; i++)
		{
			bool existsCloseOne = false;
			for (int j = 0 ; j < weeds.Count ; j++)
			{
				if (i != j && (weeds[i].transform.position - weeds[j].transform.position).magnitude <= maxDist)
					existsCloseOne = true;
			}
			if (!existsCloseOne)
				return true;
		}
		return false;
	}

	void Success()
	{
		hasSucceed = true;
		StartOnTap.instance.SetState(StartOnTap.State.View);
	}

	void UpdateCameraSize(float value)
	{
		Camera.main.orthographicSize = value;
	}

	[ContextMenu("Fill with children")]
	void FillWithChildren()
	{
		weeds.Clear();
		weeds.AddRange(GetComponentsInChildren<Weed>());
	}
}
