using System;
using UnityEngine;

[Serializable]
public class ForceRequest
{
    public Vector3 direction;

    public float force;
    public float speed;

    public ForceRequest()
    {
        direction = Vector3.zero;

        this.force = 0;
        this.speed = 0;
    }

    public ForceRequest(Vector3 dir, float force, float speed)
    {
        direction = dir;

        this.force = force;
        this.speed = speed;
    }
}