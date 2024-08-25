using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class LevelScript : MonoBehaviour {
    public List<GameObject> availableItems;

    void OnEnable() {
        ServiceManager.Instance.Get<OnLevelLoaded>().Invoke(this);
        AstarPath.active.Scan();
    }
}