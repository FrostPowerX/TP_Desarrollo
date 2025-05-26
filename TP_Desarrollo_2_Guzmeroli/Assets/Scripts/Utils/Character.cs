using UnityEngine;

/// <summary>
/// Move the character with a controller
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected Transform floorDetectorPoint;

    [SerializeField] protected string notWalkeableOn;

    [SerializeField] protected float force;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationVel;
    [SerializeField] protected float maxDistanceFloor;

    [SerializeField] protected bool onFloor;


    [SerializeField] bool isForceActive;

    protected Vector3 direction;

    Quaternion rotation;

    bool isRotating;
    public bool OnFloor { get { return onFloor; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ConstantForce();
        FloorDetector();
        Rotate();
    }

    void OnDrawGizmos()
    {
        if (!floorDetectorPoint)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(floorDetectorPoint.position, floorDetectorPoint.position + new Vector3(0, maxDistanceFloor * -1, 0));
    }


    void Rotate()
    {
        if (direction != Vector3.zero && isRotating)
        {
            rotation = rotation.normalized;
            rb.MoveRotation(rotation);
            isRotating = false;
        }
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
        rb.AddForce(dir * jumpForce, ForceMode.Impulse);
    }

    void FloorDetector()
    {
        RaycastHit hit;

        if (Physics.Raycast(floorDetectorPoint.position, Vector3.down, out hit, maxDistanceFloor))
        {
            if (hit.transform && !hit.transform.CompareTag(notWalkeableOn))
            {
                onFloor = true;
            }
        }
        else
            onFloor = false;
    }

    protected void RotateTo(Vector3 dir)
    {
        dir.y = 0;

        Quaternion rotationPrev = Quaternion.LookRotation(dir);
        rotation = Quaternion.RotateTowards(rb.rotation, rotationPrev, rotationVel * Time.deltaTime);

        isRotating = true;
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
