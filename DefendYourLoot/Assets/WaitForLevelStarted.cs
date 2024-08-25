using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaitForLevelStarted : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> scriptsToWait;
    void Awake() {
        scriptsToWait.ForEach(x => x.enabled = false);
        ServiceManager.Instance.Get<OnLevelStarted>().Subscribe(HandleLevelStarted);
    }

    private void HandleLevelStarted()
    {
        ServiceManager.Instance.Get<OnLevelStarted>().Unsubscribe(HandleLevelStarted);
        scriptsToWait.ForEach(x => x.enabled = true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
