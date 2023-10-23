using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(moveHorizontal == 0 && moveVertical == 0) 
        {
            // キーが押されていない場合、速度を0にする
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        } 
        else 
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
    }
}