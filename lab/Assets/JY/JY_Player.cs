using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JY_Player : MonoBehaviour
{
    [SerializeField]
    float v;
    [SerializeField]
    float h;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        //Sit(v);
        
    }
    void InputController()    //    전체 입력 관리자?
    {

    }
    private void Sit(float v)
    {
        if (v == -1)
        {
            Debug.Log("Sit");
            boxCollider.center = new Vector3(0, -0.5f, 0);
            boxCollider.size = new Vector3(1, 1, 1);

        }
        else
        {
            Debug.Log("Stand");
            boxCollider.center = new Vector3(0, 0, 0);
            boxCollider.size = new Vector3(1, 2, 1);
        }
    }
    public void Hit()   //  피격시 오브젝트 삭제 + 조작 불가 --> 리스폰
    {
        Debug.Log("Hit");
    }
}
