using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ヒットボックス侵入時の処理内容

public class HitboxController : MonoBehaviour
{
    public GameObject root;
    public troopControl rootPara;
    private AudioClip hitSE;
    private GameObject hitEffect;

    private void Start()
    {
        hitSE = Resources.Load("SE/hit") as AudioClip;
        hitEffect = Resources.Load("Effect/hitMini") as GameObject;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        troopControl colPara = col.transform.root.GetComponent<troopControl>();

        if (col.transform.root.gameObject == rootPara.target && col.tag == "hitCollision")
        {
            col.transform.root.GetComponent<troopControl>().hp -= rootPara.atk;
            Debug.Log(root.name + "は" + col.transform.root.name + "に" + rootPara.atk + "のダメージを与えた");

            Vector2 collider = root.transform.position;
            Vector2 hitPosition = col.ClosestPoint(collider);
            GameObject hitEf = Instantiate(hitEffect) as GameObject;
            hitEf.transform.position = (Vector3)hitPosition;

            rootPara.audioSource.PlayOneShot(hitSE);
        }
    }
}
