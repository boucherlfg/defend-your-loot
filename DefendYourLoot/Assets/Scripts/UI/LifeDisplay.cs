using System;
using Core;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LifeDisplay : MonoBehaviour
    {
        private Slider _slider;

        private void OnValidate()
        {
            if (!_slider)
            {
                _slider = GetComponentInChildren<Slider>();
            }
        }

        private void Start() {
            ServiceManager.Instance.Get<OnLifeChanged>().Subscribe(HandleManaChanged);
        }

        private void OnDestroy() {
            ServiceManager.Instance.Get<OnLifeChanged>().Unsubscribe(HandleManaChanged);
        }
        private void HandleManaChanged(float ratio)
        {
            _slider.value = ratio;
        }
    }
}