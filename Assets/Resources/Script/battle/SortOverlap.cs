using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//戦闘シーンで下にいるユニットほど手前に表示するスクリプト
public class SortOverlap : MonoBehaviour
{
    public new Transform transform;

    void Update()
    {
        if(transform.position.z !=transform.position.y)
        {
            Vector3 pos = transform.position;
            pos.z = transform.position.y;

            //xyzに直接値は入れられないのでvector3形式で代入する。
            transform.position = pos;
        }
    }
}
