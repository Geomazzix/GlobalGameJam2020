using UnityEngine;

/// <summary>
/// 
/// </summary>
public class InsertBeltDelivery : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private GameObject m_MaterialBoxPrefab;
    [SerializeField] private GameObject[] m_Toys;

    private void Start()
    {
        SpawnNewBrokenToy();
        Invoke("SpawnNewItemCrate", 2);
    }

    public void SpawnNewBrokenToy()
    {
        Instantiate(m_Toys[Random.Range(0, m_Toys.Length - 1)], m_SpawnPoint.position, Quaternion.identity);
    }

    public void SpawnNewItemCrate()
    {
        Instantiate(m_MaterialBoxPrefab, m_SpawnPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        SpawnNewItemCrate();
        SpawnNewBrokenToy();
    }
}
