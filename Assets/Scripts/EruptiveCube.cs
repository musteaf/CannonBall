using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EruptiveCube : MonoBehaviour
{
    [SerializeField]
    private float radius = 20.0F;
    [SerializeField]
    private float power = 1000.0F;

    private void OnCollisionEnter(Collision other)
    {
        BallMovement ballMovement = other.gameObject.GetComponent<BallMovement>();
        if (ballMovement)
        {
            Explode();
            if(!ballMovement.isSlowDowned())
                GameManager.instance.TimeManager.SlowDownTime();
        }
    }

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }
}
