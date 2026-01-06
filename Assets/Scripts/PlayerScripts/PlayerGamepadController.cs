using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGamepadController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 movement;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("LeftStickHorizontal");
        float moveZ = Input.GetAxisRaw("LeftStickVertical");

        if (Mathf.Abs(moveX) < 0.2f) moveX = 0f;
        if (Mathf.Abs(moveZ) < 0.2f) moveZ = 0f;

        movement.x = moveX;
        movement.z = moveZ;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector3 GetMovementDirection()
    {
        return movement.normalized;
    }
}
