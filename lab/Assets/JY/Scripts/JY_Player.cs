using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JY_Player : MonoBehaviour
{
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
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void Update()
    {
        Jump();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");        // 가로축
        float v = Input.GetAxis("Vertical");          // 세로축
        Move(h);
        Sit(v);
    }
    private void Move(float h)
    {
        transform.position += new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if (rb.velocity.y < 0.2 && rb.velocity.y > -0.2f)
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
    public void GetCoin(float coin=1)
    {
        curCoin += coin;
        curCoinText.text = "curCoin : " + curCoin;
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
        if (collision.gameObject.tag=="Enemy")
        { 
                StartCoroutine(Hit());
                Destroy(collision.gameObject);
                return;
        }

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
