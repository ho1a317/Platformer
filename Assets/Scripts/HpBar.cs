using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider bar;
    public Gradient gradient;
    public Image fill;

    public void SetHp(float maxValue,float nowValue)
    {
        float norm = nowValue / maxValue;

        bar.value = norm;
        fill.color = gradient.Evaluate(norm);
    }
}
