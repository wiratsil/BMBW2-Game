using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    public GameObject soundIcon;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            soundIcon.SetActive(true);
        }
        else
        {
            soundIcon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
