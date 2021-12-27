using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPainting : MonoBehaviour
{
    private Button button;
    private PaintMode paintMode;
    private Image image;
    public Colors answer;

    public Button buttonAns;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        paintMode = FindObjectOfType<PaintMode>();
        image = GetComponent<Image>();
        button.onClick.AddListener(Paint);
        buttonAns.onClick.AddListener(Ans);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Paint()
    {
        if (answer != paintMode.colors)
        {
            paintMode.Wrong();
            return;
        }

        image.color = paintMode.color;
        paintMode.Correct();
    }

    public void Ans()
    {
        image.color = paintMode.GetColor(answer);
    }
}
