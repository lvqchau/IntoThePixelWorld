using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MouseInventory : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorDefault;
    private SpriteRenderer spriteRenderer;

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
        }
    }

    public void OnMouseEnter() {
        if (spriteRenderer.sprite == null) {
            return;
        }
        else if (!EventSystem.current.IsPointerOverGameObject()) {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseDown() {
        if (spriteRenderer.sprite.name == "scribble") {
            // openPanel()
        }
    }

    private void OnMouseExit() {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }
}
