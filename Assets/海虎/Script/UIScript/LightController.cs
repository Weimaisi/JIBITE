using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 目标 Canvas Group
    public float fadeDuration = 2f; // 渐变持续时间
    private bool isFadingOut = true; // 当前是否在淡出

    void Start()
    {
        if (canvasGroup == null)
        {
            Debug.LogError("请指定 Canvas Group！");
            return;
        }

        // 开始循环渐变
        StartCoroutine(FadeLoop());
        isFadingOut = true; // 当前是否在淡出
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            float elapsedTime = 0f;
            float startAlpha = canvasGroup.alpha;
            float targetAlpha = isFadingOut ? 0f : 1f;

            // 渐变过程
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
                yield return null;
            }

            // 反转状态，准备下一次渐变
            isFadingOut = !isFadingOut;
        }
    }
}
