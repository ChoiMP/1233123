using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_IceSpike : MonoBehaviour
{
    [SerializeField] float Speed;
    bool isPlayer;
    Vector3 firstPos;

    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {
        if(isPlayer==true)
        {
            transform.Translate(Vector3.down * Speed * Time.fixedDeltaTime);
        }
    }

    void CheckPlayer()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        if (Physics.Raycast(transform.position, Vector3.down, 100, layerMask))
        {
            isPlayer = true;

        }
    }



}
