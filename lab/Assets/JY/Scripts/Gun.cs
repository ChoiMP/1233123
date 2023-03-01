using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    [SerializeField]
    private GameObject bullet;
    public override float Init()
    {
        delayTime = 3;
        useCount = 3;
        return delayTime;
    }
    public override void Fn(Vector3 fireVector, Vector3 lookVector)
    {
        Debug.Log(this.gameObject.name + "»ç¿ëµÊ");
        GameObject b= Instantiate(bullet, fireVector + lookVector, Quaternion.Euler(lookVector));
        b.GetComponent<bullet>().v = lookVector;
        Debug.Log(1123);
    }
}

