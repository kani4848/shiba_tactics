using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//UIを操作するときには必ず書きましょう
using UnityEngine.UI;
using main;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

	public Text enemyCount;

	//敵を倒した時にもらえるゴールド
	public int rewardGold;

	private float timer = 0.0f;

	//状態遷移用列挙体
	enum STEP{
		NONE = -1,
		START,
		BATTLE,
		END,
		CLOSE
	}
	STEP step = STEP.NONE;
	private bool isBattle = false;
	private bool isEnd = false;
	private bool isClose = false;

	//兵士数。フィールドのプレイヤー情報から値を受け取る。
	//public int playerTroopCount;
	GameManager.fieldParam battleEnemyParam = GameManager.instance.ActiveFieldEnemyUnitParam;

	public Dictionary<int,GameObject> playerTroopDic = new Dictionary<int,GameObject>();
	public Dictionary<int,GameObject> enemyTroopDic = new Dictionary<int,GameObject>();

	//外部gameobject制御用
	private TroopCreator troopCreator;
	public UIManager uiManager;
	private TroopList troopList;


	// Use this for initialization
	void Start () {
		rewardGold = battleEnemyParam.troopCount * 10;

		uiManager.updateCountUI(playerTroopDic.Count,enemyTroopDic.Count);

		troopCreator = GetComponent<TroopCreator> ();
		troopList = GetComponent<TroopList> ();

		step = STEP.START;

		//ここでtroopCreatorを使って兵隊を生成
		//兵隊を作る関数。引数は、兵隊の数、兵隊を管理するリスト、作る兵隊のタイプ。
		troopCreator.CreateTroop (GameManager.instance.playerFieldParam,playerTroopDic,"player");
		troopCreator.CreateTroop (battleEnemyParam,enemyTroopDic,"enemy");
	}


	// Update is called once per frame
	void Update () {
		StepInitialize ();
		StepUpdate ();
	}


	//状態遷移の瞬間の処理。一度だけ行う処理も記述
	void StepInitialize(){
		switch (step) {
		case STEP.START:
			{
				if (isBattle) {
					foreach (GameObject troop in playerTroopDic.Values) {
						troop.GetComponent<troopControl> ().state = troopControl.STATE.CHASE;
					}
					foreach (GameObject troop in enemyTroopDic.Values) {
						troop.GetComponent<troopControl> ().state = troopControl.STATE.CHASE;
					}
					step = STEP.BATTLE;
				}

				break;
			}
		case STEP.BATTLE:
			{
				if (isEnd) {
					if (playerTroopDic.Count > 0) {
						foreach (GameObject troop in playerTroopDic.Values) {
							troop.GetComponent<troopControl> ().state = troopControl.STATE.VICTORY;
                            }
                            uiManager.victoryUI();
                            uiManager.rewardUItext.text = rewardGold + "ゴールドを獲得した！";
                            GameManager.instance.playerFieldParam.gold += rewardGold;

                            step = STEP.END;
                        } else {
                            //負けイベント
						foreach (GameObject troop in enemyTroopDic.Values) {
                                troop.GetComponent<troopControl>().state = troopControl.STATE.VICTORY;
                                uiManager.loseUI();
                            }
                        }
			
				}
				break;
			}
		case STEP.END:
			{
				if (isClose) {

					//プレイヤーの兵士数を記録
					GameManager.instance.playerFieldParam.troopCount = playerTroopDic.Count;
					//兵士数が０ならそのenemyUnitを削除
					if (enemyTroopDic.Count <= 0) {
						GameManager.instance.fieldEnemyUnitDic.Remove (battleEnemyParam.ID);
					}
					step = STEP.CLOSE;
				}

				break;
			}
		case STEP.CLOSE:
			{
				break;
			}
				
		}
	}

	//毎フレーム行う処理を状態ごとに設定する関数
	void StepUpdate(){
		switch (step) {
		case STEP.START:
			{
				timer = Time.time;

				if (timer >= 1) {
					isBattle = true;
					timer = 0;
				}

			}
			break;

		case STEP.BATTLE:
			{
				uiManager.updateCountUI(playerTroopDic.Count,enemyTroopDic.Count);
				if(enemyTroopDic.Count==0 || playerTroopDic.Count ==0){
					isEnd = true;
				}
				break;
			}
		case STEP.END:
			{
				timer = Time.time;

				if (timer >= 2) {
					isClose = true;
				}
				break;
			}
		case STEP.CLOSE:
			{
				if (Input.GetMouseButtonDown (0)) {
					SceneManager.LoadScene("field");
				}
				break;
			}
		}
	}
}
