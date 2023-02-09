using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_FallFloor : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 firstPos;
    [SerializeField] float WaitTime;//���������� ��ٷ��ִ� �ð�
    [SerializeField] float fallSpeed;
    bool isFall;
    [SerializeField] float timer;
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
            timer = 0;
            print("�ȶ�������");
        }
      
    }

    void Timer()
    {
        timer += Time.deltaTime;
        if(timer>=WaitTime)
        {
            IsFall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && isFall == true)
        {
            IsFall();
            transform.position = firstPos;
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if ((collision.transform.position.y > transform.position.y))
        {
            if (collision.transform.CompareTag("Player") && isFall == false)
            {
                print("�÷��̾ ������ ����");
                Timer();


            }

        }
    }



}