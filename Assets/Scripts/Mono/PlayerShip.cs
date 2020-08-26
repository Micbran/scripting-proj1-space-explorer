using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{

    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float turnSpeed = 3f;

    Rigidbody rb = null;
    Transform t = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        t = transform;
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    private void MoveShip()
    {
        float moveMagnitude = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector3 moveVector = t.forward * moveMagnitude;

        rb.AddForce(moveVector);
    }

    private void TurnShip()
    {
        float turnMagnitude = Input.GetAxisRaw("Horizontal") * turnSpeed;

        Quaternion turnOffset = Quaternion.Euler(0, turnMagnitude, 0);

        rb.MoveRotation(rb.rotation * turnOffset);
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
    }

}
