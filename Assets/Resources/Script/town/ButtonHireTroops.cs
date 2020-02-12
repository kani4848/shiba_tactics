using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using main;

public class ButtonHireTroops : MonoBehaviour {

    public bool push = false;

    public void Update()
    {
        if (push)
        {
            int gold = GameManager.instance.playerFieldParam.gold;

            if (gold >= 10)
            {
                GameManager.instance.playerFieldParam.troopCount++;
                GameManager.instance.playerFieldParam.gold -= 10;
            }
            else
            {
                GameManager.callScenario = "notEnoughMoney";

            }
        }
    }

    public void PushDown()
    {
        push = true;
    }

    public void PushUp()
    {
        push = false;
    }
}
