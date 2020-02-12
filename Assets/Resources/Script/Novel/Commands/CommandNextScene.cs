using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommandNextScene : ICommand,IPreCommand {

	public string Tag{
		get{ return "scene";}
	}

	public void PreCommand(Dictionary<string,string> command){
		
	}

	public void Command(Dictionary<string,string>command){
		var sceneName = command ["name"];

		SceneManager.LoadScene(sceneName);
	}
}
