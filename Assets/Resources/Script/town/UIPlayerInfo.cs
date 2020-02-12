using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using main;

public class UIPlayerInfo : MonoBehaviour {
	[SerializeField]
	public Text textTroopCount;
	public Text textGoldCount;

	// Update is called once per frame
	void Update () {
		textTroopCount.text = "兵力　：　" + GameManager.instance.playerFieldParam.troopCount;
		textGoldCount.text = "資金　：　" + GameManager.instance.playerFieldParam.gold;
	}
}
