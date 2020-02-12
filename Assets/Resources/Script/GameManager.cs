using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//namespaceは同じ名前のクラスがあったときにカテゴライズして別物として扱うメソッド
namespace main{
	public class GameManager : MonoBehaviour {

		//シングルトン
		//ゲームを通じて保管しておきたい情報はここに書きましょう

		public static GameManager instance;
		public static string callScenario;

		//FieldUnitParamとは別物。情報保存用の構造体
		public struct fieldParam{
			public int ID;
			public int troopCount;
			public string troopType;
			public string imagePath;
			public Vector3 position;
			public int gold;
		}

		public fieldParam playerFieldParam = new fieldParam {
			troopCount = 200,
			troopType = "軽装歩兵",
			imagePath = "player",
			position = new Vector3 (27, -13, 0),
			gold = 150,
		};

		//現在なんのシーンかのフラグ
		public static Dictionary<string,bool> currentScene = new Dictionary<string, bool>(){
			{"field",true},
			{"novel",false},
			{"battle",false}
		};

		public fieldParam ActiveFieldEnemyUnitParam;

		public Dictionary<int,fieldParam> fieldEnemyUnitDic = new Dictionary<int,fieldParam>();

		private GameManager(){
			Debug.Log ("GameManagerのインスタンスを作ったよ");
		}

		void Awake(){
			if (instance == null) {
				instance = this;
				DontDestroyOnLoad (this.gameObject);
			}
		}

		//以下のスクリプトは下記の理由により壊れやすくなっており没となった
		//スクリプトオブジェクトはgameManagerとは別コンポーネントとなるため、いちいちdontdestroyする必要がある。しないとなくなる
		//シングルトン以外の情報を参照しようとするとシーン切り替え時に破棄されるため、中身がいつの間にかなくなっていることがある
		//以上の理由から、GameManagerに所属できる構造体のほうが管理が楽である。

		/*
		//プレイヤーの情報
		public FieldUnitParam playerFieldParam;
		//プレイヤーと戦闘するenemy情報の入れ物
		public FieldUnitParam ActiveFieldEnemyUnitParam;

		public Dictionary<int,FieldUnitParam> fieldEnemyUnitDic = new Dictionary<int,FieldUnitParam>();

		//クラス名の関数はそのクラスのインスタンスが作られるタイミングで
		//実行される。初期化に利用される。
		private GameManager(){
			Debug.Log ("GameManagerのインスタンスを作ったよ");
		}

		//インスタンスを一つだけ生成
		void Awake(){
			
			if (instance == null) {
				instance = this;
				DontDestroyOnLoad (this.gameObject);

				playerFieldParam = gameObject.AddComponent<FieldUnitParam>();
				//DontDestroyOnLoad (playerFieldParam);
				ActiveFieldEnemyUnitParam = gameObject.AddComponent<FieldUnitParam>();
				//DontDestroyOnLoad (ActiveFieldEnemyUnitParam);
				playerFieldParam = GameObject.FindGameObjectWithTag ("player").GetComponent<FieldUnitParam> ();
			}
		}
		*/
	}
}
