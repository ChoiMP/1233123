using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Cannon : MonoBehaviour
{
    RaycastHit hit;


    [SerializeField] GameObject user;
    [SerializeField] float spinSpeed;
    [SerializeField] float launchPower;
    [SerializeField] bool isEnter = false;
    [SerializeField] int turn=1;
    [SerializeField] bool SpinStop=false;
    // Start is called before the first frame update

    Quaternion fristRotate;



    private void Start()
    {
        fristRotate = transform.rotation;
    }

    void Update()
    {
        Spin();
        Check();
    }


    void Init()
    {
        transform.rotation = fristRotate;
        isEnter = false;
        turn = 1;
        SpinStop = false;
        GetComponent<CapsuleCollider>().isTrigger = false;
    }

    void Spin()
    {
        Debug.DrawRay(transform.position, Vector3.right * 10, Color.blue);

        if (isEnter == true)
        {
            if(spinSpeed>0)
            {
                if (transform.GetChild(0).transform.position.x <= transform.position.x || transform.GetChild(0).transform.position.y <= transform.position.y)
                {
                    Debug.Log(transform.rotation);
                    turn *= -1;
                }
            }
            else
            {

                if (transform.GetChild(0).transform.position.x >= transform.position.x || transform.GetChild(0).transform.position.y <= transform.position.y)
                {
                    Debug.Log(transform.rotation);
                    turn *= -1;
                }
            }
           

            if(SpinStop==false)
            {
                transform.Rotate(new Vector3(0, 0, turn * spinSpeed));
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                SpinStop = true;
                Fire();
            }

        }
    }

    void Fire()
    {
        user.GetComponent<Rigidbody>().velocity = Vector3.zero;
        user.GetComponent<Rigidbody>().useGravity = true;
        Vector3 vec = transform.GetChild(0).transform.position - transform.position;
        user.GetComponent<Rigidbody>().AddForce(new Vector3(vec.x, vec.y, 0) * launchPower);

         Invoke("Init",0.2f);
    }
   void Check()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        Debug.DrawRay(transform.position, Vector3.up * 10, Color.red);

        if (Physics.Raycast(transform.position, Vector3.up,  out hit, 10, layerMask))
        {
            print("상호작용 누르면 대포 탈수 있음");
            if (Input.GetKeyDown(KeyCode.X))
            {
                user = hit.transform.gameObject;
                isEnter = true;
                transform.GetComponent<CapsuleCollider>().isTrigger = true;
                user.GetComponent<Rigidbody>().useGravity = false;
                user.transform.position = transform.position;
                user.GetComponent<Rigidbody>().velocity = Vector3.zero;
                user.GetComponent<JH_Player>().myState = State.Freeze;
            }

        }


    }


}
