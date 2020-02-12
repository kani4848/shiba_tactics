using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class troopControl__ : MonoBehaviour {

	//動作制御用
	[SerializeField]
	private Rigidbody2D rigidbody;
	public Transform transform;
	private string opponent;
	private GameObject target;
	public int myNumber;
	public Dictionary<int,GameObject> myDictionary;

	//外部から制御され用
	public bool isFight;
	public bool isVictory;

	//時間処理用
	public float timeOut;
	private float timeTrigger;

	//パラメータ
	public float max_vel;
	public bool alive;
	public int hp;
	public int atk;

	//状態遷移用変数
	enum STATE{
		NONE = -1,
		WAIT,
		BATTLE,
		DEATH,
		VICTORY
	}
	private STATE state;

	//音声・エフェクト・画像・アニメーション
	private Animator animator;
	private Sprite troopImage;
	private GameObject troopSprite;
	private AudioSource audioSource;
	public AudioClip deathVoice;
	private AudioClip hitSE;
	private GameObject hitEffect;

	void Start () {

		timeOut = 30 *Time.deltaTime;
		timeTrigger = 0;




        animator = transform.Find ("troopImage").gameObject.GetComponent<Animator> ();
        //animator = transform.gameObject.GetComponent<Animator>();

        /*
		troopSprite = transform.Find ("troopImage").gameObject;
		//画像に関してはLoad関数内で型を指定してやる必要があるみたいです。
		troopSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/enemyTroop",typeof(Sprite))as Sprite;
*/

        audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.1f;
		hitSE = Resources.Load("SE/hit")as AudioClip;
		hitEffect = Resources.Load("Effect/hitMini")as GameObject;

		if(this.gameObject.tag == "player"){
			opponent = "enemy";
		}else{
			opponent = "player";
		}
	}


	void Update () {
		stateTrigger ();
		stateAction ();

		//斜め上に回転させながら吹っ飛ばすをスクリプトでやった場合の処理
/*		if (!alive) {
			transform.eulerAngles += new Vector3 (0f, 0f, -30f);
		}*/
	}
	
	//他コリジョンとの衝突時のイベント。colがぶつかった相手のcollision
	void OnCollisionStay2D(Collision2D col){
		if(Time.time>timeTrigger && col.gameObject.tag ==opponent){

			//衝突したものが敵なら
			if(col.gameObject.tag==opponent){
				animator.SetTrigger("isAttack");
				col.gameObject.GetComponent<troopControl>().hp-=atk;

				audioSource.PlayOneShot(hitSE);
				
				
				//衝突位置にエフェクトを出す
				foreach (ContactPoint2D hit in col.contacts) {
					Vector2 hitPosition= hit.point;
					GameObject hitEf = Instantiate (hitEffect)as GameObject;
					hitEf.transform.position = (Vector3)hitPosition;
				}
			}
			timeTrigger=Time.time+timeOut;		
		}
	}
		
	//遷移条件および状態遷移時に一度だけ呼ぶ処理
	void stateTrigger(){
		switch (state) {
		case STATE.WAIT:
			{
				if (isFight) {
					state = STATE.BATTLE;

					animator.SetTrigger ("isWalk");
				}
				break;
			}
		case STATE.BATTLE:
			{
                //死亡時のアクションと処理
				if (hp <= 0) {
					state = STATE.DEATH;
					animator.SetTrigger("isDeath");
					Destroy(this.gameObject.GetComponent<CircleCollider2D>());
					Destroy(transform.Find ("shadow").gameObject);
					rigidbody.constraints =RigidbodyConstraints2D.None;
					audioSource.PlayOneShot(deathVoice);
					myDictionary.Remove(myNumber);

                    rigidbody.gravityScale = 3;
                    //↓吹っ飛びの振れ幅用ランダム変数
                    float randomRange = Random.Range(1, 2);


                    if(this.gameObject.tag=="player"){
                            //rigidbody.velocity = new Vector2 (-16.0f, 16.0f);
                           

                            rigidbody.AddForce(new Vector2(-300.0f*randomRange, 600.0f * randomRange));
                        }
                        else{
						//rigidbody.velocity = new Vector2 (16.0f, 16.0f);
                        rigidbody.AddForce(new Vector2(300.0f * randomRange, 600.0f * randomRange));
                        }
                 
					this.gameObject.tag = "Untagged";
				}

				if (isVictory) {
					state = STATE.VICTORY;

					animator.SetTrigger("isVictory");
				}
				break;
			}
		case STATE.DEATH:
			{
				break;
			}
		case STATE.VICTORY:
			{
				break;
			}

		}
	}

	//状態ごとの毎フレーム行う処理
	void stateAction(){
		switch (state) {
		case STATE.WAIT:
			{
				break;
			}
		case STATE.BATTLE:
			{
				animator.SetTrigger ("isWalk");
                    //もっとも近い相手ユニットを探知して移動
                    if (!target)
                    {
                        //Find関数はマイフレーム呼び出すと重くなってしまう
                        target = this.gameObject.Nobj(gameObject, opponent);
                    }
                    if (target.tag == "Untagged")
                    {
                        target = null;
                    }
                    if (target)
                    {
                        this.gameObject.MoveToTarget(gameObject, target);
                    }

				break;
			}
		case STATE.DEATH:
			{
				if (Mathf.Abs (transform.position.y) > 16f || Mathf.Abs(transform.position.x) > 16f) {
					Destroy(this.gameObject);
				}
				break;
			}
		case STATE.VICTORY:
			{
				break;
			}
		}
	}
}
