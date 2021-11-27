using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rotationSpeed = 15f;
    public float distance = 5f;
    //public Transform enemyTF;
    //Vector2 enemyLookDirection = enemyTF.right;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        Vector2 position = transform.position;
        Vector2 lookDirection = transform.right;
        Debug.DrawLine(position, position + lookDirection, Color.red, 5f);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if(hitInfo.collider != null)
        {
            //Draw a red line
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            //This is where you would transition into a chase state

            if (hitInfo.collider.CompareTag("Player"))
            {
                Destroy(hitInfo.collider.gameObject);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }
    }

    private void Rotation()
    {
        Vector2 position = transform.position;
        Vector2 lookDirection = transform.right;


        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        
        //Revisit this to make an ocisliating enemy
        //Debug.Log(Vector2.Dot(position.normalized, lookDirection.normalized));
        //if (Vector2.Dot(position.normalized, lookDirection.normalized) > 0.25f)
        //{
        //    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        //}
    }

    Vector2 WorldToLocal(Vector2 worldSpacePt)
    {
        Vector2 objPos = transform.position;
        Vector2 right = transform.right;
        Vector2 up = transform.up;

        //Vector2 objPos = objTf.transform.position;
        Vector2 relativePt = (worldSpacePt - objPos);

        float x = Vector2.Dot(relativePt, right);
        float y = Vector2.Dot(relativePt, up);

        return new Vector2(x, y);
    }
}
