using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndControl : MonoBehaviour
{
    public CanvasGroup end1;
    public CanvasGroup end2;
    public CanvasGroup end3;
    
    void Start()
    {
        end1.alpha = 0;
        end2.alpha = 0;
        end3.alpha = 0;
    }
    
    void Update()
    {
        if (GameControl.Instance.CoinCount <= 1)
        {
            end1.alpha = 1;
        }
        else if (GameControl.Instance.CoinCount == 2)
        {
            end2.alpha = 1;
        }
        else
        {
            end3.alpha = 1;
        }
    }

    public void BackStart()
    {
        UIMgr.Instance.PlaySoundInEnd();
        SceneManager.LoadScene("StartScenes");
    }

    public void Restart()
    {
        UIMgr.Instance.PlaySoundInEnd();
        SceneManager.LoadScene("Level1");
    }
}
