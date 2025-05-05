using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Move the character with a controller
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] ForceRequest constantForceRequest;
    [SerializeField] ForceRequest instantForceRequest;
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform point;
    [SerializeField] GameObject head;

    [SerializeField] bool onFloor;
    [SerializeField] float maxDistanceFloor;

    [SerializeField] string notJumpOn;

    Vector2 rotation;
    Vector2 sensivility;

    public bool OnFloor { get { return onFloor; } }

    float xRotation = 0f;

    bool isForceActive;
    bool isHeadActive;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        constantForceRequest = new ForceRequest();
        instantForceRequest = new ForceRequest();
    }

    void FixedUpdate()
    {
        ConstantForce();
        FloorDetector();
        MoveHead();
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

        float percentForce = rb.linearVelocity.magnitude / constantForceRequest.speed;
        float percentActualForce = Mathf.Clamp01(1f - percentForce);

        rb.AddForce(constantForceRequest.direction * constantForceRequest.force * percentActualForce, ForceMode.Force);
    }

    void InstantForce()
    {
        rb.AddForce(instantForceRequest.direction * instantForceRequest.force, ForceMode.Impulse);
        instantForceRequest = null;
    }

    void MoveHead()
    {
        if (!isHeadActive)
            return;

        float Xmouse = rotation.x;
        float mouseY = rotation.y;

        //if (invert) mouseY *= -1;

        xRotation -= (mouseY * Time.deltaTime) * sensivility.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        head.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (Xmouse * Time.deltaTime) * sensivility.x);
    }

    void FloorDetector()
    {
        RaycastHit hit;

        if (Physics.Raycast(point.position, Vector3.down, out hit, maxDistanceFloor))
        {
            if(hit.transform && !hit.transform.CompareTag(notJumpOn))
            {
                onFloor = true;
            }
        }
        else
                onFloor = false;
    }


    public void ConstantForceRequest(ForceRequest forceRequest)
    {
        constantForceRequest = forceRequest;
        isForceActive = true;
    }

    public void InstantForceRequest(ForceRequest forceRequest)
    {
        instantForceRequest = forceRequest;
        InstantForce();
    }

    public void MoveHeadRequest(Vector2 rotation, Vector2 sensivility, GameObject head)
    {
        this.head = head;
        this.rotation = rotation;
        this.sensivility = sensivility;

        isHeadActive = true;
        MoveHead();
    }

    public void CancelForce()
    {
        isForceActive = false;
    }

    public void CancelMoveHead()
    {
        isHeadActive = false;
    }
}
