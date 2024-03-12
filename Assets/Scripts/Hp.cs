using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hp : MonoBehaviour
{
    public float maxHP;
    private float nowHP;

    public UnityEvent dcath;
    public UnityEvent <float,float> domaging;
    public UnityEvent <float,float> healing;

    private void Start()
    {
        RecoveruHP();
    }

    public void Domaging(float value)
    {
        nowHP -= value;

        if(nowHP <= 0)
        {
            nowHP = 0;
            dcath.Invoke();
        }

        domaging.Invoke(maxHP, nowHP);
    }

    public void Healing(float value)
    {
        nowHP += value;

        if (nowHP > maxHP)
        {
            nowHP = maxHP;
        }

        healing.Invoke(maxHP, nowHP);
    }

    public void RecoveruHP()
    {
        nowHP = maxHP;
        healing.Invoke(maxHP, nowHP);
    }
}
