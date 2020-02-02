using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportBoxBehaviour : MonoBehaviour
{

    public List<GameObject> instantiatableMaterials;

    private List<GameObject> instantiatedMaterials = new List<GameObject>();
    public int amountOfObjectsPerBox;

    void Start()
    {
        for(int i =0; i<amountOfObjectsPerBox; ++i)
        {
           int randomMaterialToAdd = Random.Range(0,15);
            randomMaterialToAdd %= instantiatableMaterials.Count;
            instantiatedMaterials.Add(Instantiate(instantiatableMaterials[randomMaterialToAdd],transform));
        }

        for (int i = 0; i < amountOfObjectsPerBox; ++i)
        {
            (instantiatedMaterials[i].GetComponent<Collider>()).enabled = false;
            Destroy(instantiatedMaterials[i].GetComponent<Rigidbody>());
            (instantiatedMaterials[i].transform).SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
