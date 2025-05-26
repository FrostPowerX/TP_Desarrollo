using UnityEngine;

enum MovementType
{
    None,
    Seek,
    Patrol
}

public class Enemy : Character
{
    [SerializeField] GameObject target;

    [SerializeField] Vector3 firstPos;
    [SerializeField] Vector3 secondPos;

    [SerializeField] MovementType moveType;

    [Range(1, 2)]
    [SerializeField] int goPos;

    [SerializeField] bool canTarget;

    HealthSystem health;

    private void Start()
    {
        health = GetComponent<HealthSystem>();

        if (health)
            health.OnDeath += Kill;
    }

    void Update()
    {
        if (target)
            ActualizeDirectionAndRotation();
        else
            switch (moveType)
            {
                case MovementType.Seek:
                    SeekMode();
                    break;

                case MovementType.Patrol:
                    PatrolMode();
                    break;

                case MovementType.None:
                    CancelForce();
                    break;
            }

        if (direction != Vector3.zero)
            RotateTo(direction);
    }

    void SeekMode()
    {

    }

    void PatrolMode()
    {
        ConstantForceRequest();

        if (Vector3.Magnitude(firstPos - transform.position) < 1f && goPos == 1)
            goPos = 2;
        else if (Vector3.Magnitude(secondPos - transform.position) < 1f && goPos == 2)
            goPos = 1;

        if (goPos == 1)
            direction = Vector3.Normalize(firstPos - transform.position);
        else
            direction = Vector3.Normalize(secondPos - transform.position);
    }

    void ActualizeDirectionAndRotation()
    {
        direction = Vector3.Normalize(target.transform.position - transform.position);
        Vector3 dir = direction;

        RotateTo(dir);
    }

    void SetTarget(Collider other)
    {
        if (!canTarget)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            ConstantForceRequest();
        }
    }

    void Kill()
    {
        gameObject.SetActive(false);
    }

    [ContextMenu("Revive")]
    public void Revive()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        SetTarget(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (target == other.gameObject)
        {
            target = null;
            CancelForce();
        }
    }
}
