using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Just copying the player controller for now, because it's not my current focus on this work -- HAHA nvm, this is for 3d movement, lmao -- that also explains why my bool for CanBeHear wasn't working correctly

    [Range(3f, 10f)]
    public float MoveSpeed = 3f;


    private Rigidbody2D playerRb;
    Camera viewCamera;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;
    }

    private void Update()
    {


        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector2.up * verticalAxis * MoveSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector2.right * horizontalAxis * MoveSpeed * Time.deltaTime, Space.World);


    }

    private void FixedUpdate()
    {
        faceMouse();

    }

    private void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        playerRb.rotation = angle;
    }
}
