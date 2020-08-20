using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DFinalChest : MonoBehaviour
{
    public GameObject carpetUI;
    public CanvasGroup carpetCanvas;
    private string condition;
    private ShibaControl shibaScript;
    private ShibaControl huskyScript;

    //each dialogue has own condition
    //Chest: locked, unlocked

    public void setCondition(string cond)
    {
        condition = cond;
    }

    public string getCondition()
    {
        return condition;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            shibaScript.SetTargetPosition();
            shibaScript.Move();
            huskyScript.SetTargetPosition();
            huskyScript.Move();
            StartCoroutine("WaitForDoneMoving");
        }
    }

    IEnumerator WaitForDoneMoving()
    {
        yield return new WaitUntil(() => (shibaScript.isMoving == false && huskyScript.isMoving == false));
        GameCanvasHandle();
    }

    void Start()
    {
        condition = "locked";
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
        GameObject husky = GameObject.Find("husky");
        huskyScript = husky.GetComponent<ShibaControl>();
    }

    public void StartCarpet()
    {
        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        huskyScript.canMove = false;
        huskyScript.isMoving = false;

        carpetCanvas.interactable = true;
        carpetCanvas.alpha = 1;
        carpetCanvas.blocksRaycasts = true;

        carpetUI.SetActive(true);
        return;
    }

    public void GameCanvasHandle()
    {
        shibaScript.canMove = false;
        shibaScript.isMoving = false;
        huskyScript.canMove = false;
        huskyScript.isMoving = false;
        if (condition == "locked")
        {
            StartCarpet();
            return;
        }
        shibaScript.canMove = true;
        huskyScript.canMove = true;
        shibaScript.anim.SetBool("isMove", false);
        huskyScript.anim.SetBool("isMove", false);  
    }
}
