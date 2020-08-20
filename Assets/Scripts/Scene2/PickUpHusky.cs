using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpHusky : MonoBehaviour
{
    private Inventory inventory;
    public Sprite sp = null;
    private ShibaControl shibaScript;
    public GameObject[] NPC;
    public Texture2D cursorDefault;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        GameObject husky = GameObject.Find("husky");
        shibaScript = husky.GetComponent<ShibaControl>();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "chest") {
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
        if (sp)
            AddItemToInventory(sp);
    }

    public void AddItemToInventory(Sprite item)
    {

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
                    if (item.name == "Yarn Ball")
                    {
                        DKatty kattyScript = NPC[0].GetComponent<DKatty>();
                        kattyScript.setKeyCondition("doneWool");
                    }
                }
                switch (sp.name) {
                    case "Crumpled Paper": 
                        SpriteRenderer paperObject = gameObject.GetComponent<SpriteRenderer>();
                        paperObject.sprite = Resources.Load<Sprite>("yarn-ball");
                        break;
                    case "chest":
                        break;
                    default:
                        Destroy(gameObject);
                        break;
                }
                Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                break;
            }
        }
    }

    public void RemoveItemInInventory(string itemString)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].sprite && inventory.slots[i].sprite.name == itemString)
            {
                inventory.isFull[i] = false;
                inventory.slots[i].sprite = null;
                break;
            }
        }
    }
}
