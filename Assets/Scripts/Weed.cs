using UnityEngine;
using System.Collections;

public class Weed : Sortable{

	public enum WeedColor { Red, Orange, Blue, Green, Pink } 

	public WeedColor color;

	public override int sortId
	{
		get
		{
			return (int)color;
		}
	}
	
	protected override void HandleOnSorted(int id, bool sorted)
	{
//		if (sortId == id)
//			GetComponent<SpriteRenderer>().color = (sorted ? Color.green : Color.white);
	}
}
