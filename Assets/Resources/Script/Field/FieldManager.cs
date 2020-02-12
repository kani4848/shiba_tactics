using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using main;

public class FieldManager : MonoBehaviour {

	public SpriteRenderer map;
	public GameObject player;

	// Use this for initialization
	void Start () {
		map = GameObject.Find ("Map").GetComponent<SpriteRenderer>();

		if (GameManager.instance.fieldEnemyUnitDic.Count == 0) {
			inputFieldEnemyUnitInfo (40,50,1000);
		}

		PutFieldPlayerUnit ();
		PutFieldEnemyUnit ();
	}

	//フィールドの適当な場所に敵ユニットを出現させる関数
	//引数は出現させる数、1ユニット当たりの兵力
	void inputFieldEnemyUnitInfo(int unitCount,int minTroopCount,int maxTroopCount){
		for (int i = 0; unitCount > i; i++) {
			//情報保存用の構造体を作成
			GameManager.fieldParam param;

			param.ID = i;
			//Random.Rangeはint型を引数にする場合、最大値未満の値を返すので注意
			param.troopCount =Random.Range(minTroopCount,maxTroopCount);
			param.troopType = "重装歩兵";
			param.imagePath ="enemyTroop";
			param.position =
				new Vector3(Random.Range (map.bounds.min.x,map.bounds.max.x),Random.Range (map.bounds.min.y,map.bounds.max.y),0);
			param.gold = 0;

			//情報をゲームマネージャーに登録
			GameManager.instance.fieldEnemyUnitDic.Add(i,param);
		}
	}

	//プレイヤーのフィールドユニットを作成する関数
	void PutFieldPlayerUnit(){
		GameObject unit = Instantiate (Resources.Load("Prefab/field/FieldUnit")as GameObject)as GameObject;
		unit.AddComponent<FieldPlayerController> ();
		unit.tag = "player";
		FieldUnitParam param = unit.GetComponent<FieldUnitParam> ();

		param.troopCount = GameManager.instance.playerFieldParam.troopCount;
		param.troopType = GameManager.instance.playerFieldParam.troopType;
		param.imagePath = GameManager.instance.playerFieldParam.imagePath;
		param.position = unit.transform.position = GameManager.instance.playerFieldParam.position;
		player = unit;
	}

	//ディクショナリに登録している敵のインスタンスを配置する関数
	void PutFieldEnemyUnit(){
		foreach (GameManager.fieldParam g in GameManager.instance.fieldEnemyUnitDic.Values) {
			GameObject unit = Instantiate (Resources.Load("Prefab/field/FieldUnit")as GameObject)as GameObject;
			unit.tag = "enemy";
			FieldUnitParam param = unit.GetComponent<FieldUnitParam> ();

			//gをparamに代入するとおかしくなるぞよ？
			param.ID = g.ID; 
			param.troopCount = g.troopCount;
			param.imagePath = g.imagePath;
			param.position = unit.transform.position = g.position;
		}
	}
}