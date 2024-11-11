using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int playerSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 dirX;
    private Vector2 dirY;
    private Vector3 transformScale;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 5;
        transformScale = transform.localScale;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = 5;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = new Vector2(playerSpeed, 0);
        dirY = new Vector2(0 ,playerSpeed);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            move();   
        } else
        {
            animator.SetTrigger("StopMoving");
            animator.ResetTrigger("StartMoving");
        }
    }

    private void move()
    {
        animator.SetTrigger("StartMoving");
        animator.ResetTrigger("StopMoving");


        if (Input.GetKey(KeyCode.A))
        {
            transformScale.x = -Mathf.Abs(transformScale.x);
            moveDirection("left");
            transform.localScale = transformScale;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transformScale.x = Mathf.Abs(transformScale.x);
            moveDirection("right");
            transform.localScale = transformScale;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection("up");
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection("down");
        }
    }

    private void moveDirection(string direction)
    {
        switch (direction)
        {
            case "up":
                rb.AddForce(dirY * Time.deltaTime, ForceMode2D.Impulse);
                break;
            case "down":
                rb.AddForce(-dirY * Time.deltaTime, ForceMode2D.Impulse);
                break;
            case "left":
                rb.AddForce(-dirX * Time.deltaTime, ForceMode2D.Impulse);
                break;
            case "right":
                rb.AddForce(dirX * Time.deltaTime, ForceMode2D.Impulse);
                break;
        }
    }
}
