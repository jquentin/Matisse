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
				info += "clusters in " + id + " : ";
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

	int NbClusters(List<Sortable> weeds)
	{
		int count = weeds.Count;
		//List of clusters, starts with each object separated, progressively grouped together
		List<List<int>> clusters = new List<List<int>>(count);
		//Dictionary of indexes, to store where in the cluster list each object is.
		List<int> dictIndex = new List<int>(count);
		for (int i = 0 ; i < count ; i++)
		{
			clusters.Insert(i, new List<int>(count) { i });
			dictIndex.Insert(i, i);
		}
		
		for (int i = 0 ; i < count - 1 ; i++)
		{
			for (int j = i + 1 ; j < count ; j++)
			{
				float dist = Distance(weeds[i].transform, weeds[j].transform);
				if (dist <= maxDist)
					Fusion(i, j, clusters, dictIndex);
			}
		}
		
		int nbClusters = clusters.FindAll(delegate(List<int> obj) {return obj != null; }).Count;
		info += nbClusters + " / " + clusters.Count + "\n";
		return nbClusters;
	}

	bool IsSorted(List<Sortable> weeds)
	{
		return NbClusters(weeds) == 1;
	}

	float Distance(Transform a, Transform b)
	{
		RaycastHit[] hitInfoAB = Physics.RaycastAll(new Ray(a.position, b.position - a.position));
		float distAB = float.MaxValue;
		foreach (RaycastHit rh in hitInfoAB)
			if (rh.transform.IsChildOf(b))
				distAB = rh.distance;
		RaycastHit[] hitInfoBA = Physics.RaycastAll(new Ray(b.position, a.position - b.position));
		float distBA = float.MaxValue;
		foreach (RaycastHit rh in hitInfoBA)
			if (rh.transform.IsChildOf(a))
				distBA = rh.distance;
		float dist = (a.position - b.position).magnitude;
		float dist2 = (distAB + distBA - dist) * 0.5f;
		return dist2;
	}

	void Fusion (int a, int b, List<List<int>> list, List<int> dic)
	{
		int setA = dic[a];
		int setB = dic[b];
		// If the objects are already in the same set, do nothing
		if (setA == setB)
			return;
		// To fusion, add object b's set to object a's set
		list[setA].AddRange(list[setB]);
		// And tell the index dictionary that the objects in the object b's set are at the same index as the object a
		foreach(int i in list[setB])
			dic[i] = setA;
		// don't remove the old unused object b's set, as the indexes need to stay the same in the list. Empty it instead.
		list[setB] = null;
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
