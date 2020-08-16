using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayDetection : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorDefault;
    

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null) {
            GameObject selection = hit.collider.gameObject;
            if (selection.CompareTag("Card")) {
                DisplayTitle(hit.collider);
                Debug.Log("card");
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
            if (selection.CompareTag("Interactable")) {
                DisplayTitle(hit.collider);
            }
        }
    }

    private void DisplayTitle(Collider2D collide) {
        // Transform titleHolder = collide.gameObject.transform.Find("Title");
        // TextMeshProUGUI title = titleHolder.GetComponentInChildren<TextMeshProUGUI>();
        // title.text = collide.gameObject.name;
    }
}
