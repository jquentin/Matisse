using UnityEngine;
using System.Collections;

public class Weed : Sortable{

	public enum Color { Red, Orange, Blue, Green, Pink } 

	public Weed.Color color;

	public override int sortId
	{
		get
		{
			return (int)color;
		}
	}
}
