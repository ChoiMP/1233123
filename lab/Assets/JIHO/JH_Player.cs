using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Player : MonoBehaviour
{
    Rigidbody rb;

    //Move()
    [SerializeField] float moveSpeed;

    //Jump()
    [SerializeField] float jumpPower;
    public GameObject JumpCheck;
    [SerializeField] float JumpCheckLen;

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


         int layerMask = 1 << LayerMask.NameToLayer("Ground");
         Debug.DrawRay(transform.position, Vector3.down * JumpCheckLen, Color.red);
         if (Physics.Raycast(transform.position, Vector3.down, JumpCheckLen, layerMask))
         {
             print("점프 가능");
             if (Input.GetKeyDown(KeyCode.C))
             {
                 rb.AddForce(Vector3.up * jumpPower);
             }

         }

        //int layerMask = 1 << LayerMask.NameToLayer("Ground");
/*        if (Physics.BoxCast(JumpCheck.transform.position, new Vector3(1,0.2f,1), Vector3.down, out RaycastHit hit, transform.rotation, 0))
        {
            print("점프 가능");
            if (Input.GetKeyDown(KeyCode.C))
            {
                rb.AddForce(Vector3.up * jumpPower);
            }
        }*/

    }

/*    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(JumpCheck.transform.position, new Vector3(1, 0.2f, 1));


    }*/
}