using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface宣言はメンバ変数に暗黙的にstatic final修飾子がつく
//static は1つのみ生成され重複せず、finalは値が変更不可になる（定数）
public interface ICommand
{
	//アクセサに特別な処理が必要ない場合、下記のように簡単な表記でOK。{get;set;}
	//setを記述しないとそのプロパティに代入はできなくなる。
	string Tag{get;}

	void Command (Dictionary<string,string>command);
}


//事前に呼ばれるコマンド
public interface IPreCommand
{
	string Tag{get;}
	void PreCommand (Dictionary<string,string> command);
}