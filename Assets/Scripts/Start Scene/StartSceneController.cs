using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartSceneController : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public Animator uiAnimator;
    public Text loadingText;
    public GameObject loadingBG;


    [Header("For Logo Video")]
    public GameObject logoVideoObj;
    public GameObject logoObj;
    public GameObject blackImgObj;
    public VideoPlayer logoVideo;
    public Image logoImg;

    [Header("Sounds")]
    public SoundManager soundManager;
    public AudioClip btnClickAudio;
    public AudioClip backBtnClickAudio;
    public AudioClip modeBtnClickAudio;
    public AudioClip loadingAudio;

    void Start()
    {
        loadingText.text = "";
        PlayerPrefs.SetInt("mode", 0);
        PlayerPrefs.Save();
        StartCoroutine(PlayLogoVideo());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartBtnOnClicked()
    {
        soundManager.CreateSound(btnClickAudio);
        uiAnimator.SetTrigger("SelectMode");
    }

    public void BackBtnOnClicked()
    {
        soundManager.CreateSound(backBtnClickAudio);
        uiAnimator.SetTrigger("back");
    }

    public void ModeBtnOnClicked(int mode)
    {
        soundManager.CreateSound(modeBtnClickAudio);
        PlayerPrefs.SetInt("mode", mode);
        PlayerPrefs.Save();
        StartCoroutine(Loading());
    }

    public void AcheveimentBtnOnClicked()
    {
        soundManager.CreateSound(btnClickAudio);
        uiAnimator.SetTrigger("acheveiment");
    }

    string[] loadingAniText = new string[] { "正在載入演算法...","演算法等級：進階",
        "正在搜尋使用者...","正在搜尋使用者...","已找到使用者!",
        "正在連線使用者的家用監視器...","正在連線使用者的電腦...","正在渲染電腦畫面...","連線成功!"};
    IEnumerator Loading()
    {
        soundManager.CreateSound(loadingAudio);
        uiAnimator.SetTrigger("load");
        for (int i = 0; i < loadingAniText.Length; i++)
        {
            foreach (char c in loadingAniText[i].ToCharArray())
            {
                loadingText.text += c;
                yield return new WaitForSeconds(0.01f);
            }
            loadingText.text += "\n";
            if (i == 1 || i == 4 || i == 9)
                yield return new WaitForSeconds(0.1f);
            else
                yield return new WaitForSeconds(0.5f);
        }
        uiAnimator.SetTrigger("start");
        yield return new WaitForSeconds(3f);
        //loadingBG.SetActive(true);
        //yield return new WaitForFixedUpdate();
        //loadingBG.GetComponent<Image>().CrossFadeAlpha(1f,1f,false);
        SceneManager.LoadScene("Game");
    }
    IEnumerator PlayLogoVideo()
    {
        yield return new WaitWhile(() => !logoVideo.isPrepared);
        logoVideo.Pause();
        //logoVideo.time = 0;
        yield return new WaitForSeconds(0.5f);
        blackImgObj.SetActive(false);
        logoVideo.Play();
        yield return new WaitForSeconds(2.5f);
        logoVideoObj.SetActive(false);
        logoImg.CrossFadeAlpha(0f, 2f, true);
        yield return new WaitForSeconds(2f);
        logoObj.SetActive(false);
    }
}
