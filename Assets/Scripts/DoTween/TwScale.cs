using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwScale : MonoBehaviour
{
    public float minScale = 1;
    public float maxScale = 1.5f;
    public float duration = 1;

    // Start is called before the first frame update
    void Start()
    {
        StepA();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StepA ()
    {
        transform.DOScale( new Vector3 (maxScale, maxScale , maxScale) , duration).OnStepComplete(StepB);
    }

    public void StepB()
    {
        transform.DOScale(new Vector3(minScale, minScale, minScale), duration).OnStepComplete(StepA);
    }
}
