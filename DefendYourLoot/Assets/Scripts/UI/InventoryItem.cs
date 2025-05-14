using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image image;

        private void OnValidate()
        {
            if(!image) image = GetComponentInChildren<Image>();
        }

        public Sprite Sprite
        {
            get => image.sprite;
            set => image.sprite = value;
        }
    }
}