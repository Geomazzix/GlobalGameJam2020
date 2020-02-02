using UnityEngine.Events;
using UnityEngine;

public class ButtonScript : MonoBehaviour, I_Interactable
{
    public UnityEvent m_event;
    public AudioClip buttonSound;

    public void onInteract()
    {
        FindObjectOfType<SoundController>().PlayAudioSource(buttonSound, transform.position);
        m_event.Invoke();
    }
}
