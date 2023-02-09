using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JY_Player : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCollider;
    Vector3 startPos;
    public Item item;
    public Text curCoinText;
    private float curCoin;
    //public enum State { Idle, Walk, Freeze, rope, Dead };
    [SerializeField] private State state = State.Idle;
    public State myState
    {
        get { return state; }
        set { state = value; }
    }
    //Move()
    [SerializeField] float moveSpeed;

    //Jump()
    [SerializeField] float jumpPower;
    public GameObject JumpCheck;
    [SerializeField] float JumpCheckLen;

    public Vector3 lookVector;

    float h;
    float v;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Jump();
        InputKey();
        if (Input.GetKeyDown(KeyCode.Z)) this.UseItem();
    }

    private void FixedUpdate()
    {

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
            //print("점프 가능");
            if (Input.GetKeyDown(KeyCode.C))
            {
                rb.AddForce(Vector3.up * jumpPower);
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
    void InputKey()
    {
        if (state == State.Freeze)
        {
            return;
        }

        h = Input.GetAxis("Horizontal");        // 가로축
        v = Input.GetAxis("Vertical");          // 세로축

        if (h != 0 || v != 0)
        {
            lookVector = new Vector3(h, v).normalized*1.5f;
        }

        if (state == State.Idle || state == State.Walk)
        {
            Move(h);
        }
        else if (state == State.rope)
        {
            Move(0, v);
        }

    }

    private void UseItem()
    {
        // GetComponentInChildren<Item>();
        item.UseItem(transform.position,lookVector);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Freeze)
        {
            state = State.Idle;
        }
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
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Hit());
            Destroy(collision.gameObject);
            return;
        }
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
