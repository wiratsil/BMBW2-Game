using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwShake : MonoBehaviour
{
    private RectTransform rectTransform;

    public float duration = 1;
    public float str = 1;
    public int vib = 10;
    public float ran = 90;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.DOShakeScale(duration , strength:str , vibrato:vib, randomness:ran, fadeOut:false).SetLoops(-1);
    }
}
