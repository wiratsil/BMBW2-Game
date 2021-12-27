using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class ChangeLangEvent : UnityEvent{}

[RequireComponent(typeof(TextMeshProUGUI))]
public class InputLanguage : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    public static ChangeLangEvent changeLangEvent = new ChangeLangEvent();

    public string THA;
    [Space]
    public string ENG;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        changeLangEvent.AddListener(ChangeLanguage);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLanguage()
    {
        if (MultiLanguage.Instance.language == MultiLanguage.Language.THA)
        {
            textMeshProUGUI.text = THA;
        }
        else
        {
            textMeshProUGUI.text = ENG;
        }

        textMeshProUGUI.text = textMeshProUGUI.text.Replace("\\n", "\n");
    }

}
