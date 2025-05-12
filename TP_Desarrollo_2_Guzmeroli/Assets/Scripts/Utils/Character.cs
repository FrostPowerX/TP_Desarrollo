using UnityEngine;

/// <summary>
/// Move the character with a controller
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform point;

    [SerializeField] bool onFloor;
    [SerializeField] float maxDistanceFloor;

    [SerializeField] string notWalkeableOn;

    [SerializeField] protected float force;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float speed;

    bool isForceActive;

    protected Vector3 direction;

    public bool OnFloor { get { return onFloor; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ConstantForce();
        FloorDetector();
    }

    void OnDrawGizmos()
    {
        if (!point)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(point.position, point.position + new Vector3(0, maxDistanceFloor * -1, 0));
    }

    void ConstantForce()
    {
        if (!isForceActive)
            return;

        float percentForce = rb.linearVelocity.magnitude / speed;
        float percentActualForce = Mathf.Clamp01(1f - percentForce);

        rb.AddForce(direction * force * percentActualForce, ForceMode.Force);
    }

    void InstantForce()
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    

    void FloorDetector()
    {
        RaycastHit hit;

        if (Physics.Raycast(point.position, Vector3.down, out hit, maxDistanceFloor))
        {
            if(hit.transform && !hit.transform.CompareTag(notWalkeableOn))
            {
                onFloor = true;
            }
        }
        else
                onFloor = false;
    }


    protected void ConstantForceRequest()
    {
        isForceActive = true;
    }

    protected void InstantForceRequest()
    {
        InstantForce();
    }

    protected void CancelForce()
    {
        isForceActive = false;
    }
}
