using System;
using UnityEngine;
using UnityEngine.UI;
public class OnManaChanged : BaseEvent<float>{}
public class ManaDisplay : MonoBehaviour {
    void Start() {
        ServiceManager.Instance.Get<OnManaChanged>().Subscribe(HandleManaChanged);
    }
    void OnDestroy() {
        ServiceManager.Instance.Get<OnManaChanged>().Unsubscribe(HandleManaChanged);
    }
    private void HandleManaChanged(float ratio)
    {
        GetComponentInChildren<Slider>().value = ratio;
    }
}