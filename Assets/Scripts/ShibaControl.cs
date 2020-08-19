using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShibaControl : MonoBehaviour
{
    private float idleAnimation = 0f;
    public Animator anim;
    public bool canMove;
    private float speed = 3f;
    private Vector3 mousePosition;
    public bool isMoving = false;
    public AudioSource shibaSrc;
    public AudioClip shibaSleepFx;
    public AudioClip shibaWalkFx;

    private Vector2 velocity;
    private Vector2 movement;
    public Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        velocity = new Vector2(speed, speed);
        Physics.IgnoreLayerCollision(9, 10);
    }

    IEnumerator PlaySounds() {
        shibaSrc.clip = shibaWalkFx;
        if (!shibaSrc.isPlaying) {
            shibaSrc.Play();
        }
        else if (shibaSrc.isPlaying) {
            yield return new WaitForSeconds(1);
            shibaSrc.Stop ();
        }
    }

    void Update() {
        idleAnimation += Time.deltaTime;
        if (idleAnimation > Random.Range(8, 12) && canMove) {
            anim.SetBool("isSleeping", true);
            // shibaSrc.clip = shibaSleepFx;
            // shibaSrc.Play();
        } 

        if (Input.GetMouseButtonDown(0) && canMove) {
            idleAnimation = 0;
            if (!IsOverUI()) {
                SetTargetPosition();
            }
        }
    }

    private bool IsOverUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void SetTargetPosition() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        anim.SetBool("isSleeping", false);
        anim.SetBool("isMove", true);
        isMoving = true;
        if (mousePosition.x < transform.position.x) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if (mousePosition.x >= transform.position.x) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        isMoving = false;
        anim.SetBool("isMove", false);
        rb.velocity = Vector2.zero;
    }


    private void FixedUpdate() {
        if (isMoving && canMove) {
            movement.x = mousePosition.x;
            movement.y = mousePosition.y;
            rb.velocity = movement;
            movement.Normalize();
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
            Move();
        } else {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMove", false);
        }
    }

    public void Move() {
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed*Time.deltaTime);
        if (transform.position == mousePosition) {
            // shibaSrc.Stop();
            anim.SetBool("isMove", false);
            isMoving = false;
        }
    }
}
