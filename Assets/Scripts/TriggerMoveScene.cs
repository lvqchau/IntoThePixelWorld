using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerMoveScene : MonoBehaviour
{
    public DLumberjack lumberjackScript;
    public DFortune fortuneScript;
    public DMoveScene moveScript;

    void Update() {
        if (lumberjackScript.getCondition() == "havePeace" && fortuneScript.getCondition() == "haveChosen" ) {
            moveScript.changeCondition();
        }
    }
}
