using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayAudioSource(AudioClip sfx, Vector3 position)
    {
        GameObject gm = Instantiate(new GameObject(), position, Quaternion.identity);
        AudioSource source = gm.AddComponent<AudioSource>();
        source.clip = sfx;
        source.minDistance = 0.5f;
        source.maxDistance = 4;
        source.Play();
    }
}
