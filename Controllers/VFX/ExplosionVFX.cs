using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{

    public float set_time = 1.3f;
    private float time = 0f;

    private void Awake()
    {
        Destroy(gameObject, set_time);
    }
}
