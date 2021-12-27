using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioList : MonoBehaviour
{
    public AudioLanguage[] audiosList;
    [System.Serializable]
    public class AudioLanguage
    {
        public AudioClip thai;
        public AudioClip eng;
    }

}
