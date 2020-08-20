using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MouseInventory : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorDefault;
    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI textHolder;

    public GameObject carpetUI;
    private CanvasGroup carpetCanvas;
    private Sprite paperSprite;
    private Image carpetImage;

    private void OnMouseOver() {
        if (spriteRenderer.sprite) {
            textHolder.text = spriteRenderer.sprite.name;
        }
    }

    private void OnMouseDown() {
        if (spriteRenderer.sprite && spriteRenderer.sprite.name == "Crumpled Paper") {
            carpetCanvas.interactable = true;
            carpetCanvas.alpha = 1;
            carpetCanvas.blocksRaycasts = true;
            carpetUI.SetActive(true);
            carpetImage.sprite = Resources.Load<Sprite>("paper-hint");
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
        GameObject canvasObject = GameObject.Find("FortuneCardCanvas");
        if (carpetUI) {
            carpetImage = carpetUI.GetComponentInChildren<Image>();
            carpetCanvas = canvasObject.GetComponent<CanvasGroup>();
        }
    }
}
