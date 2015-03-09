using UnityEngine;
using System.Collections;

public class BasicSortable : Sortable {

	public int _sortId;

	public override int sortId 
	{
		get 
		{
			return _sortId;
		}
	}
	
	protected override void HandleOnSorted(int id, bool sorted)
	{
//		if (sortId == id)
//			GetComponent<SpriteRenderer>().color = (sorted ? Color.green : Color.white);
	}

}
