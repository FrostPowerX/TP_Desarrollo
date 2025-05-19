using UnityEngine;

public class Enemy : Character
{
    [SerializeField] GameObject target;

    void Update()
    {
        if (target)
        {
            ActualizeDirection();
        }
    }

    void ActualizeDirection()
    {
        direction = Vector3.Normalize(target.transform.position - transform.position);
        transform.LookAt(direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            ConstantForceRequest();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(target == other.transform)
        {
            target = null;
            CancelForce();
        }
    }
}
