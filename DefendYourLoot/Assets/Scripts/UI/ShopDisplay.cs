using System;
using Config;
using Core;
using Events;
using Level;
using UnityEngine;

namespace UI
{
    public class ShopDisplay : MonoBehaviour
    {
        private OnMoneyChanged onMoneyChanged;
        private OnShopClosed onShopClosed;
        [SerializeField] private TMPro.TMP_Text moneyText;
        [SerializeField] private BuyButton buyButtonPrefab;
        [SerializeField] private BuyOption[] buyOptions;
        [SerializeField] private Transform buyButtonsParent;
        private void OnValidate()
        {
            if(!moneyText) moneyText = GetComponent<TMPro.TMP_Text>();
        }

        private void Start()
        {
            onMoneyChanged = ServiceManager.Instance.Get<OnMoneyChanged>();
            onShopClosed = ServiceManager.Instance.Get<OnShopClosed>();
            onMoneyChanged.Subscribe(HandleMoneyChanged);
            
            foreach (var option in buyOptions)
            {
                var button = Instantiate(buyButtonPrefab, buyButtonsParent);
                button.whatToBuy = option.whatToBuy;
                button.price = option.price;
                button.buyName = option.buyName;
            }
        }

        private void HandleMoneyChanged(int obj)
        {
            
            moneyText.text = $"{obj} $";
        }

        public void CloseShop()
        {
            onShopClosed?.Invoke();
        }
    }
}
