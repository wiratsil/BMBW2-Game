using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichLangText : MonoBehaviour
{
    public GameObject tha;
    public GameObject eng;
    // Start is called before the first frame update
    void Start()
    {
        if (MultiLanguage.Instance.language == MultiLanguage.Language.THA)
        {
            tha.SetActive(true);
            eng.SetActive(false);
        }
        else
        {
            eng.SetActive(true);
            tha.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (MultiLanguage.Instance.language == MultiLanguage.Language.THA)
        {
            tha.SetActive(true);
            eng.SetActive(false);
        }
        else
        {
            eng.SetActive(true);
            tha.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
