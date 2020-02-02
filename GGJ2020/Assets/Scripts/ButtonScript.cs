using UnityEngine.Events;
using UnityEngine;

public class ButtonScript : MonoBehaviour, I_Interactable
{
    public UnityEvent m_event;
 
    public void onInteract()
    {
        m_event.Invoke();
    }
}
