using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayAudioSource(AudioSource prefab, Vector3 position)
    {
        GameObject gm = Instantiate(prefab.gameObject, position, Quaternion.identity);
        gm.GetComponent<AudioSource>().Play();
    }
}
