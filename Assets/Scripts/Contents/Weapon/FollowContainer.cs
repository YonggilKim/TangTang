using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowContainer : MonoBehaviour
{
    public float _value = 100;

    void Update()
    {
        transform.Rotate(0, 0, +_value);
    }
}

