using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 firstPos;
    public float WaitTime;//���������� ��ٷ��ִ� �ð�
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
            print("��������");
        }
        else
        {
            isFall = false;
            print("�ȶ�������");
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.transform.position.y > transform.position.y))
        {
            if (collision.transform.CompareTag("Player"))
            {
                print("�÷��̾ ������ ����");
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