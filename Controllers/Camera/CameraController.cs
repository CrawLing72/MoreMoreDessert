using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float zoomLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0 && !GameManager.isFrozen)
        {
            zoomLevel += scrollWheelInput * 10;
            zoomLevel = Mathf.Clamp(zoomLevel, 1, 5);
            cam.transform.localScale = new Vector3(1/zoomLevel, 1/zoomLevel, 1/zoomLevel);
        }
    }
}
