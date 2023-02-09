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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
        Check();
    }

    void Spin()
    {
        Debug.DrawRay(transform.position, Vector3.right * 10, Color.blue);

        if (isEnter == true)
        {
            if ((int)transform.eulerAngles.z%90 == 0)
            {
                turn *= -1;
            }

            if(SpinStop==false)
            {
                transform.Rotate(new Vector3(0, 0, turn * spinSpeed) * Time.deltaTime);

            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                SpinStop = true;
               
            }

            if(SpinStop==true)
            {
                //print(new Vector3(Mathf.Cos(transform.eulerAngles.z), Mathf.Cos(transform.eulerAngles.z), 0)+ "transform.eulerAngles.z" + transform.eulerAngles.z);
                user.transform.Translate(new Vector3(( Mathf.Cos(90 + transform.eulerAngles.z))/180, -Mathf.Cos(transform.eulerAngles.z) / 180, 0) * launchPower * Time.deltaTime);
               // user.GetComponent<Rigidbody>().AddForce()
            }
        }
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
                print("대포 탑승");
                user = hit.transform.gameObject;
                isEnter = true;
                transform.GetComponent<CapsuleCollider>().isTrigger = true;
                user.transform.position = transform.position;
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && isEnter ==false)
        {
           

        }
    }

}
