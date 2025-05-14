using Core;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ManaDisplay : MonoBehaviour 
    {
        private Slider _slider;

        private void OnValidate()
        {
            if (!_slider)
            {
                _slider = GetComponentInChildren<Slider>();
            }
        }
        void Start() {
            ServiceManager.Instance.Get<OnManaChanged>().Subscribe(HandleManaChanged);
        }
        void OnDestroy() {
            ServiceManager.Instance.Get<OnManaChanged>().Unsubscribe(HandleManaChanged);
        }
        private void HandleManaChanged(float ratio)
        {
            _slider.value = ratio;
        }
    }
}