using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using main;

public class FollowTextUI : MonoBehaviour {

	RectTransform rect;
	Text text;

	[SerializeField]
	GameObject target;

	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform> ();
		text = this.gameObject.GetComponent<Text>();

		switch (target.tag) {
		case "player":
			text.text = GameManager.instance.playerFieldParam.troopCount.ToString();
			break;

		case "enemy":
			text.text = target.GetComponent<FieldUnitParam> ().troopCount.ToString ();
			break;
		
		case "place":
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main,target.transform.position);
		if(SceneManager.GetSceneByName("novel").isLoaded && target.tag == "player"){
			text.text = GameManager.instance.playerFieldParam.troopCount.ToString();
		}
	}
}
