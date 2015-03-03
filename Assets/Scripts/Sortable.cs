using UnityEngine;
using System.Collections;

public abstract class Sortable : MonoBehaviour{

	public abstract int sortId
	{
		get;
	}

	protected virtual void Awake()
	{
		WeedCleanCheck.OnSorted += HandleOnSorted;
	}

	protected abstract void HandleOnSorted(int id, bool sorted);

}
