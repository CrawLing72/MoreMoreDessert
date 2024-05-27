using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGateController : MonoBehaviour
{
    // Get MarkerObject
    [Header("Deafult Settings")]
    public GameObject MarkerObejct;
    public GameObject Rainyk;

    [Header("Warp Settings")]
    public bool SidetoTop;
    public bool StayCurrent = true;

    Vector3 impotredVector;

    private void Start()
    {
        impotredVector = MarkerObejct.transform.localPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Animator�� ������ ó�� �߰��� ���� ��
            Rainyk.transform.localPosition = impotredVector;

            if(!StayCurrent) GameManager.isSideView = SidetoTop? false : true;
        }
    }
}
