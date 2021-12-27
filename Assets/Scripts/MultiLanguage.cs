using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLanguage : Singleton<MultiLanguage>
{
    public enum Language { THA , ENG }
    public Language language;

    // Start is called before the first frame update
    void Start()
    {
        if (language == Language.THA)
            ToTHA();
        else
            ToENG();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToTHA()
    {
        language = Language.THA;
        InputLanguage.changeLangEvent.Invoke();
    }
    public void ToENG()
    {
        language = Language.ENG;
        InputLanguage.changeLangEvent.Invoke();
    }
}
