using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationTracker : MonoBehaviour
{
    public float GetOrientation()
    {
        return Mathf.Sign(transform.localScale.x) * 1.0f;
    }
}
