using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PickupSoup : MonoBehaviour
{
    private Inventory inventory;
    public Sprite sp = null;
    private ShibaControl shibaScript;
    public GameObject[] NPC;
    public Texture2D cursorDefault;
    private DBird birdScript;

    private void Start()
    {
        GameObject shiba = GameObject.Find("shiba");
        GameObject bird = GameObject.Find("bird");
        inventory = shiba.GetComponent<Inventory>();
        shibaScript = shiba.GetComponent<ShibaControl>();
        birdScript = bird.GetComponent<DBird>();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "crate")
            {
                return;
            }
            else
            {
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
                }
                Destroy(gameObject);
                birdScript.increaseItemCount();
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
