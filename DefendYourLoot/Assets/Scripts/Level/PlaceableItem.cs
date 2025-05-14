using System;
using UnityEngine;

namespace Level
{
    
    public class PlaceableItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnValidate()
        {
            if(!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public Sprite Sprite
        {
            set => spriteRenderer.sprite = value;
            get => spriteRenderer.sprite;
        }
    }
}