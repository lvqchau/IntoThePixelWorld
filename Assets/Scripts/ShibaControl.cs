using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShibaControl : MonoBehaviour
{
    public float idleAnimation = 0f;
    public Animator anim;
    public float speed = 3.5f;
    public Vector3 mousePosition;
    public bool isMoving = false;

    private Vector2 velocity;
    private Vector2 movement;
    public Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(speed, speed);
    }

    void Update() {
        idleAnimation += Time.deltaTime;
        if (idleAnimation > Random.Range(8, 12)) {
            anim.SetBool("isSleeping", true);
        } 

        if (Input.GetMouseButtonDown(0)) {
            idleAnimation = 0;
            SetTargetPosition();
        }
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
        if (isMoving) {
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
            anim.SetBool("isMove", false);
            isMoving = false;
        }
    }
}
