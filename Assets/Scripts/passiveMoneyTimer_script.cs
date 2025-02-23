using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passiveMoneyTimer_script : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdatePassiveMoneyTimeBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
