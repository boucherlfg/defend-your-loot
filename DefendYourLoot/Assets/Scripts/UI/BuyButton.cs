using System;
using Core;
using Events;
using Level;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuyButton : MonoBehaviour
    {
        public string buyName;
        public PlaceableItem whatToBuy;
        public int price;
        private GameState gameState;
        
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TMP_Text text;
        private void Start()
        {
            gameState = ServiceManager.Instance.Get<GameState>();
            image.sprite = whatToBuy.Sprite;
            text.text = $"{buyName} ({price})";
        }

        public void Buy() 
        {
            if(price > gameState.Money) 
                return;
            gameState.AddItem(whatToBuy);
            gameState.Money -= price;
        }
    }
}
