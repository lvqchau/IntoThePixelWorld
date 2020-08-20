using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour
{
    private ShibaControl shibaScript;
    public CanvasGroup carpetCanvas;
    private DFortuneS2 fortuneScript;
    public GameObject FortuneTeller;
    public GameObject carpetUI;

    void Start()
    {
        if (FortuneTeller) fortuneScript = FortuneTeller.GetComponent<DFortuneS2>();
        GameObject husky = GameObject.Find("husky");
        shibaScript = husky.GetComponent<ShibaControl>();
    }

    private void ClearCarpet()
    {
        if (fortuneScript) {
            if (fortuneScript.getCondition() == "noPlay") {
                fortuneScript.setCondition("playDone");
            }
            fortuneScript.TriggerDialogue();
        }
        carpetCanvas.interactable = false;
        carpetCanvas.alpha = 0;
        carpetCanvas.blocksRaycasts = false;

        carpetUI.SetActive(false);

        shibaScript.canMove = true;
    }

    public void CloseCarpet()
    {
        shibaScript.isMoving = false;
        shibaScript.canMove = false;
        ClearCarpet();
    }
}
