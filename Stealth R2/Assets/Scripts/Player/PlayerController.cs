using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Just copying the player controller for now, because it's not my current focus on this work -- HAHA nvm, this is for 3d movement, lmao -- that also explains why my bool for CanBeHear wasn't working correctly

    [Range(3f, 10f)]
    public float MoveSpeed = 3f;

    private Detectable detectable;

    private Rigidbody2D playerRb;
    Camera viewCamera;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        detectable = GetComponent<Detectable>();
        viewCamera = Camera.main;
    }

    private void Update()
    {
        ////Grabbing the distance from the camera to the object . . .
        //float camDis = viewCamera.transform.position.y - transform.position.y;


        //Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));

        ////Rotating the player now using the angle to the mouse position
        //float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        //float angle = (180 / Mathf.PI) * AngleRad;

        //playerRb.rotation = angle;

        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        detectable.CanBeHear = verticalAxis != 0f || horizontalAxis != 0f;

        transform.Translate(Vector2.up * verticalAxis * MoveSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector2.right * horizontalAxis * MoveSpeed * Time.deltaTime);
    }
}
