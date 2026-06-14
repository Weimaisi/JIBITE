using UnityEngine;
using System.Collections;
public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 引用 CanvasGroup 组件
    public float fadeDuration = 2f; // 渐变的持续时间（单位：秒）

    private void Start()
    {
        StartCoroutine(FadeIn()); // 启动协程，开始渐变过程
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f; // 已经过的时间

        // 初始时，CanvasGroup 的透明度设置为 0（完全透明）
        canvasGroup.alpha = 0f;

        // 渐变过程：将透明度从 0 过渡到 1（不透明）
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; // 累加经过的时间
            // 使用 Mathf.Lerp 线性插值，从 0 到 1 逐渐过渡透明度
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null; // 等待下一帧
        }

        // 渐变完成后，如果需要，可以禁用 CanvasGroup 或其他操作
    }
}