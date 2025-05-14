using System;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace Config
{
    [CreateAssetMenu(menuName = "Felix/BuyOption")]
    public class BuyOption : ScriptableObject
    {
        public string buyName;
        public PlaceableItem whatToBuy;
        public int price;
    }
}