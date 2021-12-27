using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwPunchScale : MonoBehaviour
{
    private RectTransform rectTransform;

    public Vector3 vector3;
    public float duration = 9999;
    public float ela = 1;
    public int vib = 10;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.DOPunchScale(vector3, duration, vibrato: vib, elasticity: ela);
    }
}
