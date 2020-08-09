using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShibaControl : MonoBehaviour
{
    public float idleAnimation = 0f;
    public Animator anim;

    public Vector3 mousePosition;
    public bool isMoving = false;

    public Rigidbody2D rb;

    void Update() {
        idleAnimation += Time.deltaTime;
        if (idleAnimation > Random.Range(8, 12)) {
            anim.SetTrigger("isSleeping");
        } 

        if (Input.GetMouseButtonDown(0)) {
            idleAnimation = 0;
            SetTargetPosition();
        }
        if (isMoving) {
            Move();
        }
    }

    public void SetTargetPosition() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        anim.ResetTrigger("isSleeping");
        isMoving = true;
        if (mousePosition.x < transform.position.x) {
            anim.SetTrigger("isMove");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if (mousePosition.x >= transform.position.x) {
            anim.SetTrigger("isMove");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Move() {
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, 3.5f*Time.deltaTime);
        if (transform.position == mousePosition) {
            anim.ResetTrigger("isMove");
            isMoving = false;
        }
        
    }
}
