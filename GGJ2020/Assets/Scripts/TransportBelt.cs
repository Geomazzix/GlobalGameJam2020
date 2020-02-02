using UnityEngine;

public class TransportBelt : MonoBehaviour
{
    enum ETransportBeltType
    {
        IN,
        OUT
    }

    [SerializeField] ETransportBeltType m_Type = ETransportBeltType.IN;
    [SerializeField] private Vector3 m_Direction;
    [SerializeField] private float m_BeltSpeed;
    private const float MAX_VELOCITY = 50;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null) return;

        if (m_Type == ETransportBeltType.OUT)
        {
            if (other.GetComponent<Fixable>() != null && !other.GetComponent<Fixable>().isFixed())
                return;
        }


        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 velocity = new Vector3(
            Mathf.Clamp(m_Direction.x * m_BeltSpeed * Time.fixedDeltaTime, -MAX_VELOCITY, MAX_VELOCITY),
            rb.velocity.y,
            Mathf.Clamp(m_Direction.z * m_BeltSpeed * Time.fixedDeltaTime, -MAX_VELOCITY, MAX_VELOCITY));
        rb.velocity = velocity;

    }
}
