using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected GameObject curItem;
    protected int useCount;       //  사용가능횟수
    [SerializeField]
    protected float delayTime;    //  사용 후 다음 사용가능시간까지의 딜레이
    protected float curDelayTime=0;    //  현재 딜레이 시간
    protected Vector3 lookVector;

    private void Awake()
    {
        lookVector = GetComponentInParent<JY_Player>().lookVector;
    }
    public virtual void UseItem(Vector3 fireVector,Vector3 lookVector)
    {
        if (curDelayTime <= 0)
        {
            Fn(fireVector,lookVector);
            useCount--;
            curDelayTime = delayTime;
        }
    }
    public virtual void Fn(Vector3 fireVector, Vector3 lookVector)
    {
    }
    private void Update()
    {
        if (useCount <= 0)
        {
            Destroy(this.gameObject);
        }
        curDelayTime -= Time.deltaTime;
    }
}
