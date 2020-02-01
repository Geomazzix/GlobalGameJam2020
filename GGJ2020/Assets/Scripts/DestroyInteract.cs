using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteract : PickupItem, I_Interactable
{

    public void onInteract()
    {
        Debug.Log("hi");
        if (pickedUp) {
            releaseFromPlayer();
        }
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());
        int count = transform.childCount;
        for (int i = count; i > 0; --i)
        {
            GameObject Go = transform.GetChild(i - 1).gameObject;
            Go.GetComponent<Collider>().enabled = true;
            Go.AddComponent<Rigidbody>();

            if(Go.GetComponent<Item>() != null)
            {
                Go.transform.Translate(new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
                Go.transform.SetParent(null);
            }

        }
        Invoke("disableColliders", 0.2f);
        Invoke("destroyEverything", 2.5f);
    }

    void disableColliders()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Go = transform.GetChild(i).gameObject;
            Destroy(Go.GetComponent<Collider>());
        }
    }

    void destroyEverything()
    {
        int count = transform.childCount;
        for (int i = count; i > 0; i--)
        {
            GameObject Go = transform.GetChild(i - 1).gameObject;
            Destroy(Go);
        }
        Destroy(gameObject);
    }

}
