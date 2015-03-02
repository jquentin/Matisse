using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

	public int level;

	public IEnumerator OnMouseDown () 
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
		ScaleTweenable st = ScaleTweenable.Get(gameObject);
		st.TweenScale(4f * st.scale, 1f);
		PositionTweenable.Get(gameObject).TweenYPosition(0f, 1f);
		yield return new WaitForSeconds(1.1f);
		Application.LoadLevel(level);
	}

}
