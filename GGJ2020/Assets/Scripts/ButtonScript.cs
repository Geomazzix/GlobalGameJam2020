using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ButtonScript : MonoBehaviour, I_Interactable
{

    public UnityEvent m_event;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onInteract()
    {
        m_event.Invoke();
    }
}
