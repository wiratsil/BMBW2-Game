using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public enum Colors {None, Yellow, Green, Blue, Red }
public class PaintMode : MonoBehaviour
{
    public Colors colors;
    public Color color;
    public Color colorYellow;
    public Color colorGreen;
    public Color colorBlue;
    public Color colorRed;

    [Space]
    public RectTransform correctTxt;
    public RectTransform wrongTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectColor(int index)
    {
        colors = (Colors)index;
        color = GetColor((Colors)index);
    }

    public Color GetColor(Colors colors)
    {
        Color _color = new Color();
        switch (colors)
        {
            case Colors.Yellow:
                _color = colorYellow;
                break;
            case Colors.Green:
                _color = colorGreen;
                break;
            case Colors.Blue:
                _color = colorBlue;
                break;
            case Colors.Red:
                _color = colorRed;
                break;
        }
        return _color;
    }

    public void Correct()
    {
        correctTxt.gameObject.SetActive(true);
        correctTxt.DOAnchorPos(new Vector2(0, 200), 0.5f).OnStepComplete(() =>
        {
            correctTxt.gameObject.SetActive(false);
            correctTxt.anchoredPosition = new Vector2(0, 0);
        }  ) ;
    }
    public void Wrong()
    {
        wrongTxt.gameObject.SetActive(true);
        wrongTxt.DOAnchorPos(new Vector2(0, 200), 0.5f).OnStepComplete(() =>
        {
            wrongTxt.gameObject.SetActive(false);
            wrongTxt.anchoredPosition = new Vector2(0, 0);
        });
    }

    public void Reset(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }
}
