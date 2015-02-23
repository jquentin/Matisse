using UnityEngine;
using System.Collections.Generic;

public class WeedCleanCheck : MonoBehaviour {

	public GameObject character;

	public List<Sortable> weeds = new List<Sortable>();
	Dictionary<int, List<Sortable>> weedsByColor;
	public float maxDist = 1f;
	private bool hasSucceed = false;
	public GameObject textEnd;

	void Start()
	{
		textEnd.SetActive(false);
		weedsByColor = new Dictionary<int, List<Sortable>>();
		foreach(Sortable w in weeds)
		{
			if(weedsByColor.ContainsKey(w.sortId))
			   weedsByColor[w.sortId].Add(w);
			else
				weedsByColor.Add(w.sortId, new List<Sortable>(new Sortable[]{w}));
		}
	}

	void Update ()
	{
		if (hasSucceed)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel(0);
			}
		}
		else
		{
			foreach(List<Sortable> c in weedsByColor.Values)
			{
				if(ExistsTooFar(c))
					return;
			}
			Success();
		}
	}

	bool ExistsTooFar(List<Sortable> weeds)
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
		StartOnTap.instance.EndGame();
		textEnd.SetActive(true);
	}

	void UpdateCameraSize(float value)
	{
		Camera.main.orthographicSize = value;
	}

	[ContextMenu("Fill with children")]
	void FillWithChildren()
	{
		weeds.Clear();
		weeds.AddRange(GetComponentsInChildren<Sortable>());
	}
}
