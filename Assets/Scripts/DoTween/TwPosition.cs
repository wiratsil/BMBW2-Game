using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwPosition : MonoBehaviour
{
    public Vector3 posA;
    public Vector3 posB;
    public bool inverted;
    public float duration = 1;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (!inverted)
            StepA();
        else
            StepB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StepA ()
    {
        rectTransform.DOAnchorPos3D(posA, duration / 2).OnStepComplete(StepB);
    }

    public void StepB ()
    {
        rectTransform.DOAnchorPos3D(posB, duration / 2).OnStepComplete(StepA);
    }

    public void SetPosA()
    {
        posA = GetComponent<RectTransform>().anchoredPosition3D;
    }

    public void SetPosB()
    {
        posB = GetComponent<RectTransform>().anchoredPosition3D;
    }
}
