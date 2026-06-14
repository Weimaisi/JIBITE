using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneController : MonoBehaviour
{
    public GameObject stopBg;
    private bool isPaused = false;
    private bool isHelps = false;
    
    public GameObject HelpPic;
    
    public GameObject previewButton;
    public GameObject preview;
    private bool isPreview = false;
    public CanvasGroup[] images;  // 三张图片的 CanvasGroup
    public CanvasGroup[] nextImages;  // 三张图片的 CanvasGroup


    private void Awake()
    {
        GameControl.Instance.RestartCG();
    }

    void Start()
    {
        FadeStart();
        GameControl.Instance.ResetAll();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIMgr.Instance.PlaySoundInLevel();
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIMgr.Instance.PlaySoundInLevel();
            Preview();
        }
        ShowImage(GameControl.Instance.moveBoxID - 1);
        ShowNextImage(GameControl.Instance.moveBoxID);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            UIMgr.Instance.PlaySoundInLevel();
            UIMgr.Instance.CloseUI(stopBg);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            UIMgr.Instance.PlaySoundInLevel();
            UIMgr.Instance.OpenUI(stopBg);
            Time.timeScale = 0f;
            isPaused = true;
            
        }
    }

    public void Preview()
    {
        if (isPreview)
        {
            UIMgr.Instance.PlaySoundInLevel();
            UIMgr.Instance.CloseUI(preview);
            UIMgr.Instance.OpenUI(previewButton);
            isPreview = false;
        }
        else
        {
            UIMgr.Instance.PlaySoundInLevel();
            UIMgr.Instance.OpenUI(preview);
            UIMgr.Instance.CloseUI(previewButton);
            isPreview = true;
            
        }
    }

    public void Restart()
    {
        UIMgr.Instance.PlaySoundInLevel();
        // 获取当前场景的名字
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        Time.timeScale = 1f;
        
        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneName);
        
    }
    public void Help()
    {
        if (isHelps)
        {
            UIMgr.Instance.PlaySoundInLevel();
            UIMgr.Instance.CloseUI(HelpPic);
            isHelps = false;
        }
        else
        {
            UIMgr.Instance.PlaySoundInLevel();
            UIMgr.Instance.OpenUI(HelpPic);
            isHelps = true;
            
        }
    }

    public void FadeStart()
    {
        // 初始化图片透明度，确保只有第一张图片可见
        for (int i = 0; i < images.Length; i++)
        {
            images[i].alpha = i == 0 ? 1 : 0;  // 只有第一张图片完全可见
            images[i].interactable = i == 0;   // 只有当前图片可以交互
            images[i].blocksRaycasts = i == 0; // 只有当前图片响应点击
        } 
        
        // 初始化图片透明度，确保只有第一张图片可见
        for (int i = 0; i < nextImages.Length; i++)
        {
            nextImages[i].alpha = i == 0 ? 1 : 0;  // 只有第一张图片完全可见
            nextImages[i].interactable = i == 0;   // 只有当前图片可以交互
            nextImages[i].blocksRaycasts = i == 0; // 只有当前图片响应点击
        }
        
    }

    // 显示指定索引的图片，并隐藏其他图片
    void ShowImage(int index)
    {
        for (int i = 0; i < images.Length; i++)
        {
            // 设置透明度，显示或隐藏图片
            images[i].alpha = (i == index) ? 1f : 0f;
            images[i].interactable = (i == index);
            images[i].blocksRaycasts = (i == index);
        }
    }
    
    // 显示指定索引的图片，并隐藏其他图片
    void ShowNextImage(int index)
    {
        for (int i = 0; i < nextImages.Length; i++)
        {
            // 设置透明度，显示或隐藏图片
            nextImages[i].alpha = (i == index) ? 1f : 0f;
            nextImages[i].interactable = (i == index);
            nextImages[i].blocksRaycasts = (i == index);
        }
    }

    public void Quit()
    {
        UIMgr.Instance.PlaySoundInLevel();
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScenes");
    }
}
