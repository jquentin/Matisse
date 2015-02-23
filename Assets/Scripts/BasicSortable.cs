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

}
