using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using main;

public class TroopCreator : MonoBehaviour {

    //戦闘画面の兵隊を作る関数。引数は、兵隊の数、兵隊を管理するリスト、作る兵隊のタイプ。
    public void CreateTroop(GameManager.fieldParam param, Dictionary<int, GameObject> troopDic, string side) {

        for (int i = 0; i < param.troopCount; i++) {
            GameObject troopIns = Instantiate(Resources.Load("Prefab/battle/Troop") as GameObject) as GameObject;
            //オブジェクトの座標を変えたい場合は、positionのxとyに代入せず、新たにvector3オブジェクトを用意する
            //position,x(y)で渡されるのは座標の値のコピーだそうです
            //troopIns.GetComponent<Transform>().position += new Vector3(0.0f, 0.3f * i, 0.0f);

            //兵士のパラメータを取得
            TroopList.troopInfo type = TroopList.list[param.troopType];

            troopIns.transform.Find("troopImage").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/" + type.imagePath, typeof(Sprite)) as Sprite;
            troopIns.GetComponent<troopControl>().hp = type.hp;
            troopIns.GetComponent<troopControl>().atk = type.atk;
            troopIns.GetComponent<troopControl>().max_vel = type.max_vel;
            troopIns.GetComponent<troopControl>().deathVoice = Resources.Load("SE/" + type.deathVoicePath) as AudioClip;
            troopIns.GetComponent<troopControl>().myDictionary = troopDic;
            troopIns.GetComponent<troopControl>().myNumber = i;

            troopDic.Add(i, troopIns);

            //敵味方で座標を変更
            if (side == "player") {
                troopIns.tag = "player";
                //scaleを変えたいときはlocalScaleで呼ぶ
                Vector3 sca = troopIns.GetComponent<Transform>().localScale;
                troopIns.GetComponent<Transform>().localScale = new Vector3(sca.x * -1.0f, sca.y, sca.z);
                troopIns.GetComponent<Transform>().position += new Vector3(-8.0f, -1.0f, 0.0f);
            }
            else {
                troopIns.tag = "enemy";
                troopIns.GetComponent<Transform>().position += new Vector3(8.0f, -1.0f, 0.0f);
            }
        }
        
        //兵士を正方形に整列させる処理。
        int wh = Mathf.CeilToInt(Mathf.Sqrt(param.troopCount));
        //int wh = (int)Mathf.Sqrt(param.troopCount);
        float row = 0;
        float column = 0;
        //兵隊同士の間隔
        float interbal = 1f;
        for(int i = 0; i < troopDic.Count; i++)
        {
            GameObject troopIns = troopDic[i];
            if(troopIns.tag == "player")
            {
                troopIns.GetComponent<Transform>().position += new Vector3(interbal * wh / 2 - interbal * row, interbal * wh / 2 - interbal * column, 0.0f);

            }
            else
            {
                troopIns.GetComponent<Transform>().position += new Vector3(-interbal * wh / 2 + interbal * row, interbal * wh / 2 - interbal * column, 0.0f);
            }
            column++;
            if (column >= wh)
            {
                row++;
                column = 0;
            }
        }
    }
}
