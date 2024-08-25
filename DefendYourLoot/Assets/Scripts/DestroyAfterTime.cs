using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float fadeTime = 1;
    // Start is called before the first fram#e update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(fadeTime);
        Destroy(gameObject);
    }
}
