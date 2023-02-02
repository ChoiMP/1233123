using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 firstPos;
    public float WaitTime;//떨어지기전 기다려주는 시간
    public float fallSpeed;
    bool isFall;

    private void Start()
    {
        firstPos=transform.position;
    }

    private void Update()
    {
        Fall();
    }

    void Fall()
    {
        if(isFall==true)
        {
            transform.Translate(Vector3.down * fallSpeed*Time.deltaTime);
        }
    }

    void IsFall()
    {
        if(isFall==false)
        {
            isFall = true;
            print("떨어진당");
        }
        else
        {
            isFall = false;
            print("안떨어진당");
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.transform.position.y > transform.position.y))
        {
            if (collision.transform.CompareTag("Player"))
            {
                print("플레이어가 위에서 밟음");
                Invoke("IsFall", WaitTime);


            }

        }
       
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground")&&isFall==true)
        {
            IsFall();
            transform.position = firstPos;
        }
    }
}