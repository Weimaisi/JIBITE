using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMgr : MonoBehaviour
{
    // 动画帧数组
    public Sprite[] frames;
    // 播放的时间（秒）
    public float animationDuration = 1f;

    // 当前时间，跟踪动画播放时间
    private float timer;
    // 当前帧索引
    private int currentFrame;
    private SpriteRenderer spriteRenderer;

    // 渐变的持续时间（秒）
    public float fadeDuration = 0.2f;
    private float fadeTimer;

    void Start()
    {
        // 获取 SpriteRenderer 组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = 0f;
        fadeTimer = 0f;
        currentFrame = 0;
        // 初始为完全透明
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    void Update()
    {
        // 累加时间
        timer += Time.deltaTime;
        fadeTimer += Time.deltaTime;

        // 计算当前应该显示的帧
        if (timer < animationDuration)
        {
            currentFrame = Mathf.FloorToInt((timer / animationDuration) * frames.Length);
            spriteRenderer.sprite = frames[currentFrame];
        }

        // 渐进渐出效果
        // 计算当前透明度：前后逐渐透明，中间逐渐不透明
        float alpha = 0f;
        if (timer < fadeDuration)
        {
            // 动画开始时，逐渐从透明到不透明
            alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
        }
        else if (timer > animationDuration - fadeDuration)
        {
            // 动画结束时，逐渐从不透明到透明
            alpha = Mathf.Lerp(1f, 0f, (timer - (animationDuration - fadeDuration)) / fadeDuration);
        }
        else
        {
            // 动画中间时，保持完全不透明
            alpha = 1f;
        }

        // 应用透明度
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }
}