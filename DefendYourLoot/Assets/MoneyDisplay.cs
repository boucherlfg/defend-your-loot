using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMoneyChanged : BaseEvent<int> {}
public class MoneyDisplay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TMP_Text>().text = LootScript.money + " $";
    }
}
