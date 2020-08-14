using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public SpriteRenderer mouseRender;
    public Sprite mouseSprite;

    public void HoverSound() {
        myFx.volume = 0.9f;
        myFx.PlayOneShot(hoverFx);
        mouseRender.sprite = mouseSprite;
    }

    public void OnMouseLeave() {
        mouseRender.sprite = null;
    }

    public void ClickSound() {
        myFx.volume = 0.3f;
        myFx.PlayOneShot(clickFx);
        mouseRender.sprite = mouseSprite;
    }
}
