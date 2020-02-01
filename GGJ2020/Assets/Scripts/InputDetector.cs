 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(amountOfObjects==1)
        {
            inputIndicator.SetActive(true);
        }
        else
        {
            inputIndicator.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<I_PickupItem>() != null)
        {
            amountOfObjects++;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<I_PickupItem>() != null)
        {
            amountOfObjects--;
        }
    }



    [SerializeField]
    private GameObject inputIndicator;

    [SerializeField]
    private BoxCollider inputCollider;

    private int amountOfObjects = 0;

}
