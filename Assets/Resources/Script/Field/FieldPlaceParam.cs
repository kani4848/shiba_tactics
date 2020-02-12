using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FieldPlaceParam : MonoBehaviour {

	public Dictionary<string,string> param = new Dictionary<string, string>(){
		{"name","hoge"}
	};

	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		param["name"] = this.gameObject.transform.Find ("Canvas/Text").transform.GetComponent<Text>().text;
		if(param["name"]==null){
			param ["name"] = "none";
		}
	}
}
