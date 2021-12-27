using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Page4 : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoPop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator AutoPop()
    {
        yield return new WaitUntil(() => SoundSystem.Instance.playVocal);

        popup1.SetActive(false);
        popup2.SetActive(false);
        popup3.SetActive(false);
        while (canvasGroup.alpha == 1 && !popup1.activeInHierarchy)
        {
            if (SoundSystem.Instance.audioClipPlaying.Contains("4"))
            {
                SoundSystem.Instance.audioClipPlaying = "";

                popup1.transform.localScale = new Vector3(0, 0, 0);
                popup1.transform.DOScale(1, 1);
                popup1.SetActive(true);
            }
            yield return null;
        }
        while (canvasGroup.alpha == 1 && !popup2.activeInHierarchy)
        {
            if (SoundSystem.Instance.audioClipPlaying.Contains("4"))
            {
                SoundSystem.Instance.audioClipPlaying = "";

                popup2.transform.localScale = new Vector3(0, 0, 0);
                popup2.transform.DOScale(1, 1);
                popup2.SetActive(true);
            }
            yield return null;
        }
        while (canvasGroup.alpha == 1 && !popup3.activeInHierarchy)
        {
            if (SoundSystem.Instance.audioClipPlaying.Contains("4"))
            {
                SoundSystem.Instance.audioClipPlaying = "";

                popup3.transform.localScale = new Vector3(0, 0, 0);
                popup3.transform.DOScale(1, 1);
                popup3.SetActive(true);
            }
            yield return null;
        }
        yield return new WaitUntil(() => canvasGroup.alpha == 0);
        popup1.SetActive(false);
        popup2.SetActive(false);
        popup3.SetActive(false);
        yield return new WaitUntil(() => canvasGroup.alpha == 1);
        StartCoroutine(AutoPop());
    }

}
