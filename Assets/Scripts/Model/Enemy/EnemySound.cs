using UnityEngine;

[System.Serializable]
public class EnemySound
{
	#region Config
	public string soundName;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume;

	[Range(.1f, 3f)]
	public float pitch;

	[Range(0f, 100f)]
	public float fadeOutDuration;

	public float targetFadeOutVolume;

	public bool loop;
	#endregion

	//[HideInInspector]
	public AudioSource source;
}

