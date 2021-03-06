﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public Sprite sp = null;
    //public SpriteRenderer itemButton;
    private ShibaControl shibaScript;
    public GameObject[] NPC;
    public Texture2D cursorDefault;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        GameObject shiba = GameObject.Find("shiba");
        shibaScript = shiba.GetComponent<ShibaControl>();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (sp && sp.name == "Apple") {
                return;
            } else {
                shibaScript.SetTargetPosition();
                shibaScript.Move();
                StartCoroutine("WaitForDoneMoving");
            }
        }
    }

    IEnumerator WaitForDoneMoving()
    {
        yield return new WaitUntil(() => shibaScript.isMoving == false);
        AddItemToInventory(sp);
    }

    private bool checkInInventory(string name)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].sprite && inventory.slots[i].sprite.name == name)
            {
                return true;
            }
        }
        return false;
    }

    public void AddItemToInventory(Sprite item) 
    {
        if (checkInInventory(item.name)) return;
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                if (item == null)
                {
                    inventory.slots[i].sprite = sp;
                }
                else
                {
                    inventory.slots[i].sprite = item;
                    switch (item.name) {
                        case "Unknown Key":
                            DKeyHolder keyHolderScript = NPC[0].GetComponent<DKeyHolder>();
                            keyHolderScript.setKeyCondition("doneKey");
                            break;
                        case "Apple":
                            DCrying cryingScript = NPC[3].GetComponent<DCrying>();
                            cryingScript.setCondition("doneApple");
                            break;
                        default: break;
                    }
                }
                if (gameObject != null)
                    Destroy(gameObject);
                Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                break;
            }
        }
    }

    public void RemoveItemInInventory(string itemString) 
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].sprite) {
                if (inventory.slots[i].sprite.name == itemString)
                {
                    switch (itemString) {
                        case "apple": 
                            DLumberjack lumberScript = NPC[2].GetComponent<DLumberjack>();
                            lumberScript.setCondition("donePeace");
                            break;
                        default: break;
                    }
                    inventory.isFull[i] = false;
                    inventory.slots[i].sprite = null;
                    break;
                }
            }
        }
    }
}
