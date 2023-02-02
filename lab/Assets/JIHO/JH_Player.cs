using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Player : MonoBehaviour
{
    Rigidbody rb;

    //Move()
    public float moveSpeed;

    //Jump()
    public float jumpPower;
    public float JumpCheckLen;

    Vector3 lookVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");        // 가로축
        float v = Input.GetAxis("Vertical");          // 세로축

        if(h!=0||v!=0)
        {
            lookVector = new Vector3(h,v);
        }

        // Point 2.
        transform.position += new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
       if(rb.velocity.y<0.2&&rb.velocity.y>-0.2f)
        {

            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            Debug.DrawRay(transform.position, Vector3.down * JumpCheckLen, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, JumpCheckLen, layerMask))
            {

                if (Input.GetKeyDown(KeyCode.C))
                {
                    rb.AddForce(Vector3.up * jumpPower);
                }

            }

        }





    }
}