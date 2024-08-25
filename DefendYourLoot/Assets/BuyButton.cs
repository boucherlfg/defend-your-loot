using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public GameObject whatToBuy;
    public int price;
    public void Buy() {
        if(price > LootScript.money) return;
        FindObjectOfType<PlaceItemScript>().inventory.Add(whatToBuy);
        LootScript.money -= price;
        ServiceManager.Instance.Get<OnMoneyChanged>().Invoke(LootScript.money);
    }
}
