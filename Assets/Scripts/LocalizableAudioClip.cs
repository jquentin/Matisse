using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class LocalizableAudioClip {

	public string key;
	private bool alreadyTriedLoading = false;
	private AudioClip _clip;
	public AudioClip clip
	{
		get
		{
//			if (_clip == null && !alreadyTriedLoading)
//			{
//				alreadyTriedLoading = true;
//				if (string.IsNullOrEmpty(key))
//				{
//					Debug.LogWarning("LocalizableAudioClip empty key");
//					return null;
//				}
//				string name = Localization.Get(key);
//				// if there is no value for this key, try with adding the language's suffix at the end of the key
//				if (string.IsNullOrEmpty(name))
//				{
//					name = Localization.Get("Language_Prefix") + key;
//				}
//				if (string.IsNullOrEmpty(name))
//				{
//					Debug.LogWarning("LocalizableAudioClip key not found in dictionary");
//					return null;
//				}
//				_clip = Resources.Load(name) as AudioClip;
//				if (_clip == null)
//				{
//					Debug.LogWarning("LocalizableAudioClip clip not found in Resources:" + name);
//				}
//			}
			return _clip;
		}
	}

}
