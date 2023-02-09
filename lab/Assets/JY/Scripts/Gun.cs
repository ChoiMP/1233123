using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    [SerializeField]
    private GameObject bullet;
    private void Awake()
    {
        delayTime = 3;
        useCount = 3;
    }
    public override void Fn(Vector3 fireVector, Vector3 lookVector)
    {
        print(this.gameObject.name + "»ç¿ëµÊ");
        Instantiate(bullet, fireVector + lookVector, Quaternion.Euler(lookVector));
    }
}
