using UnityEngine;

/// <summary>
/// 
/// </summary>
public class InsertBeltDelivery : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private GameObject m_MaterialBoxPrefab;
    [SerializeField] private GameObject[] m_Toys;
    public AudioClip despawnSound;

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
        if(other.gameObject.GetComponent<Fixable>() != null)
        {
            if (other.gameObject.GetComponent<Fixable>().isFixed())
            {
                SpawnNewBrokenToy();
                Invoke("SpawnNewItemCrate", 2);
                Destroy(other.gameObject);
                AudioSource playSource = FindObjectOfType<SoundController>().PlayAudioSource(despawnSound, transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
