using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBeltIn : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform m_SpawnPos;
    [SerializeField] private float m_BeltSpeed = 1;

    public void SpawnNewInstance(GameObject gm)
    {
        Instantiate(gm, m_SpawnPos.position, Quaternion.identity);
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, m_Target.position, m_BeltSpeed * Time.deltaTime);
    }
}
