using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class troopControl : MonoBehaviour {

	//動作制御用
	[SerializeField]
	//public new Rigidbody2D rigidbody;
	public new Transform transform;
    public GameObject hitBox;
    private string opponent;
	public GameObject target;
	public int myNumber;
	public Dictionary<int,GameObject> myDictionary;

    //状態遷移時に一度だけ処理する用
    private string stateOnce;

	//時間処理用
	public float timeOut;
	private float timeTrigger;

	//パラメータ
	public float max_vel;
	public bool alive;
	public int hp;
	public int atk;

	//状態遷移用変数
	public enum STATE{
		NONE = -1,
		WAIT,
		CHASE,
        ATTACK,
		DEATH,
		VICTORY
	}
	public STATE state = STATE.NONE;

	//音声・エフェクト・画像・アニメーション
	private Animator animator;
	private Sprite troopImage;
	private GameObject troopSprite;
	public AudioSource audioSource;
	public AudioClip deathVoice;

	void Start () {

        this.name = this.tag + myNumber;

        state = STATE.WAIT;

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

		if(this.gameObject.tag == "player"){
			opponent = "enemy";
		}else{
			opponent = "player";
		}
	}


	void Update () {

        //HPが0になったら死亡ステート
        if (hp <= 0)
        {
            state = STATE.DEATH;
        }
        
        stateTrigger ();
		stateAction ();
    }

    //遷移条件および状態遷移時に一度だけ呼ぶ処理
    void stateTrigger(){
		switch (state) {
		    case STATE.WAIT:
			    {
				    if (stateOnce != "isChase") {
                        stateOnce = "isChase";
                        state = STATE.CHASE;
					    animator.SetTrigger ("isWalk");
				    }
				    break;
			    }
		    case STATE.CHASE:
			    {
				    break;
			    }
            case STATE.ATTACK:
                {
                    if (!target)
                    {
                        state = STATE.CHASE;
                    }
                    if (stateOnce != "isAttack")
                    {
                        stateOnce = "isAttack";
                        animator.SetTrigger("isAttack");
                    }
                    break;
                }
		    case STATE.DEATH:
			    {
                    if (stateOnce != "isDeath")
                    {
                        Debug.Log(this.gameObject.tag + myNumber + "は死亡した");

                        animator.SetTrigger("isDeath");
                        Destroy(this.gameObject.GetComponent<CircleCollider2D>());
                        Destroy(hitBox);
                        Destroy(transform.Find("shadow").gameObject);
                        //rigidbody.constraints = RigidbodyConstraints2D.None;
                        audioSource.PlayOneShot(deathVoice);
                        myDictionary.Remove(myNumber);

                        /*
                        rigidbody.gravityScale = 3;
                        //↓吹っ飛びの振れ幅用ランダム変数
                        float randomRange = Random.Range(1, 2);


                        if (this.gameObject.tag == "player")
                        {
                            rigidbody.AddForce(new Vector2(-300.0f * randomRange, 600.0f * randomRange));
                        }
                        else
                        {
                            rigidbody.AddForce(new Vector2(300.0f * randomRange, 600.0f * randomRange));
                        }
                        */

                        this.gameObject.tag = "Untagged";
                        stateOnce = "isDeath";
                    }
				    break;
			    }
		    case STATE.VICTORY:
			    {
                    if (stateOnce != "isVictory")
                    {
                        animator.SetTrigger("isVictory");
                        stateOnce = "isVictory";
                    }
                    
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
		    case STATE.CHASE:
			    {
				    animator.SetTrigger ("isWalk");
                        //もっとも近い相手ユニットを探知して移動
                        if (!target)
                        {
                            //Find関数はマイフレーム呼び出すと重くなってしまう
                            //Nobjは一番近いオブジェクトを探すスクリプト
                            target = this.gameObject.Nobj(gameObject, opponent);
                        }
                        if (target)
                        {
                            this.gameObject.MoveToTarget(gameObject, target);
                        }

				    break;
			    }
            case STATE.ATTACK:
                {
                    break;
                }
		    case STATE.DEATH:
			    {

				    //if (Mathf.Abs (transform.position.y) > 16f || Mathf.Abs(transform.position.x) > 16f) {
					    Destroy(this.gameObject);
				    //}
				    break;
			    }
		    case STATE.VICTORY:
			    {
				    break;
			    }
		}
	}
}
