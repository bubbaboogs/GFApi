using System.Collections;
using System.IO;
using GFApi;
using UnityEngine;
using UnityEngine.Networking;

namespace GFApi.Audio{
    public static class Sounds{
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

        public static AudioClip LoadAudioFromFile(string filePath){
            WWW audioLoader = new WWW(Path.Combine("file://", filePath));
	        AudioClip selectedClip = audioLoader.GetAudioClip(false, false, AudioType.MPEG);
            return selectedClip;
        }
    }
}