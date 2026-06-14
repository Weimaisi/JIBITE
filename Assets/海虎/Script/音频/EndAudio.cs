using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAudio : MonoBehaviour
{
    public AudioClip musicClip; // 在 Inspector 中指定音频剪辑
    public AudioSource audioSource;

    private void Start()
    {
        // 设置音频剪辑
        audioSource.clip = musicClip;
        audioSource.loop = false; // 设置循环播放
        audioSource.Play(); // 播放音乐
    }

    private void OnDestroy()
    {
        audioSource.Stop();

    }
}
