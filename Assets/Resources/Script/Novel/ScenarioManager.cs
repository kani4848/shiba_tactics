using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Text.RegularExpressions;

//どうやら外部ファイルを読み込むための名前空間らしい
using System.IO;
//asciiおよびunicodeのエンコーディングを表す名前空間
using System.Text;

using main;

//RequireComponent属性は指定したコンポーネントを自動的にゲームオブジェクトへと付与する
[RequireComponent(typeof(TextController))]

//このクラスで外部からテキストファイルの文字列を読み込み、会話ウインドウに表示させる
//シングルトンクラスを
public class ScenarioManager : SingletonMonoBehaviourFast<ScenarioManager> {

	static public string loadFileName;

	//シナリオを行ごとに管理する配列
	private string[] m_scenarios;

	private int m_currentLine = 0;
	private bool m_isCallPreload = false;

	private TextController m_textController;
	private CommandController m_commandControler;
	private CommandGetWord m_commandGetWord;

	//textControllerに表示してもらう新しい行を渡す
	void RequestNextLine(){
		var currentText = m_scenarios [m_currentLine];

		m_textController.SetNextLine (CommandProcess(currentText));
		m_currentLine++;
		m_isCallPreload = false;
	}

	//外部テキストファイルを読み込み、改行ごとに配列要素に分割してm_scenariosにぶちこむ関数
	public void UpdateLines(string fileName){
		//Resource.Load<hoge>("huga")はResource.Load("huga",typeof(hoge))as hogeと同じらしい？
		var ScenarioText = Resources.Load<TextAsset> ("Scenario/" + fileName);

		if (ScenarioText == null) {
			Debug.LogError ("シナリオファイルが見つかりませんでした。");
			Debug.LogError ("ScenarioManagerを無効化します。");
			//enabledがfalseだと更新されない
			enabled = false;
			return;
		}
		//splitは対象の文字列を、指定された文字列を境に分割して配列に格納する。
		//StringSplitOptionsは戻り値の空の部分文字列（何も書かれていない文字列）を配列に入れるかどうかを決める。以下の2つのメンバで処理を変える。
		//StringSplitOptions.Noneは戻り値に空の文字列を含んだ配列要素を返す。
		//StringSplitOptions.RemoveEmptyEntriesは空の文字列を省略した配列要素を返す。
		m_scenarios = ScenarioText.text.Split (new string[]{ "@br" }, System.StringSplitOptions.None);
		m_currentLine = 0;

		Resources.UnloadAsset (ScenarioText);
	}
		
	//テキストの文字列からコメントやコマンドを除外する。
	private string CommandProcess(string line){
		//stringReaderクラスは改行を含む文字列を扱う。おそらくreadLineメソッドを使い、行単位で文字列を呼び出すために使うのだろう。
		//ちなみにこれを使うのは、OSによって改行コードが異なるのを一括で扱えるため。
		var lineReader = new StringReader (line);

		//stringは固定長であり、中身の文字列に追加をしたい場合、再代入が必要になる。
		//これに対してSttingBuilderクラスは可変長であり、オブジェクトを再度作ることなく後から文字列の追加が可能。
		var lineBuilder = new StringBuilder ();

		var text = string.Empty;
		while ((text = lineReader.ReadLine ()) != null) {
			//string.indexOf（引数）は、string内から引数の文字列を探してその位置を整数で返す。
			//見つからなければ-1を返す。
			var commentCharacterCount = text.IndexOf ("//");
			if (commentCharacterCount != -1) {
				//string.SubStringは、指定した位置から指定した文字数分のstringを取得する。
				//"//"以前の文字列だけを回収する
				text = text.Substring (0, commentCharacterCount);
			}

			//<>の置き換え
			text = m_commandGetWord.getWord(text);

			//string.IsNullEmptyは、文字列がnullかempty（空の文字列）であるかどうかをbooleanで返す
			if (!string.IsNullOrEmpty (text)) {

				//コマンド表記があった場合、テキストには加えずとばす
				if (text[0] == '@' && m_commandControler.LoadCommand (text)) {
					continue;
				}
				
				lineBuilder.AppendLine(text);
			}
		}
		return lineBuilder.ToString ();
	}

	//#regionはvisualStudioで見るときに折り畳みができるようになるらしい
	#region UNITY_CALLBACK

	// Use this for initialization
	void Start () {
		//TextControllerクラスのインスタンスを作る
		m_textController = GetComponent<TextController> ();
		m_commandControler = GetComponent<CommandController> ();
		m_commandGetWord = GetComponent<CommandGetWord>();

		UpdateLines (loadFileName);

		RequestNextLine ();

	}
	
	// Update is called once per frame
	void Update () {
		//プリロード、次の行に進む、文字表示を飛ばす処理
		if (m_textController.IsCompleteDisplayText) {
			if (m_currentLine < m_scenarios.Length) {
				if (!m_isCallPreload) {
					m_commandControler.PreloadCommand (m_scenarios [m_currentLine]);
					m_isCallPreload = true;
				}

				if (Input.GetMouseButtonDown (0)) {
					RequestNextLine ();
				}
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				m_textController.ForceCompleteDisplayText ();
			}
		}
		if (GameManager.callScenario != null) {
			UpdateLines (GameManager.callScenario);
			RequestNextLine ();
			GameManager.callScenario = null;

		}
	}

	#endregion

}
