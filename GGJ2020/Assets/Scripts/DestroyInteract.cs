using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteract : MonoBehaviour, I_Interactable
{

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
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Go = transform.GetChild(i).gameObject;
            Go.GetComponent<Collider>().enabled = true;
            Go.AddComponent<Rigidbody>();
        }
        Invoke("disableColliders", 0.2f);
    }

    void disableColliders()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Go = transform.GetChild(i).gameObject;
            Destroy(Go.GetComponent<Collider>());
        }
    }

}
