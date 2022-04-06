using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Vertical Movement")]
    [SerializeField] float jumpMaxHeight = 5f;
    [SerializeField] float jumpCancelRate = 100f;
    [Header("Horizontal Movement")]
    [SerializeField] float moveSpeed = 8f;

    Rigidbody2D rb;
    Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    // horizontal movement
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // vertical movement
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Jump();
        }
        else
        {
            StartCoroutine(CancelJump());
        }

    }

    // jump impulse
    void Jump()
    {
        // conservation of mechanical energy: mgh=0.5*(mv^2)
        float vy = Mathf.Sqrt(2 * -Physics2D.gravity.y * rb.gravityScale * jumpMaxHeight);
        rb.velocity += Vector2.up * vy;
    }

    // cancel jumping
    IEnumerator CancelJump()
    {
        while (rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * jumpCancelRate);
            yield return null;
        }
    }

    // set horizontal velocity by moveInput
    void Run()
    {
        rb.velocity = new Vector2(moveSpeed * moveInput.x, rb.velocity.y);
    }
}
