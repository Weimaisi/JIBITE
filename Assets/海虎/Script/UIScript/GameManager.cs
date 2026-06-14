using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
    
    // 标志是否正在播放动画
    private bool isPlayingAnimation = false;

    void Start()
    {
        // 获取 SpriteRenderer 组件
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 通过按钮点击触发的动画播放函数
    public void PlayAnimationAndLoadScene()
    {
        isPlayingAnimation = true;
        timer = 0f;
        currentFrame = 0;
    }

    void Update()
    {
        // 如果正在播放动画
        if (isPlayingAnimation)
        {
            // 累加时间
            timer += Time.deltaTime;

            // 如果动画还没有播放完
            if (timer < animationDuration)
            {
                // 根据时间来计算应该显示哪一帧
                currentFrame = Mathf.FloorToInt((timer / animationDuration) * frames.Length);
                spriteRenderer.sprite = frames[currentFrame];
            }
            else
            {
                // 如果动画播放完了，加载场景
                isPlayingAnimation = false;  // 停止播放动画
                SceneManager.LoadScene("Level1");
            }
        }
    }
}