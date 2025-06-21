using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class testCharData
{
    public string charName;
    public int charPassiveMoney;
    public float passiveMoneyTimeCount;

    public testCharData(testA_script testA)
    {
        charName = testA.charName;
        charPassiveMoney = testA.charPassiveMoney;
        passiveMoneyTimeCount = testA.passiveMoneyTimeCount;
    }
}
