using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixable : PickupItem {

    public float snapDistance = 0.1f;

    public GameObject toBreak;
    public GameObject pivotList;
    public int brokenPartCount;
    private List<bool> taken = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        List<int> keepList = new List<int>();
        for (int i = 0; i < brokenPartCount; i++)
        {
            int childCount = toBreak.transform.childCount;
            int index = Random.Range(0, childCount);
            Destroy(toBreak.transform.GetChild(index).gameObject);
            keepList.Add(index);
        }
        if (pivotList.transform.childCount > 0)
        {
            for (int k = pivotList.transform.childCount - 1; k >= 0; k--)
            {
                if (!keepList.Contains(k))
                {
                    Destroy(pivotList.transform.GetChild(k).gameObject);
                }
            }
        }
        for (int k = 0; k < pivotList.transform.childCount; k++)
        {
            taken.Add(false);
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("update");
        if (pickedUp)
        {
            Debug.Log("picked up");
        }
        if(editVolume != null)
        {
            Debug.Log("is in volume");
        }
        if(pickedUp && editVolume != null) {
            Debug.Log(editVolume.parts.Count);
            foreach(Part part in editVolume.parts)
            {
                checkPivotForPart(part);
            }
        }
    }

    public void checkPivotForPart(Part a_part) {
        Debug.Log("checking for part pivot");
        float closestSqrLength = float.PositiveInfinity;
        int closest = -1;
        for(int k = 0; k < pivotList.transform.childCount; k++) {
            if (!taken[k]) {
                Vector3 pivotWorld = pivotList.transform.GetChild(k).transform.position;
                Vector3 partWorld = a_part.transform.position;
                Vector3 diff = (pivotWorld - partWorld);
                float currentSqr = diff.sqrMagnitude;
                if (currentSqr < closestSqrLength) {
                    closestSqrLength = currentSqr;
                    closest = k;
                }
            }
        }
        if(closest > -1) {
            if(closestSqrLength <= snapDistance * snapDistance) {
                attach(a_part, closest);
            }
        }
    }

    public bool isFixed() {
        foreach(bool take in taken) {
            if (!take) {
                return false;
            }
        }
        return true;
    }

    private void attach(Part a_part, int a_pivotIndex) {
        Debug.Log("attaching");
        a_part.transform.parent = transform;
        a_part.transform.position = pivotList.transform.GetChild(a_pivotIndex).transform.position;
        taken[a_pivotIndex] = true;
        releaseFromPlayer();
        editVolume.parts.Remove(a_part);
        a_part.releaseFromPlayer();
        Destroy(a_part.GetComponent<Part>());
        Destroy(a_part.GetComponent<Rigidbody>());
    }
}
