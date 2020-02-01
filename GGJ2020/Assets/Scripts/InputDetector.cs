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
        if (inputIndicator != null)
        {
            if (amountOfObjects == 1)
            {
                inputIndicator.SetActive(true);
            }
            else
            {
                inputIndicator.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<I_PickupItem>() != null)
        {
            amountOfObjects++;
            items.Add(other.gameObject.GetComponent<Item>());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<I_PickupItem>() != null)
        {
            amountOfObjects--;
            items.Remove(other.gameObject.GetComponent<Item>());
        }
    }

    public Item GetItem()
    {
        return items[0];
    }

    public bool GetValidInput()
    {
        // if item is material return true;
        if (amountOfObjects == 1 && (int)items[0].Type < 5) return true;
        return false;
    }


    [SerializeField]
    private GameObject inputIndicator;

    [SerializeField]
    private BoxCollider inputCollider;


    private List<Item> items;

    private int amountOfObjects = 0;

}
