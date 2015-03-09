using UnityEngine;
using System.Collections.Generic;

public class WeedCleanCheck : MonoBehaviour {
	
	private static WeedCleanCheck _instance;
	public static WeedCleanCheck instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<WeedCleanCheck>();
			}
			return _instance;
		}
	}

	public List<Sortable> weeds = new List<Sortable>();
	Dictionary<int, List<Sortable>> weedsByColor;
	Dictionary<int, bool> groupsSortStates;
	public float maxDist = 1f;
	private float _sqrMaxDist;
	private bool hasSucceed = false;

	public delegate void OnSortedHandler(int id, bool sorted);
	public static OnSortedHandler OnSorted;
	
	public delegate void OnCompleteHandler();
	public OnCompleteHandler OnComplete;

	void Awake()
	{
		_sqrMaxDist = maxDist * maxDist;
	}

	void Start()
	{
		groupsSortStates = new Dictionary<int, bool>();
		foreach(Sortable w in weeds)
			if(!groupsSortStates.ContainsKey(w.sortId))
				groupsSortStates.Add(w.sortId, false);
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
			info = "Max Dist : " + maxDist + "\n";
//			foreach(List<Sortable> c in weedsByColor.Values)
//			{
//				if(ExistsTooFar(c))
//					return;
//			}
			bool fail = false;
			foreach(int id in weedsByColor.Keys)
			{
				List<Sortable> sameId = weedsByColor[id];
				info += "furthest in " + id + " : ";
				bool sorted = IsSorted(sameId);
				if (!sorted)
					fail = true;
				if (sorted != groupsSortStates[id])
				{
					groupsSortStates[id] = sorted;
					OnSorted(id, sorted);
				}
			}
			if (!fail)
				Success();
		}
	}

	bool IsSorted(List<Sortable> weeds)
	{
		float furthest = 0f;
		bool existsTooFar = false;
		for (int i = 0 ; i < weeds.Count ; i++)
		{
			float closest = float.MaxValue;
			bool existsCloseOne = false;
			if (weeds.Count == 1)
				existsCloseOne = true;
			for (int j = 0 ; j < weeds.Count ; j++)
			{
				if (i != j)
				{
					float sqrMag = (weeds[i].transform.position - weeds[j].transform.position).sqrMagnitude;
					closest = Mathf.Min(closest, sqrMag);
					if (sqrMag <= _sqrMaxDist)
					{
						existsCloseOne = true;
					}
				}
			}
			
			furthest = Mathf.Max(furthest, closest);
			if (!existsCloseOne)
				existsTooFar = true;
		}
		info += Mathf.Sqrt(furthest) + "\n";
		return !existsTooFar;
	}

	void Success()
	{
		hasSucceed = true;
		if (OnComplete != null)
			OnComplete();
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

	private string info = "";
	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label(new Rect(Screen.width * 0.15f, 10f, 200f, 100f), info);
	}
}
