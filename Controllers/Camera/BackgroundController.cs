 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Get MarkerObject
    [Header("Deafult Settings")]
    public GameObject Background;

    [Header("Warp Settings")]
    public bool isBackground = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Background.SetActive(isBackground);
        }
    }
}