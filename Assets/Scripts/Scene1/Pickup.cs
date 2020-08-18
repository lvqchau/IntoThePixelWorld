using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public Sprite sp;
    //public SpriteRenderer itemButton;
    private ShibaControl shibaScript;
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
            shibaScript.SetTargetPosition();
            shibaScript.Move();
            StartCoroutine("WaitForDoneMoving");
        }
    }

    IEnumerator WaitForDoneMoving()
    {
        yield return new WaitUntil(() => shibaScript.isMoving == false);
        AddItemToInventory(null);
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
                Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                break;
            }
        }
    }
}
