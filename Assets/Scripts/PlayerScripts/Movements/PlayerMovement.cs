using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private float rotationSpeed = 200.0f;
    private Animator animator;
    private float movementInput;
    private float rotationInput;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movementInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
        Move();
        Turn();
    }

    void Move()
    {
        transform.Translate(Vector3.forward * movementInput * speed * Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(movementInput));
    }

    void Turn()
    {
        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }
}