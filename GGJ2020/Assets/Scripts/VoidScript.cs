using UnityEngine;

/// <summary>
/// 
/// </summary>
public class VoidScript : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint;


    private void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.SetPositionAndRotation(m_SpawnPoint.position,m_SpawnPoint.rotation);
    }
}
