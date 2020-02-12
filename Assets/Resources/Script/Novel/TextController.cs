using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//会話イベント用の会話文制御スクリプト
public class TextController : MonoBehaviour {

	/*

	//シナリオ
	public string[] scenarios;
	//uiTextへの参照
	public Text uiText;

	*/

	//1文字の表示にかかる時間
	//[***]これは属性を表記するルール。SelialiseFieldもrangeもアトリビュートである。
	[SerializeField][Range(0.001f,0.3f)]
	public float intervalForCharacterDisplay = 0.05f;

	//現在の行番号
	private int currentLine = 0;
	//現在の文字列
	private string currentText = string.Empty;
	//表示にかかる時間
	private float timeUnitDisplay = 0;
	//文字列の表示を開始した時間
	private float timeElapsed = 1;
	//表示中の文字数
	private int lastUpdateCharacter = -1;

	[SerializeField]
	private Text _uiText;

	//文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText
	{
		get{ return Time.time > timeElapsed + timeUnitDisplay;}
	}


	//強制的に全文表示する
	public void ForceCompleteDisplayText(){
		timeUnitDisplay = 0;
	}

	/*

	// Use this for initialization
	void Start () {
		SetNextLine(scenarios);
	}

	*/


	public void SetNextLine(string text){
		//表示するテキストの行を設定
		currentText = text;
		currentLine++;

		//想定表示時間と現在の時刻をキャッシュ
		timeUnitDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;

		//文字カウントを初期化
		lastUpdateCharacter = -1;
	}


	#region UNITY_CALLBACK

	// Update is called once per frame
	void Update () {

		/*

		//文字の表示が完了しているなら
		if (IsCompleteDisplayText) {
			//ボタンが押されたときに次の行へ進む
			if (currentLine < scenarios.Length && Input.GetMouseButtonUp (0)) {
				SetNextLine (scenarios);
			}
		} else {
			//完了していないなら文字をすべて表示する
			if (Input.GetMouseButtonUp (0)) {
				timeUnitDisplay = 0;
			}
		}

		*/

		//クリックから経過した時間が想定表示時間の何パーセントか確認し、表示文字数を出す
		//(int)はキャスト宣言。後に続く値をその型に直す。
		//Mathf.clamp01は0と１の間に値を制限し、その値を返す。
		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed)/timeUnitDisplay)*currentText.Length);

		//表示文字数が前回の表示文字数と異なるならテキストを更新する
		if (displayCharacterCount != lastUpdateCharacter) {

			//string.Substringは文字列から部分文字列を抽出する。最初の引数文字位置から開始し、次の引数文字位置まで取得する。
			_uiText.text = currentText.Substring (0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}
	#endregion
}
