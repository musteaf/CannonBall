using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float flightDuration;
    private Rigidbody rigidbody;
    private Vector3 startPosition;
    private bool slowDowned = false;

    void Start()
    {
        Physics.gravity = Vector3.up * gravity;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        startPosition = transform.position;
    }

    public void Launch(Vector3 initialVelocity)
    {
        rigidbody.velocity = initialVelocity;
    }

    public Vector3 CalculateInitialVelocity(float angleY, float angleZ, Vector3 target)
    {
        Vector2 vector2 = new Vector2(target.x - startPosition.x, target.z - startPosition.z);
        vector2 = vector2 * Mathf.Tan(angleZ * Mathf.Deg2Rad);
        Vector2 dot = new Vector2(vector2.y, -vector2.x);
        float xPosition = ((target.x - startPosition.x) + (dot.x)) / flightDuration;
        float yPosition = ((angleY) - (0.5f * gravity * Mathf.Pow(flightDuration, 2))) / flightDuration;
        float zPosition = ((target.z - startPosition.z) + (dot.y)) / flightDuration;
        return new Vector3(xPosition, yPosition, zPosition);
    }

    public float Gravity
    {
        get => gravity;
        set => gravity = value;
    }

    public float FlightDuration
    {
        get => flightDuration;
        set => flightDuration = value;
    }

    public Vector3 StartPosition
    {
        get => startPosition;
        set => startPosition = value;
    }

    public bool isSlowDowned()
    {
        if (!slowDowned)
        {
            slowDowned = true;
            return false;
        }
        return true;
    }
}