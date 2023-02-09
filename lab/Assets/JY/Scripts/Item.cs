using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected GameObject curItem;
    protected int useCount;       //  ��밡��Ƚ��
    [SerializeField]
    protected float delayTime;    //  ��� �� ���� ��밡�ɽð������� ������
    protected float curDelayTime=0;    //  ���� ������ �ð�
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
