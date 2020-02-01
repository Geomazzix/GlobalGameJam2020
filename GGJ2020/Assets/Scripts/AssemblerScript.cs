using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblerScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        animControllerPress = GetComponent<Animator>();
        animControllerLeftFlap = flapLeft.GetComponent<Animator>();
        animControllerRightFlap = flapRight.GetComponent<Animator>();
        animControllerPress.SetBool("shouldClose", false);
        animControllerLeftFlap.SetBool("shouldClose", false);
        animControllerRightFlap.SetBool("shouldClose", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActivate()
    {
        // check if input is valid.
        if (!(left.GetValidInput() && right.GetValidInput()))
        {
            // give red light. text sign "Seriously?"
        }
        else
        {
           
            Item leftItem = left.GetItem();
            Item rightItem = right.GetItem();
            EPart combo = Part.GetPossibleItemCombination(leftItem.Type, rightItem.Type);
            if ((int)combo == -1)
            {
                // give feedback that bad combo has been made.
                animControllerLeftFlap.SetBool("shouldClose", false);
                animControllerRightFlap.SetBool("shouldClose", false);
            }
            else
            {
                animControllerLeftFlap.SetBool("shouldClose", true);
                animControllerRightFlap.SetBool("shouldClose", true);
                leftToDelete = leftItem.gameObject;
                rightToDelete = rightItem.gameObject;
                // instantiate correct combo result.

                Invoke("openFlapsLate",2.5f);
                //Invoke("closePressLate",1.5f);
                //Invoke("openPressLate",3.5f);
            }
        }
    }

    public void SpawnResult()
    {

    }

    private Animator animControllerPress;
    private Animator animControllerLeftFlap;
    private Animator animControllerRightFlap;


    void openFlapsLate()
    {
        Destroy(leftToDelete);
        Destroy(rightToDelete);
        animControllerLeftFlap.SetBool("shouldClose", false);
        animControllerRightFlap.SetBool("shouldClose", false);      
    }

    void closePressLate()
    {
        animControllerPress.SetBool("shouldClose", true);
    }

    void openPressLate()
    {
        animControllerPress.SetBool("shouldClose", false);
    }

    private GameObject leftToDelete, rightToDelete;

    [SerializeField]
    private InputDetector left, right;

    [SerializeField]
    private GameObject flapLeft, flapRight, BigPress;

}
