using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AutoPopup : MonoBehaviour
{
    [System.Serializable]
    public class BubleSet
    {
        public GameObject bubble;
        public AudioClip audioClipTH;
        public AudioClip audioClipENG;
    }
    [SerializeField]
    public List<BubleSet> bubleSets = new List<BubleSet>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForAudio());
    }

    IEnumerator WaitForAudio ()
    {
        yield return new WaitUntil(() => SoundSystem.Instance.playVocal);

        for (int i = 0; i < bubleSets.Count; i++)
        {
            bubleSets[i].bubble.SetActive(false);
        }

        for (int j = 0; j < bubleSets.Count; j++)
        {
            yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying == bubleSets[j].audioClipTH.name
            || SoundSystem.Instance.audioClipPlaying == bubleSets[j].audioClipENG.name);

            SoundSystem.Instance.audioClipPlaying = "";
            bubleSets[j].bubble.transform.localScale = new Vector3(0, 0, 0);
            bubleSets[j].bubble.transform.DOScale(1, 1);
            bubleSets[j].bubble.SetActive(true);
        }
    }
}
