using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFit : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetDelta();
    }

    private void OnEnable()
    {
        SetDelta();
    }
    // Update is called once per frame
    void Update()
    {

    }

    void SetDelta()
    {
        if (rectTransform == null)
            return;

        rectTransform.anchoredPosition3D = new Vector3();

        if (MultiLanguage.Instance.language == MultiLanguage.Language.THA)
            rectTransform.sizeDelta = new Vector2(0, 6500);
        else
            rectTransform.sizeDelta = new Vector2(0, 7000);
    }
}
