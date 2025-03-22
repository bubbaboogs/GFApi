using GFApi;
using UnityEngine;

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
    }
}