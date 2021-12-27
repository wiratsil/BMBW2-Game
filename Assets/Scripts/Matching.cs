using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Matching : MonoBehaviour
{
    public Animator animator;
    public CanvasGroup canvasGroup;

    public Button buttonA;
    public Button buttonB;

    public int indexA;
    public int indexB;

    [System.Serializable]
    public class Match
    {
        public int a;
        public int b;
    }
    public Match[] match;

    public Button[] buttons;
    public UILineRenderer line;
    public List<GameObject> lineList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoPlay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectButA(Button button)
    {
        buttonA = button;
    }

    public void SelectButB(Button button)
    {
        buttonB = button;
    }
    public void ButtonIndexA(int index)
    {
        indexA = index;
    }
    public void ButtonIndexB(int index)
    {
        indexB = index;
    }

    public void Check()
    {
        if (buttonA == null || buttonB == null)
            return;


        UILineRenderer clone = Instantiate(line.gameObject, transform).GetComponent<UILineRenderer>();
        clone.Points = new Vector2[2];
        clone.Points[0] = buttonA.GetComponent<RectTransform>().anchoredPosition;
        clone.Points[1] = buttonB.GetComponent<RectTransform>().anchoredPosition;
        clone.Points[0].x += 150;
        clone.Points[1].x -= 150;
        clone.gameObject.SetActive(true);
        lineList.Add(clone.gameObject);

        buttonA.interactable = false;
        buttonB.interactable = false;

        buttonA = null;
        buttonB = null;

        for (int i = 0; i < match.Length; i++)
        {
            if (match[i].a == indexA && match[i].b == indexB)
            {
                //Correct
                Debug.Log("Correct");
                animator.SetTrigger("Correct");
                clone.color = Color.green;
                return;
            }
        }

        //Wrong
        Debug.Log("Wrong");
        animator.SetTrigger("Wrong");
    }

    public void Reset()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        foreach (GameObject g in lineList)
            Destroy(g);
    }

    IEnumerator AutoPlay()
    {
        yield return new WaitUntil(() => canvasGroup.alpha == 1 && SoundSystem.Instance.playVocal) ;
        //yield return new WaitForSeconds(24);
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitForSeconds(1);

        buttons[match[0].a - 1].onClick.Invoke();
        buttons[match[0].b + 4].onClick.Invoke();

        //yield return new WaitForSeconds(6);
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitForSeconds(1);

        buttons[match[1].a - 1].onClick.Invoke();
        buttons[match[1].b + 4].onClick.Invoke();

        //yield return new WaitForSeconds(6);
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitForSeconds(1);

        buttons[match[2].a - 1].onClick.Invoke();
        buttons[match[2].b + 4].onClick.Invoke();

        //yield return new WaitForSeconds(7);
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitForSeconds(1);

        buttons[match[3].a - 1].onClick.Invoke();
        buttons[match[3].b + 4].onClick.Invoke();

        //yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitUntil(() => SoundSystem.Instance.audioClipPlaying.Contains("11-2"));
        SoundSystem.Instance.audioClipPlaying = "";
        yield return new WaitForSeconds(1);

        buttons[match[4].a - 1].onClick.Invoke();
        buttons[match[4].b + 4].onClick.Invoke();

    }

    public void Answer()
    {
        StartCoroutine(Ans());

    }

    IEnumerator Ans()
    {
        yield return null;

        buttons[match[0].a - 1].onClick.Invoke();
        buttons[match[0].b + 4].onClick.Invoke();

        yield return new WaitForSeconds(1);

        buttons[match[1].a - 1].onClick.Invoke();
        buttons[match[1].b + 4].onClick.Invoke();

        yield return new WaitForSeconds(1);

        buttons[match[2].a - 1].onClick.Invoke();
        buttons[match[2].b + 4].onClick.Invoke();

        yield return new WaitForSeconds(1);

        buttons[match[3].a - 1].onClick.Invoke();
        buttons[match[3].b + 4].onClick.Invoke();

        yield return new WaitForSeconds(1);

        buttons[match[4].a - 1].onClick.Invoke();
        buttons[match[4].b + 4].onClick.Invoke();
    }
}
