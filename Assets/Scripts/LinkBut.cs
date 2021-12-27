using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinkBut : MonoBehaviour
{
    TextMeshProUGUI text;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenLink);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenLink()
    {
        Application.OpenURL(text.text);
    }
}
