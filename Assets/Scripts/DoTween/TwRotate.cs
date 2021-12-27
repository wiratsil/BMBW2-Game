using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwRotate : MonoBehaviour
{
    public float radius;
    public bool inverted;
    public float duration = 1;

    private Vector3 rotA;
    private Vector3 rotB;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rotA = new Vector3(0, 0, rectTransform.rotation.z - (radius / 2));
        rotB = new Vector3(0, 0, rectTransform.rotation.z + (radius / 2));

        if(!inverted)
            Step1();
        else
            Step2();
    }

    public void Step1()
    {
        rectTransform.DORotate(rotA, duration / 2, RotateMode.Fast).OnStepComplete(Step2) ;
    }
    public void Step2()
    {
        rectTransform.DORotate(rotB, duration / 2, RotateMode.Fast).OnStepComplete(Step1);
    }
}
