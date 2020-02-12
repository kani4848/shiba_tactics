using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;

//ノベルシナリオ内の<>内を特定の言葉に置き換えるクラス
public class CommandGetWord : MonoBehaviour {
	
	//プレイヤーが立ち寄った街のパラメータ
	public static FieldPlaceParam fieldParam;

	//引数のテキスト内＜＞を置き換える
	public string getWord(string text){
		string _text = string.Empty;

		Regex regex = new Regex(@"<(\S+)=(\S+)>");
		var matches = regex.Matches (text);

		if(matches.Count >= 0){

			string replaceWord;

			foreach (Match match in matches) {
				if (match.Groups [1].ToString () == "town") {
					replaceWord = fieldParam.param[match.Groups [2].ToString ()];
					_text = text.Replace (match.ToString (), replaceWord);
				}
				return _text;
			}
		}

		return text;
	}
}
