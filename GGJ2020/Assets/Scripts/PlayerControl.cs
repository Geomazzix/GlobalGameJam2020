using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //settings
    public float mouseSensitivity;
    public Transform cameraTransform;
    public float moveSpeed;
    public float itemFollowSpeed;
    public float maxPickupDistance;
    public float pickupObjectDistance;
    public float pickedupObjectVerticalOriginOffset;

    //variables
    private float rotationX = 0;
    private bool wasClicked = false;
    private GameObject pickedObject = null;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {

        //item interactions
        if (Input.GetMouseButtonDown(0)) {
            if (!wasClicked) {
                wasClicked = true;
                
                //pick up an item
                if(pickedObject == null) {
                    RaycastHit hit;
                    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxPickupDistance)) {
                        if (hit.transform.gameObject.GetComponent<I_PickupItem>() != null) {
                            pickedObject = hit.transform.gameObject;
                            Rigidbody pickedBody = pickedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
                            pickedBody.useGravity = false;
                        }
                    }
                }
                //throw the picked up item
                else {
                    Rigidbody pickedBody = pickedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
                    pickedBody.useGravity = true;
                    pickedBody.velocity = Vector3.zero;
                    pickedObject = null;
                }
                
            }
        }
        //resets wasClicked
        else {
            wasClicked = false;
        }

    }

    void FixedUpdate() {
        //mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //movement input
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

        //camera rotation
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        //movement
        transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed);

        //item following camera
        if (pickedObject != null) {

            //rotating with camera
            pickedObject.transform.rotation = cameraTransform.rotation;

            //getting target position (corrected for collisions)
            Vector3 origin = cameraTransform.position + new Vector3(0, pickedupObjectVerticalOriginOffset, 0);
            Vector3 targetPos = origin + cameraTransform.forward * pickupObjectDistance;
            RaycastHit hit;
            Vector3 diff = (targetPos - pickedObject.transform.position);
            if (Physics.Raycast(pickedObject.transform.position, diff.normalized, out hit, diff.magnitude)) {
                targetPos = hit.point;
                diff = (targetPos - pickedObject.transform.position);
            }

            //moving the object
            Rigidbody pickedBody = pickedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
            if (diff.sqrMagnitude > 0.001f) {
                pickedBody.MovePosition(targetPos);
            }
            
        }
    }

}
