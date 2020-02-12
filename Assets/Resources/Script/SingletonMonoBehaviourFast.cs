using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//abstructは抽象クラス。このクラスを継承したクラスがオーバーライドすることが前提で、このクラスでは中身がなく定義のみ。
//<T>はジェネリクス関数の”型引数”宣言。ここでのTは任意のクラスを指し、利用時に改めて型を指定する。
//ジェネリックの利点は複数の値を管理するコンテナクラスの際に発揮される。要素の型、格納方式、操作を都度指定すれば用意するコードは1つで済む。
//whereは型引数に制約条件を付与する。型引数Tにクラスやinterfaceなどを継承させるとでも覚えておこう・・・。参考URL　http://ufcpp.net/study/csharp/sp2_generics.html
//というわけで、ここでのwhereは｛｝内の内容をそのまま持っている、という処理なんでしょう、きっと。
public abstract class SingletonMonoBehaviourFast<T> : MonoBehaviour where T:SingletonMonoBehaviourFast<T>{

	//pritectedはアクセス修飾子。そのクラスと派生クラスのみからアクセス可能
	//staticは静的メンバーの宣言。インスタンス化できず、呼び出すときはそのクラス名から呼ぶ。
	//static readonlyは定数扱いの変数。代入ができない不動の値（円周率など値が変わらないものに使うとよい）。似たようなのにconstがある。
	protected static readonly string[] findTags = {
		"GameController",
	};

	protected static T instance;
	public static T Instance{
		//getは外部から変数instanceを呼び出すときの内部処理。
		//外部からは変更不可でメンバ変数に見える。そのほかsetで値に条件を付けることができる。
		get{
			if (instance == null) {
				//System名前空間のSystem.Typeは、オブジェクトの型（クラス）の種類を取得する。
				Type type = typeof(T);

				foreach(var tag in findTags){
					GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

					for (int j = 0; j < objs.Length; j++) {
						instance = (T)objs[j].GetComponent (type);
						if (instance != null) {
							return instance;
						}　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
					}
				}
				//string.Formatはクラスのメンバーを成型して文字列にする。
				//{0}は、"",以降の変数を描かれている順番に表す。下記の場合、type.nameは０番目の変数なので{0}。
				Debug.LogWarning (string.Format ("{0} is not found",type.Name));
			}
			return instance;
		}
	}

	//Awake関数はゲーム起動時にstartより最初に呼び出される特殊な関数
	virtual protected void Awake(){
		{
			CheckInstance ();
		}
	}

	protected bool CheckInstance(){
		if (instance == null) {
			instance = (T)this;
			return true;
		}else if(Instance == this){
			return true;
		}

		Destroy (this);
		return false;
	}
}
