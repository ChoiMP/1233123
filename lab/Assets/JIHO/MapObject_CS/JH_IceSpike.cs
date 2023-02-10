using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_IceSpike : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] bool isPlayer;
    [SerializeField] Vector3 firstPos;

    private void Start()
    {
        firstPos=transform.position;
    }
    private void FixedUpdate()
    {
        if(isPlayer==true)
        {
            transform.Translate(Vector3.down * Speed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        CheckPlayer();
    }
    void CheckPlayer()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        Debug.DrawRay(firstPos, Vector3.down * 100, Color.blue);

        if (Physics.Raycast(firstPos, Vector3.down, 100, layerMask))
        {
            isPlayer = true;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
            transform.GetComponent<MeshRenderer>().enabled = false;
            Invoke("Init", 0.5f);
        }
    }

    void Init()
    {
        print("오브젝트와 충돌");
        transform.GetComponent<BoxCollider>().isTrigger = false;
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.position = firstPos;
        isPlayer = false;
    }
}
