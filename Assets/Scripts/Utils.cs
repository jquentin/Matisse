using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GettableComponent <Type> : MonoBehaviour where Type : Component
{
	
	public static Type Get(GameObject go)
	{
		Type res = go.GetComponent<Type>();
		if (res == null)
			res = go.AddComponent<Type>();
		return res;
	}
}

public static class Utils 
{

	public static float RandomXOnScreen(this Camera cam)
	{
		float z = -cam.transform.position.z;
		float leftScreen = cam.ScreenToWorldPoint(new Vector3(0f, 0f, z)).x;
		float rightScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, z)).x;
		float x = Random.Range(leftScreen, rightScreen);
		return x;
	}

	public static float RandomXOnScreen()
	{
		return Camera.main.RandomXOnScreen();
	}

	public static float randomSign
	{
		get
		{
			return Random.Range(0f, 1f) > 0.5f ? -1f : 1f;
		}
	}

	public static void DestroyChildren(this Transform root) {
		int childCount = root.childCount;
		for (int i = root.childCount - 1 ; i >= 0 ; i--) {
			GameObject.Destroy(root.GetChild(i).gameObject);
		}
	}
	
	public static void DestroyChildrenImmediate(this Transform root, params Transform[] exceptions) {
		List<Transform> excList = new List<Transform>(exceptions);
		int childCount = root.childCount;
		for (int i = root.childCount - 1 ; i >= 0 ; i--) {
			Transform t = root.GetChild(i);
			if (!excList.Contains(t))
				GameObject.DestroyImmediate(t.gameObject);
		}
	}
	
	public static List<Transform> FindChildren(this Transform root, string name) {
		List<Transform> res = new List<Transform>();
		for(int i = 0 ; i < root.childCount ; i++)
		{
			Transform t = root.GetChild(i);
			if (t.name.Contains(name))
				res.Add(t);
			res.AddRange(t.FindChildren(name));
		}
		return res;
	}

	public static List<GameObject> FindChildrenGameObjects(this Transform root, string name = "", bool recursive = true) {
		List<GameObject> res = new List<GameObject>();
		for(int i = 0 ; i < root.childCount ; i++)
		{
			Transform t = root.GetChild(i);
			if (t.name.Contains(name))
				res.Add(t.gameObject);
			if (recursive)
				res.AddRange(t.FindChildrenGameObjects(name));
		}
		return res;
	}
	
	/// <summary>
	/// Finds the specified component on the game object or one of its parents.
	/// </summary>
	
	static public T FindInParents<T> (this Transform trans) where T : Component
	{
		if (trans == null) return null;
		T comp = trans.GetComponent<T>();
		if (comp == null)
		{
			Transform t = trans.transform.parent;
			
			while (t != null && comp == null)
			{
				comp = t.gameObject.GetComponent<T>();
				t = t.parent;
			}
		}
		return comp;
	}
	
	public static List<System.Type> GetSubTypes<T>() where T : class
	{
		var types = new List<System.Type>();
		
		foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
		{
			if (assembly.FullName.StartsWith("Mono.Cecil"))
				continue;
			
			if (assembly.FullName.StartsWith("UnityScript"))
				continue;
			
			if (assembly.FullName.StartsWith("Boo.Lan"))
				continue;
			
			if (assembly.FullName.StartsWith("System"))
				continue;
			
			if (assembly.FullName.StartsWith("I18N"))
				continue;
			
			if (assembly.FullName.StartsWith("UnityEngine"))
				continue;
			
			if (assembly.FullName.StartsWith("UnityEditor"))
				continue;
			
			if (assembly.FullName.StartsWith("mscorlib"))
				continue;
			
			foreach (System.Type type in assembly.GetTypes())
			{
				if (!type.IsClass)
					continue;
				
				if (type.IsAbstract)
					continue;
				
				if(!type.IsSubclassOf(typeof(T)))
					continue;
				
				types.Add(type);
			}
		}
		
		return types;
	}
	
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}

}
