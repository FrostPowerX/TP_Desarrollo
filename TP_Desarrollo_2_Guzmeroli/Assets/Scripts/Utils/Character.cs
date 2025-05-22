using UnityEngine;

/// <summary>
/// Move the character with a controller
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected Transform point;

    [SerializeField] protected string notWalkeableOn;

    [SerializeField] protected float force;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationVel;
    [SerializeField] protected float maxDistanceFloor;

    [SerializeField] protected bool onFloor;


    bool isForceActive;

    protected Vector3 direction;

    protected Quaternion rotation;

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

    void InstantForce(Vector3 dir)
    {
        rb.AddForce(dir * force, ForceMode.Impulse);
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

    protected void RotateTo(Vector3 dir)
    {
        Quaternion rotationPrev = Quaternion.LookRotation(dir);
        rotation = Quaternion.RotateTowards(rb.rotation, rotationPrev, rotationVel * Time.deltaTime);
    }

    protected void ConstantForceRequest()
    {
        isForceActive = true;
    }

    protected void InstantForceRequest(Vector3 dir)
    {
        InstantForce(dir);
    }

    protected void CancelForce()
    {
        isForceActive = false;
    }
}
