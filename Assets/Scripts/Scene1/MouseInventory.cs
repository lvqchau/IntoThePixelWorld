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
    private TextMeshProUGUI textHolder;

    void Update() {
    }

    private void OnMouseOver() {
        if (spriteRenderer.sprite) {
            textHolder.text = spriteRenderer.sprite.name;
        }
    }

    private void OnMouseDown() {
        if (spriteRenderer.sprite && spriteRenderer.sprite.name == "scribble") {
            // openPanel()
        }
    }

    private void OnMouseExit() {
        if (spriteRenderer.sprite) {
            textHolder.text = "";
        }
    }

    void Start() {
        textHolder = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }
}
