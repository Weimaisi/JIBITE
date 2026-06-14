using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene_Controller : MonoBehaviour
{

    public bool isThanks = false;
    public bool isHelps = false;
    
    public GameObject ThanksPic;
    public GameObject HelpPic;
    
    
    
    public void StartGame()
    {
        UIMgr.Instance.PlaySoundInStart();
        // 直接调用过渡场景切换
        TransitionToScene("Level1");
        // 强制卸载当前场景和过渡场景
        SceneManager.UnloadSceneAsync("TransitionScene");
        SceneManager.UnloadSceneAsync("StartScene");
    }

    public void TransitionToScene(string targetSceneName)
    {
        // 1. 加载过渡场景
        StartCoroutine(LoadSceneWithTransition(targetSceneName));
    }

    private IEnumerator LoadSceneWithTransition(string targetSceneName)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current scene: " + currentSceneName);

        // 检查并卸载 TransitionScene（如果已加载）
        Scene transitionScene = SceneManager.GetSceneByName("TransitionScene");
        if (transitionScene.isLoaded)
        {
            Debug.Log("Unloading TransitionScene...");
            // 等待卸载完成
            yield return SceneManager.UnloadSceneAsync("TransitionScene");
        }

        // 加载 TransitionScene
        Debug.Log("Loading TransitionScene...");
        yield return SceneManager.LoadSceneAsync("TransitionScene", LoadSceneMode.Additive);

        // 异步加载目标场景
        Debug.Log("Loading target scene: " + targetSceneName);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 显示过渡场景一秒钟
        yield return new WaitForSeconds(1f);

        // 卸载过渡场景
        Debug.Log("Unloading TransitionScene after 1 second...");
        if (SceneManager.GetSceneByName("TransitionScene").isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync("TransitionScene");
        }

        // 卸载当前场景（如果目标场景与当前场景不同）
        if (currentSceneName != targetSceneName)
        {
            Debug.Log("Unloading current scene: " + currentSceneName);
            yield return SceneManager.UnloadSceneAsync(currentSceneName);
        }

        Debug.Log("Scene transition complete.");
    }


    public void Thanks()
    {
        if (isThanks)
        {
            UIMgr.Instance.PlaySoundInStart();
            UIMgr.Instance.CloseUI(ThanksPic);
            isThanks = false;
        }
        else
        {
            UIMgr.Instance.PlaySoundInStart();
            UIMgr.Instance.OpenUI(ThanksPic);
            isThanks = true;
            
        }
    }

    public void Help()
    {
        if (isHelps)
        {
            UIMgr.Instance.PlaySoundInStart();
            UIMgr.Instance.CloseUI(HelpPic);
            isHelps = false;
        }
        else
        {
            UIMgr.Instance.PlaySoundInStart();
            UIMgr.Instance.OpenUI(HelpPic);
            isHelps = true;
            
        }
    }

    public void ExitGame()
    {
        // 如果是在编辑器中运行，使用 UnityEditor 的功能退出播放模式
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 在发布的应用中，退出游戏
            Application.Quit();
#endif
    }
}
