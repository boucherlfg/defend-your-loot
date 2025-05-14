using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Events;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        ServiceManager.Instance.Get<OnIntroductionClosed>().Invoke();
    }
}
