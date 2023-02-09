using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JY_Player : MonoBehaviour
{
    bool isTrigger;
    string collision;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    Rigidbody rb;

    public  Text curCoinText;
    private float curCoin;
    //Move()
    public float moveSpeed;

    //Jump()
    public float jumpPower;
    public float JumpCheckLen;

    Vector3 startPos;

    Vector3 lookVector;
    enum State
    {
        Idle,Rope,iced
    }
    State state = State.Idle;
    void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        rb = GetComponentInChildren<Rigidbody>();
        startPos = transform.position;
        
    }

    void Update()
    {
        InputManager();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        
    }
    private void InputManager()
    {
        if (state == State.iced) return;
        float v = Input.GetAxis("Vertical");          // 세로축
        if (state == State.Rope)
        {
            Move(0,v);
        }
        float h = Input.GetAxis("Horizontal");        // 가로축
        if (state == State.Idle)
        {
            Move(h);
            Sit(v);
            if (Input.GetKey(KeyCode.Z)) { }//UseItem();
            if (Input.GetKeyDown(KeyCode.X) && isTrigger)
            {
                switch (collision)
                {
                    case "Rope":
                        print("로프 진입");
                        return;
                    case "Cannon":
                        return;
                    case "Portal":
                        return;
                }

            }


            if (Input.GetKey(KeyCode.C)) Jump();
        }
        
    }
    private void Move(float h, float v = 0)
    {
        transform.position += new Vector3(h, v, 0) * moveSpeed * Time.fixedDeltaTime;
    }

    private void Jump()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
         Debug.DrawRay(transform.position, Vector3.down * JumpCheckLen, Color.red);
         if (Physics.Raycast(transform.position, Vector3.down, JumpCheckLen, layerMask))
         {
            rb.AddForce(Vector3.up * jumpPower* Time.fixedDeltaTime);
         }
    }
    private void Sit(float v)
    {
        if (v < 0)
        {
            //Debug.Log("Sit");
            boxCollider.center = new Vector3(0, -0.5f, 0);
            boxCollider.size = new Vector3(1, 1, 1);

        }
        else
        {
            //Debug.Log("Stand");
            boxCollider.center = new Vector3(0, 0, 0);
            boxCollider.size = new Vector3(1, 2, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Coin")
        {
            if (collision.gameObject.GetComponent<Coin>().isBig)
            {
                GetCoin(5);
            }
            else
            {
                GetCoin();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Hit());
            Destroy(collision.gameObject);
            return;
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        this.collision = other.gameObject.name;
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        this.collision = null;
    }
    private void OnTriggerStay(Collider collision)
    {
        
    }

    public void GetCoin(float coin=1)
    {
        curCoin += coin;
        curCoinText.text = "curCoin : " + curCoin;
    }
    public IEnumerator Hit()//  피격시 오브젝트 삭제 + 조작 불가 --> 리스폰
    {
        Debug.Log("Hit");
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3f);

        Debug.Log("리스폰");
        GetComponent<MeshRenderer>().enabled = true;
        transform.position = startPos;
    }
}
