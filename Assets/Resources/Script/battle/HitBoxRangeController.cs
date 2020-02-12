using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxRangeController : MonoBehaviour
{
    public GameObject root;
    public troopControl rootPara;

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.transform.parent.gameObject == rootPara.target && col.tag == "hitCollision")
        {
            rootPara.state = troopControl.STATE.ATTACK;
        }
    }
}
