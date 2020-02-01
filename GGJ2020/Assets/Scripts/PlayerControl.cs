using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //settings
    public float mouseSensitivity;
    public Transform cameraTransform;
    public float moveSpeed;
    public float maxPickupDistance;
    public float pickupObjectDistance;
    public float pickedupObjectVerticalOriginOffset;
    public float minPickupDistance;
    public float pickupMoveSpeed;

    //variables
    private float rotationX = 0;
    private bool wasClicked = false;
    private PickupItem pickedObject = null;
    private Canvas m_PauseCanvas;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        m_PauseCanvas = FindObjectOfType<Canvas>();
        m_PauseCanvas.gameObject.SetActive(false);
    }

    private void OnGUI() { 
        Event ev = Event.current;

        if (ev.type == EventType.KeyDown) {
            if(ev.keyCode == KeyCode.E) {
                RaycastHit hit;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxPickupDistance)) {
                    I_Interactable interactable = hit.transform.gameObject.GetComponent<I_Interactable>();
                    if(interactable != null) {
                        interactable.onInteract();
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            m_PauseCanvas.gameObject.SetActive(true);
            enabled = false;
        }

        //item interactions
        if (Input.GetMouseButtonDown(0)) {
            if (!wasClicked) {
                wasClicked = true;
                
                //pick up an item
                if(pickedObject == null) {
                    RaycastHit hit;
                    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxPickupDistance)) {
                        PickupItem pickup = hit.transform.gameObject.GetComponent<PickupItem>();
                        if (pickup != null) {
                            pickedObject = pickup;
                            pickup.player = this;
                            pickup.pickedUp = true;

                            Rigidbody pickedBody = pickedObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
                            pickedBody.useGravity = false;
                            pickedBody.velocity = Vector3.zero;
                        }
                    }
                }
                //throw the picked up item
                else {
                    releasePickup();
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1) && pickedObject != null) {
            Rigidbody pickedBody = pickedObject.transform.GetComponent<Rigidbody>();
            releasePickup();
            pickedBody.velocity = cameraTransform.forward * 10;
        }
        //resets wasClicked
        else {
            wasClicked = false;
        }

        //releases item if its too far away
        if(pickedObject != null)
        {
            if((pickedObject.transform.position - transform.position).magnitude > maxPickupDistance)
            {
                releasePickup();
            }
        }
    }

    //updates camera and pickup object
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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);

        //item following camera
        if (pickedObject != null) {

            //rotating with camera
            pickedObject.transform.rotation = cameraTransform.rotation;

            //getting target position (corrected for collisions)
            Vector3 origin = cameraTransform.position + new Vector3(0, pickedupObjectVerticalOriginOffset, 0);
            Vector3 targetPos = origin + cameraTransform.forward * pickupObjectDistance;
            Vector3 diff = (targetPos - pickedObject.transform.position);

            //moving the object
            Rigidbody pickedBody = pickedObject.GetComponent<Rigidbody>();
            Vector3 closestToCenter = pickedObject.GetComponent<Collider>().ClosestPoint(transform.position);
            Vector3 targetMovement = targetPos - pickedObject.transform.position;
            Vector3 closestDiff = (transform.position - (closestToCenter + targetMovement));
            if (diff.sqrMagnitude > 0.00001f && (Mathf.Abs(closestDiff.x) >= minPickupDistance || Mathf.Abs(closestDiff.z) >= minPickupDistance)) {
                pickedBody.velocity = (targetPos - pickedObject.transform.position).normalized * Mathf.Min(pickupMoveSpeed, diff.magnitude / Time.deltaTime);
            }
            else {
                pickedBody.velocity = Vector3.zero;
            }
        }
    }

    public void releasePickup() {
        Rigidbody pickedBody = pickedObject.GetComponent<Rigidbody>();
        pickedObject.GetComponent<PickupItem>().pickedUp = false;
        pickedBody.useGravity = true;
        pickedBody.velocity = Vector3.zero;
        pickedObject.player = null;
        pickedObject = null;
    }

}
