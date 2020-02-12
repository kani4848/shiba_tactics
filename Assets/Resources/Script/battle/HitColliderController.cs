using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アニメーションタイムライン上でヒットボックスのオンオフするためのスクリプト

public class HitColliderController : MonoBehaviour
{
    public new Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider.enabled = false;
    }

    public void PunchOn()
    {
        collider.enabled = true;
    }


    public void PunchOff()
    {
        collider.enabled = false;
    }
}