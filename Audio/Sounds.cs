using System.Collections;
using System.IO;
using GFApi;
using UnityEngine;
using UnityEngine.Networking;

namespace GFApi.Audio{
    public static class Sounds
    {
        public static void PlaySound(AudioClip soundToPlay, bool randomPitch)
        {
            GameObject gameObject = new GameObject("Sound");
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            if (randomPitch)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
            }
            audioSource.outputAudioMixerGroup = MainPlugin.soundManager.sfxGroup;
            audioSource.PlayOneShot(soundToPlay);
            audioSource.gameObject.AddComponent<DestroyAfterSound>();
            audioSource.gameObject.GetComponent<DestroyAfterSound>().audioSource = audioSource;
        }

        public static AudioClip LoadAudioFromFile(string filePath)
        {
            WWW audioLoader = new WWW(Path.Combine("file://", filePath));
            AudioClip selectedClip = audioLoader.GetAudioClip(false, false, AudioType.MPEG);
            return selectedClip;
        }
        
        public static IEnumerator LoadAudioFromFile(string filePath, System.Action<AudioClip> callback)
		{
			using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG))
			{
				yield return www.SendWebRequest();

				if (www.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError("Failed to load audio: " + www.error);
					callback?.Invoke(null);
				}
				else
				{
					AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
					callback?.Invoke(clip);
				}
			}
		}
    }
}