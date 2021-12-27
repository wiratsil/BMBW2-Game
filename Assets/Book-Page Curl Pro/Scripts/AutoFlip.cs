using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BookPro))]
public class AutoFlip : MonoBehaviour
{
    public BookPro ControledBook;
    public FlipMode Mode;
    public float PageFlipTime = 1;
    public float DelayBeforeStart;
    public float TimeBetweenPages=5;
    public bool AutoStartFlip=true;
    bool flippingStarted = false;
    bool isPageFlipping = false;
    float elapsedTime = 0;
    float nextPageCountDown = 0;

    public int pageNum = 1;
    public GameObject lastPageShow;
    public GameObject lastRecordShow;
    public GameObject lastAniShow;

    // Use this for initialization
    void Start () {
        if (!ControledBook)
            ControledBook = GetComponent<BookPro>();
        ControledBook.interactable = false;
        if (AutoStartFlip)
            StartFlipping();
    }
    public void FlipRightPage()
    {
        if (isPageFlipping) return;
        if (ControledBook.CurrentPaper >= ControledBook.papers.Length - 1)
        {
            if (GameManager.Instance.record)
            {
                StartCoroutine(SetShow()) ;
            }
            else
            {
                lastPageShow.SetActive(true);
            }
            isPageFlipping = false;
            return;
        }

        isPageFlipping = true;
        PageFlipper.FlipPage(ControledBook, PageFlipTime, FlipMode.RightToLeft, ()=> { isPageFlipping = false; });
        pageNum++;
        SoundSystem.Instance.AddNumPage();
    }
    public void FlipLeftPage()
    {
        if (isPageFlipping) return;
        if (ControledBook.CurrentPaper <= 0) return;

        lastPageShow.SetActive(false);
        isPageFlipping = true;
        PageFlipper.FlipPage(ControledBook, PageFlipTime, FlipMode.LeftToRight, () => { isPageFlipping = false; });
        pageNum--;
        SoundSystem.Instance.MinusNumPage();
    }
    public void StartFlipping()
    {
        flippingStarted = true;
        elapsedTime = 0;
        nextPageCountDown = 0;
    }
    void Update()
    {
        if (flippingStarted)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > DelayBeforeStart)
            {
                if (nextPageCountDown < 0)
                {
                    if ((ControledBook.CurrentPaper <= ControledBook.EndFlippingPaper &&
                        Mode == FlipMode.RightToLeft) ||
                        (ControledBook.CurrentPaper > ControledBook.StartFlippingPaper &&
                        Mode == FlipMode.LeftToRight))
                    {
                        isPageFlipping = true;
                        PageFlipper.FlipPage(ControledBook, PageFlipTime, Mode, ()=> { isPageFlipping = false; });
                    }
                    else
                    {
                        flippingStarted = false;
                        this.enabled = false;
                    }

                    nextPageCountDown = PageFlipTime + TimeBetweenPages+ Time.deltaTime;
                }
                nextPageCountDown -= Time.deltaTime;
            }
        }
    }

    IEnumerator SetShow ()
    {
        lastAniShow.SetActive(true);
        yield return new WaitForSeconds(2);
        GameManager.Instance.StopRecord();
        yield return new WaitForSeconds(1);
        lastAniShow.SetActive(false);
        lastRecordShow.SetActive(true);
    }
}
