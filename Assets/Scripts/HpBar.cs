using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHp(int hp)
    {
        slider.maxValue = hp;
        slider.value = slider.maxValue;
    }

    public void SetHp(int hp)
    {
        slider.value = hp;
    }
}
