using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using main;

//シーン遷移を使う時の名前空間
using UnityEngine.SceneManagement;

public class FieldPlayerController : MonoBehaviour {

	public new Transform transform;

	//　移動速度 
	public float speed = 10;
	Vector3 vec;

	enum STATE{
		NONE=-1,
		MOVE,
		WAIT
	}

	public bool isMove;
	public bool isWait;
	public bool isStop;
	public bool isNovel;

	private STATE state;

	void Start(){
		transform = this.gameObject.transform;
		vec = transform.position;

		state = STATE.WAIT;

		isMove = false;
		isWait = false;
		isStop = false;

	}

	void OnTriggerEnter2D(Collider2D col){
		vec = transform.position;
		isWait = true;
		isStop = true;
		isMove = false;

		//ぶつかったオブジェクトのタグで処理分岐
		switch(col.gameObject.tag){
		case "enemy":

			//ぶつかった相手の情報を格納
			int ID = col.gameObject.GetComponent<FieldUnitParam> ().ID;
			GameManager.instance.ActiveFieldEnemyUnitParam = GameManager.instance.fieldEnemyUnitDic [ID];
			GameManager.instance.playerFieldParam.position = transform.position;

			ScenarioManager.loadFileName = "encountEnemy";
			SceneManager.LoadScene("novel",LoadSceneMode.Additive);
			break;

		case"place":
			
			CommandGetWord.fieldParam = col.gameObject.GetComponent<FieldPlaceParam> ();
			ScenarioManager.loadFileName = "place";
			SceneManager.LoadScene("novel",LoadSceneMode.Additive);

			break;
		}
	}

	void Update () { 

		fieldPlayerStateTrigger ();
		fieldPlayerStateAct ();

		if(SceneManager.GetSceneByName("novel").isLoaded == true){
			isNovel = true;
		}

		if (isNovel == true && SceneManager.GetSceneByName("novel").isLoaded == false) {
			isNovel = false;
			isStop = false;
		}
	}

	void fieldPlayerStateTrigger(){
		switch (state) {
		case STATE.MOVE:
			if (isWait) {
				transform.Find ("charaImage").gameObject.GetComponent<Animator> ().SetTrigger ("isWait");
				state = STATE.WAIT;
			}
			break;

		case STATE.WAIT:
			if (isMove) {
				transform.Find ("charaImage").gameObject.GetComponent<Animator> ().SetTrigger ("isWalk");
				state = STATE.MOVE;
			}
			break;
		}
	}

	void fieldPlayerStateAct(){
		switch (state) {
		case STATE.MOVE:
			if (transform.position == new Vector3(vec.x,vec.y,transform.position.z)) {
				isWait = true;
				isMove = false;
			}

			//cameraがperspectiveになっていると正確に動きまへん
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (vec.x, vec.y), speed * Time.deltaTime); 
			break;


		case STATE.WAIT:
			if(Input.GetMouseButton(0) && isStop == false){
				vec=Camera.main.ScreenToWorldPoint(Input.mousePosition); 
				isMove =true;
				isWait = false;
			}
			break;
		}

	}
}