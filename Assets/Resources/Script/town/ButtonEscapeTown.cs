using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEscapeTown : MonoBehaviour {

	public void ButtonPush(){
		SceneManager.UnloadSceneAsync("novel");
		SceneManager.UnloadSceneAsync ("town");
	}
}
