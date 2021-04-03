using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private float jumpSpeed;
    private Rigidbody rb;
    private int jumpCount = 0;
    private float movementX;
    private float movementY;
    private int count;
    private bool isOnGround = true;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        //Moving the ball
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    //Code for testing double jump

    void OnJump(InputValue value)
    {
        //If the ball is on the ground or jump once.
        if (jumpCount < 2 || isOnGround)
        {
            jumpCount++;
            jumpSpeed = 50.0f;
            isOnGround = false;
            print("Jump Count: " + jumpCount);
        }
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, jumpSpeed, movementY);
        rb.AddForce(movement * speed);
        jumpSpeed = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
        }
        
    }
}
