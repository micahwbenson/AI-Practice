using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FieldOfView : MonoBehaviour
{
    public Detectable detectable;
    public float angleThreshold = 30f;
    public float radius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Eyes();
        Ears();
    }

    private void Ears()
    {
        if (IsHeard())
        {
            Debug.Log("I hear you!");
        }
    }

    private void Eyes()
    {
        if (IsInVisibleArea(detectable) && IsOccluded(detectable))
        {
            Debug.Log("I see you!");
        }
    }

    private bool IsHeard()
    {
        return Vector2.Distance(detectable.transform.position, transform.position) <= radius && detectable.CanBeHear;
    }

    private bool IsOccluded(Detectable detectable)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, detectable.transform.position - transform.position, radius);
        Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        if (hitInfo.collider.gameObject.Equals(detectable.gameObject))
        {
            return true;
        }

        return false;
    }

    private bool IsInVisibleArea(Detectable detectable)
    {
        Vector2 targetDir = detectable.transform.position - transform.position;

        Vector2 lookDir = transform.right;
        Vector2 distance = transform.position - detectable.transform.position;

        //Getting the angle in degrees - refresh on the Math of how this works -- visualise the field of view that you are creating
        float angle = Vector2.Angle(targetDir, lookDir);
        if (angle < angleThreshold && distance.magnitude < radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 lookDir = transform.right;
        Vector2 enemyPos = transform.position;
        //Vector2 angle = Vector2
        Vector2 targetDir = (detectable.transform.position - transform.position).normalized;

        //Vector2 angleVector = Mathf.Tan(angleThreshold));
        //Vector2 negativeAngleVector = 

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, enemyPos + lookDir);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, enemyPos + targetDir);

        Gizmos.color = Color.blue;
        Handles.DrawWireDisc(enemyPos, new Vector3(0, 0, 1),  radius);
    }
}
