using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public GameObject audioSource;

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
        playingEmitters.Add(gm);
        Invoke("deleteSoundSource", sfx.length + 0.5f);
    }

    public void deleteSoundSource()
    {
        foreach(GameObject obj in playingEmitters)
        {
            if (obj.GetComponent<AudioSource>().isPlaying == false)
            {
                for(int i = obj.transform.childCount; i > 0; i--)
                {
                    Destroy(obj.transform.GetChild(i - 1).gameObject);
                }
                Destroy(obj);
            }
        }
    }

    private List<GameObject> playingEmitters = new List<GameObject>();
}
