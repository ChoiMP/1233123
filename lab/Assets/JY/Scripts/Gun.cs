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
    public override void Fn()
    {
        print(this.gameObject.name + "»ç¿ëµÊ");
        Instantiate(bullet, transform.position + lookVector, Quaternion.Euler(lookVector));
    }
}
