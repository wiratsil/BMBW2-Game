using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwScalePlus : MonoBehaviour
{
    public Vector3 minScale = new Vector3 (1, 1, 1);
    public Vector3 maxScale = new Vector3 (1.5f, 1.5f, 1.5f);
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

    public void StepA()
    {
        transform.DOScale(minScale, duration).OnStepComplete(StepB);
    }

    public void StepB()
    {
        transform.DOScale(maxScale, duration).OnStepComplete(StepA);
    }
}
