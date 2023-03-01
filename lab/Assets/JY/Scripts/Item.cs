using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item :MonoBehaviour
{
    [SerializeField]
    protected GameObject curItem;
    [SerializeField]
    protected int useCount=3;       //  사용가능횟수
    [SerializeField]
    protected float delayTime;    //  사용 후 다음 사용가능시간까지의 딜레이   //  현재 딜레이 시간
    protected Vector3 lookVector;

    public virtual float Init()
    {
        return delayTime;
    }
    public virtual bool UseItem(Vector3 fireVector, Vector3 lookVector)
    {
        if (useCount <= 0) return false;
        Fn(fireVector, lookVector);
        useCount--;
        return true;
    }
    public virtual void Fn(Vector3 fireVector, Vector3 lookVector)
    {
    }
    void Update()
    {
    }
}
