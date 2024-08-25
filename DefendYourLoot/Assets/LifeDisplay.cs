using UnityEngine;
using UnityEngine.UI;

public class OnLifeChanged : BaseEvent<float>{}
public class LifeDisplay : MonoBehaviour {
    void Start() {
        ServiceManager.Instance.Get<OnLifeChanged>().Subscribe(HandleManaChanged);
    }
    void OnDestroy() {
        ServiceManager.Instance.Get<OnLifeChanged>().Unsubscribe(HandleManaChanged);
    }
    private void HandleManaChanged(float ratio)
    {
        GetComponentInChildren<Slider>().value = ratio;
    }
}