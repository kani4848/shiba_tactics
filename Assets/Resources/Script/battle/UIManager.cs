using UnityEngine;
using System.Collections;

//UIを操作するときには必ず書きましょう
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Image victory;
    public Image gameover;
    public AudioSource audioSource;
	public AudioClip victorySE;

	[SerializeField]
	private Image rewardUI;
	[SerializeField]
	public Text rewardUItext;
	[SerializeField]
	private Text playerCountUI;
	[SerializeField]
	private Text enemyCountUI;

	// Use this for initialization
	void Start () {
		victory.enabled = false;
        gameover.enabled = false;
		rewardUI.enabled = false;
		rewardUItext.enabled = false;
		audioSource = this.gameObject.GetComponent<AudioSource> ();
		victorySE = Resources.Load ("SE/victory")as AudioClip;
	}

	public void updateCountUI(int playerCount,int enemyCount){
		playerCountUI.text = playerCount.ToString ();
		enemyCountUI.text = enemyCount.ToString ();
	}

	public void victoryUI(){
		audioSource.PlayOneShot(victorySE);
		victory.enabled = true;
		rewardUI.enabled = true;
		rewardUItext.enabled = true;
	}

    public void loseUI()
    {
        gameover.enabled = true;
    }
}
