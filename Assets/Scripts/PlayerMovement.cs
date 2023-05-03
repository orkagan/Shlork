using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private Vector2 mousePos;
    private Vector2 velocity;
    private Vector2 moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - (Vector2)transform.position;
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDir * 20 * moveSpeed * Time.fixedDeltaTime;
    }
}
