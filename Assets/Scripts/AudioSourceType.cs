using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioType { Sound, Music, VoiceOver, ToBePlayedAnyway}

[RequireComponent(typeof(AudioSource))]
public class AudioSourceType : MonoBehaviour {

	public AudioType audioType = AudioType.Sound;

	void Awake () 
	{
		UpdateSource();
		AudioSettings.OnMusicStateChanged += UpdateSource;
	}

	void OnDestroy()
	{
		AudioSettings.OnMusicStateChanged -= UpdateSource;
	}

	void UpdateSource()
	{
		bool playable = audioType.IsPlayable();
		GetComponent<AudioSource>().enabled = playable;
	}

}

public static class AudioSettings
{
	
	private static bool _voiceOverEnabled;
	private static bool _sfxEnabled;
	private static bool _extendedStoryTellingVoiceOverEnabled;
	
	private static bool alreadyLoadedSavedSettingVoiceOverEnabled;
	private static bool alreadyLoadedSavedSettingSFXEnabled;
	private static bool alreadyLoadedSavedSettingextendedStoryTellingVoiceOverEnabled;

	public delegate void OnMusicStateChangedHandler();
	public static OnMusicStateChangedHandler OnMusicStateChanged;

	public static bool voiceOverEnabled{
		get {
			if(!alreadyLoadedSavedSettingVoiceOverEnabled){
				int _voiceOverEnabledInt = PlayerPrefs.GetInt("_voiceOverEnabled",1);
				if(_voiceOverEnabledInt==1){
					_voiceOverEnabled = true;
				}else{
					_voiceOverEnabled = false;
				}
				alreadyLoadedSavedSettingVoiceOverEnabled = true;
			}
			return _voiceOverEnabled;
		}
		
		set {
			_voiceOverEnabled = value;
			
			Debug.Log("_voiceOverEnabled = "+value);
			
			if(_voiceOverEnabled){
				PlayerPrefs.SetInt("_voiceOverEnabled", 1);
			}else{
				PlayerPrefs.SetInt("_voiceOverEnabled", 0);
			}
		}
	}
	
	
	public static bool extendedStoryTellingVoiceOverEnabled{
		get {
			if(!alreadyLoadedSavedSettingextendedStoryTellingVoiceOverEnabled){
				int _extendedStoryTellingVoiceOverEnabledInt = PlayerPrefs.GetInt("_extendedStoryTellingVoiceOverEnabled",1);
				if(_extendedStoryTellingVoiceOverEnabledInt==1){
					_extendedStoryTellingVoiceOverEnabled = true;
				}else{
					_extendedStoryTellingVoiceOverEnabled = false;
				}
				alreadyLoadedSavedSettingextendedStoryTellingVoiceOverEnabled = true;
			}
			return _extendedStoryTellingVoiceOverEnabled;
		}
		
		set {
			_extendedStoryTellingVoiceOverEnabled = value;
			
			Debug.Log("_extendedStoryTellingVoiceOverEnabled = "+value);
			
			if(_extendedStoryTellingVoiceOverEnabled){
				PlayerPrefs.SetInt("_extendedStoryTellingVoiceOverEnabled", 1);
			}else{
				PlayerPrefs.SetInt("_extendedStoryTellingVoiceOverEnabled", 0);
			}
			if (OnMusicStateChanged != null)
				OnMusicStateChanged();
		}
	}
	
	
	
	public static bool sfxEnabled{
		get {
			if(!alreadyLoadedSavedSettingSFXEnabled){
				int _sfxEnabledInt = PlayerPrefs.GetInt("_sfxEnabled",1);
				if(_sfxEnabledInt==1){
					_sfxEnabled = true;
				}else{
					_sfxEnabled = false;
				}
				alreadyLoadedSavedSettingSFXEnabled = true;
			}
			return _sfxEnabled;
		}
		
		set {
			_sfxEnabled = value;
			
			Debug.Log("_sfxEnabled = "+value);
			
			if(_sfxEnabled){
				PlayerPrefs.SetInt("_sfxEnabled", 1);
			}else{
				PlayerPrefs.SetInt("_sfxEnabled", 0);
			}
		}
	}
}

public static class AudioUtils
{

	
	public static AudioType GetAudioType(this AudioSource source)
	{
		AudioSourceType sourceType = source.GetComponent<AudioSourceType>();
		AudioType type;
		if (sourceType == null)
			type = AudioType.Sound;
		else
			type = sourceType.audioType;
		return type;
	}
	
	public static bool IsPlayable(this AudioType type)
	{
		return (type == AudioType.ToBePlayedAnyway
		        ||type == AudioType.Sound && AudioSettings.sfxEnabled
		        || type == AudioType.Music && AudioSettings.extendedStoryTellingVoiceOverEnabled
		        || type == AudioType.VoiceOver && AudioSettings.voiceOverEnabled);
	}
	
	public static void PlayOneShotControlled(this AudioSource source, List<AudioClip> clips)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		source.PlayOneShotControlled(clips, source.GetAudioType());
	}
	
	public static void PlayOneShotControlled(this AudioSource source, List<AudioClip> clips, AudioType type)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		if (!type.IsPlayable())
			return;
//		#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5 && !UNITY_4_6
//		source.spatialBlend = 0f;
//		#endif
		if (clips.Count > 0)
			source.PlayOneShot(clips[Random.Range(0, clips.Count)]);
		else
			Debug.LogWarning("Empty sound list to play");
	}
	
	public static void PlayOneShotControlled(this AudioSource source, AudioClip clip)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		source.PlayOneShotControlled(clip, source.GetAudioType());
	}
	
	
	public static void PlayOneShotControlled(this AudioSource source, AudioClip clip, AudioType type)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		if (!type.IsPlayable())
			return;
//		#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5 && !UNITY_4_6
//		source.spatialBlend = 0f;
//		#endif
		source.PlayOneShot(clip);
	}
	
	public static void PlayOneShotControlled(this AudioSource source, List<LocalizableAudioClip> clips)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		source.PlayOneShotControlled(clips, source.GetAudioType());
	}
	
	public static void PlayOneShotControlled(this AudioSource source, List<LocalizableAudioClip> clips, AudioType type)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		if (!type.IsPlayable())
			return;
//		#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5 && !UNITY_4_6
//		source.spatialBlend = 0f;
//		#endif
		if (clips.Count > 0)
			source.PlayOneShot(clips[Random.Range(0, clips.Count)].clip);
		else
			Debug.LogWarning("Empty sound list to play");
	}
	
	public static void PlayOneShotControlled(this AudioSource source, LocalizableAudioClip clip)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		source.PlayOneShotControlled(clip, source.GetAudioType());
	}
	
	
	public static void PlayOneShotControlled(this AudioSource source, LocalizableAudioClip clip, AudioType type)
	{
		if (source == null)
		{
			Debug.LogWarning("PlayOneShotControlled null audio source");
			return;
		}
		if (!type.IsPlayable())
			return;
//		#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5 && !UNITY_4_6
//		source.spatialBlend = 0f;
//		#endif
		source.PlayOneShot(clip.clip);
	}

}
