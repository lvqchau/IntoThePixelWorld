using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public SpriteRenderer mouseRenderer;
    public Sprite mouseSprite;

    public void HoverSound() {
        myFx.volume = 0.9f;
        myFx.PlayOneShot(hoverFx);
        if (mouseRenderer)
            mouseRenderer.sprite = mouseSprite;
    }

    public void OnMouseLeave() {
        if (mouseRenderer)
            mouseRenderer.sprite = null;
    }

    public void ClickSound() {
        myFx.volume = 0.3f;
        myFx.PlayOneShot(clickFx);
        if (mouseRenderer)
            mouseRenderer.sprite = mouseSprite;
    }

}
