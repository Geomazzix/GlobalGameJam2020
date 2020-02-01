using UnityEngine;

public class TransportBeltOut : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_BeltSpeed = 1;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, m_Target.position, m_BeltSpeed * Time.deltaTime);

        if (Mathf.Abs((other.transform.position - m_Target.position).magnitude) <= 0.5f)
            Destroy(other.gameObject);
    }
}
