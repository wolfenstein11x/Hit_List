using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationTracker : MonoBehaviour
{
    [SerializeField] bool overrideOn = false;
    [SerializeField] float overrideVal = 1.0f;

    public float GetOrientation()
    {
        if (overrideOn) { return overrideVal; }

        return Mathf.Sign(transform.localScale.x) * 1.0f;
    }
}
