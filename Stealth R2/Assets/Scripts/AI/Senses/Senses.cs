using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Senses : MonoBehaviour
{
    public Detectable Detectable; //The player object, essentially but will be a value true or false
    //The distance between the AI and the Detectable player object -- used in part for a threshold check of both eyes and ears
    public float Distance;

    //Not sure, but I think this is set when the AI is activately detecting a player object -- protected so it can be set by each of the children classes (eyes & ears)
    protected bool IsSensing;

    //I don't know what this does yet, but will learn soon enough . . .
    public bool IsDetectionContinuous = true;

    //The way these actions work always confuse me -- conceptually they are a bit hard to wrap ym head around
    public UnityAction<Detectable> OnDetect;
    public UnityAction<Detectable> OnLost;

    private void Detect(Detectable detectable)
    {
        IsSensing = true;
        //I think this just sets detectable to true, interesting though . . . what's the benefit of using an action like this?
        OnDetect?.Invoke(detectable);
    }

    private void Lost(Detectable detectable)
    {
        IsSensing = false;
        OnLost?.Invoke(detectable);
    }

    private void Update()
    {
        if (IsSensing)
        {
            if (!HasDetected(Detectable))
            {
                Lost(Detectable);
                return;
            }

            if (IsDetectionContinuous)
            {
                Detect(Detectable);
            }
        }
        else
        {
            if (!HasDetected(Detectable))
                return;

            Detect(Detectable);
        }
    }

    protected virtual bool HasDetected(Detectable detectable) => false;
}
