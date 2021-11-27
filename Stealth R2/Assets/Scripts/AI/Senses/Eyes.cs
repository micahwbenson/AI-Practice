using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//My eyes function should SORT of work now, but I think there is going to be some weird stuff with some of the vectors -- getting this to work fully definitely is going to help me to really understand what's going on

public class Eyes : Senses
{
    [Range(0, 180)]
    public float FieldOfView = 80f;

    private float FieldOfViewDot;

    // Start is called before the first frame update
    void Start()
    {
        //Remapping the field of view to a 0 to 1 value so it can easily be compared to a dot product and assess if an enemy is in view
        FieldOfViewDot = 1 - Remap(0f, 90f, 0f, 1f, FieldOfView);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float Remap(float iMin, float iMax, float oMin, float oMax, float v)
    {
        float t = Mathf.InverseLerp(iMin, iMax, v);
        return Mathf.Lerp(oMin, oMax, t);
    }

    //Direction Function - Normalises any vectors you are calculating with so you can get a usable DOT product for comparison
    private Vector2 Direction(Vector2 from, Vector2 to)
    {
        return (from - to).normalized;
    }

    //Overall, the detectable component is what tracks the player's location here, making it easier to run checks and calculations against it
    private bool IsInVisibleArea(Detectable detectable)
    {
        float distance = Vector2.Distance(detectable.transform.position, this.transform.position);
        //Going to write this arrow operator out the old-school way to make sure that I fully understand it . . . ok, why do I need the forward vector from the transform for this operation to work????? -- not sure its going to work with this forward operator, msay need to rerite this function a little
        //I'm not totally sure how I will manage the enemy direction via this right vector, but will figure it out
        if (distance <= Distance && Vector2.Dot(Direction(detectable.transform.position, this.transform.position), this.transform.right) >= FieldOfViewDot)
        {
            return true;
        }

        return false;
    }

    private bool IsNotOccluded(Detectable detectable)
    {
        //Haza! I have solved it for a 2D context, brilliant
        RaycastHit2D hit = Physics2D.Raycast(transform.position, detectable.transform.position - transform.position, Distance);

        if (hit.collider.gameObject.Equals(detectable.gameObject))
        {
            return true;
        }

        return false;
    }

    //If both are true this will return true into the parent senses class
    protected override bool HasDetected(Detectable detectable)
    {
        //Checks if the player object, i.e. detectable component, is within a detectable radius and isn't currently occluded by any other objects in the scene -- if both are true, then HasDetected will return true
        return IsInVisibleArea(detectable) && IsNotOccluded(detectable);
    }

    //Ugh, this isn't going to work until I understand how this is all functioning as a part of the overall AI logic . . . hmmm
    private void OnDrawGizmos(Detectable detectable)
    {
        Vector2 position = transform.position;
        Vector2 playerPos = detectable.transform.position;
    }

}
