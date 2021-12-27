using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoConMode : MonoBehaviour
{
    public GameObject[] popupText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopup()
    {
        foreach (GameObject g in popupText)
            g.SetActive(false);
    }
}
