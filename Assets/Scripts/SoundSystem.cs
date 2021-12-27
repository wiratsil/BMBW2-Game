using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : Singleton<SoundSystem>
{
    public AudioMixer audioMixer;
    [Space]
    public AudioSource vocalSource;
    public bool playVocal = false;
    [System.Serializable]
    public class VocalAudio
    {
        public List<AudioClip> vocalTH;
        public List<AudioClip> vocalENG;
    }
    public List<VocalAudio> vocalAudios;

    private int numPage = 0;

    [Space]

    public AudioSource sfxSource;
    [Space]
    public AudioSource musicSource;

    [Space]
    public AudioClip oncilck;
    public AudioClip bgm;
    [Space]
    public string audioClipPlaying;

    private void Start()
    {
        PlayVocal();
        musicSource.clip = bgm;
        musicSource.Play();

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            SetMusic(true);
        }
        else
        {
            SetMusic(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sfxSource.PlayOneShot(oncilck);
        }
    }

    public void PlayVocal()
    {
        if (!playVocal)
            return;

        vocalSource.Stop();
        StopAllCoroutines();

        List<AudioClip> audioClips = new List<AudioClip>();

        if (MultiLanguage.Instance.language == MultiLanguage.Language.THA)
        {
            audioClips.AddRange(vocalAudios[numPage].vocalTH);
        }
        else
        {
            audioClips.AddRange(vocalAudios[numPage].vocalENG);
        }

        StartCoroutine(PlayVocalSound(audioClips));
    }

    public IEnumerator PlayVocalSound(List<AudioClip> audioClips)
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < audioClips.Count; i++)
        {
            vocalSource.clip = audioClips[i];
            vocalSource.Play();
            audioClipPlaying = vocalSource.clip.name;
            yield return new WaitForSeconds(audioClips[i].length);
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
        GameManager.Instance.NextPage();

    }

    public void AddNumPage()
    {
        numPage++;
        PlayVocal();
    }

    public void MinusNumPage()
    {
        numPage--;
        PlayVocal();
    }

    public void Z_AutoVocal(bool b)
    {
        playVocal = b;
        PlayVocal();
    }

    public void SetMusic(bool b)
    {
        if (b)
        {
            PlayerPrefs.SetInt("Music",1);
        }
        else
            PlayerPrefs.SetInt("Music",0);


        sfxSource.mute = b;
        musicSource.mute = b;
        vocalSource.mute = b;
    }

}
