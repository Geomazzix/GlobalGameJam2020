using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixable : PickupItem {

    public float snapDistance = 5f;
    public GameObject pivotPrefab;

    [SerializeField] private Transform[] pivots;
    private List<bool> taken = new List<bool>();

    // Start is called before the first frame update
    void Start() {
        for(int k = 0; k < pivots.Length; k++) {
            taken.Add(false);
            GameObject child = Instantiate(pivotPrefab, transform);
            child.transform.Translate(pivots[k].position);
        }
    }

    // Update is called once per frame
    void Update() {
        if(pickedUp && editVolume != null) {
            foreach(Part part in editVolume.parts)
            {
                checkPivotForPart(part);
            }
        }
    }

    public void checkPivotForPart(Part a_part) {
        float closestSqrLength = float.PositiveInfinity;
        int closest = -1;
        for(int k = 0; k < pivots.Length; k++) {
            if (!taken[k]) {
                float currentSqr = (pivots[k].position - a_part.transform.position).sqrMagnitude;
                if (currentSqr < closestSqrLength) {
                    closestSqrLength = currentSqr;
                    closest = k;
                }
            }
        }
        if(closest > -1) {
            if(closestSqrLength <= snapDistance * snapDistance)
            {
                attach(a_part, closest);
            }
        }
    }

    private void attach(Part a_part, int a_pivotIndex) {
        a_part.transform.parent = transform;
        a_part.transform.position = pivots[a_pivotIndex].position;
        taken[a_pivotIndex] = true;
        releaseFromPlayer();
        a_part.releaseFromPlayer();
        Destroy(a_part.GetComponent<Part>());
        Destroy(a_part.GetComponent<Rigidbody>());
    }
}
