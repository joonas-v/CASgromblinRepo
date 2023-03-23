using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrMove : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask lmask;
    public bool isGrounded;
    public bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            //input
            float x = Input.GetAxisRaw("Horizontal") * speed;
            float y = Input.GetAxisRaw("Vertical") * speed;


            //isground
            isGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, lmask);

            //jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }

            //movement
            Vector3 movePos = transform.right * x + transform.forward * y;
            Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);
            rb.velocity = newMovePos;
        }
    }


}
