using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using main;
using UnityEngine.SceneManagement;

public class CommandBackToField : ICommand,IPreCommand {

	public string Tag{
		get{ return "back";}
	}

	public void PreCommand(Dictionary<string,string> command){
	}

	public void Command(Dictionary<string,string>command){
		SceneManager.UnloadScene ("novel");
	}
}