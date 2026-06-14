    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerMove : MonoBehaviour
    {
        private Vector2 movedir;
        public LayerMask layerMask;
        private GameControl gameControl; // 新增，用于存储GameControl单例实例

        public Animator animator;
        public CanvasGroup cantgolevel;
        
        public SpriteRenderer spriteRenderer; // 用于控制精灵翻转
        
        private bool isPushingBox = false; // 新增标记，表示玩家是否正在推方块
        

        private void Start()
        {
            gameControl = GameControl.Instance; // 获取GameControl单例实例
            if (gameControl == null)
            {
                Debug.LogError("GameControl instance not found!");
            }
        }

        private void Update()
        {
            if (Time.timeScale != 0)
            {
                MovePlayer();
                ChangePlayerTransform();
            }
            else
            {
                return;
            }
            
        }
        
        private bool isCoroutineRunning = false;  // 用来标识协程是否在运行

        public void ChangePlayerTransform()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 获取当前物体的当前位置
                Vector3 position = transform.position;
                Vector3 positionnow = transform.position;
                position.y = transform.position.y + 100f;
                float checkRadius = 0.4f;  // 检测半径

                // 使用OverlapCircle检查当前位置附近是否有其他2D Collider
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, checkRadius);

                // 如果检测到其他碰撞体
                if (hitColliders.Length > 0)
                {
                    cantgolevel.alpha = 1;  // 显示文字

                    // 只有在协程未启动时，才启动新的协程
                    if (!isCoroutineRunning)
                    {
                        StartCoroutine(HideTextAfterDelay(1f));
                        isCoroutineRunning = true;  // 标记协程已启动
                    }

                    transform.position = positionnow;
                }
                else
                {
                    GameControl.Instance.GoToNextlevel(GetComponent<PlayerMove>().gameObject);
                    cantgolevel.alpha = 0;
                }
            }
        }

// 定义协程，控制文字显示和消失
        private IEnumerator HideTextAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);  // 等待指定的时间
            cantgolevel.alpha = 0;  // 隐藏文字
            isCoroutineRunning = false;  // 协程结束后，标记协程已停止
        }

        public void MovePlayer()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                movedir = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                movedir = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                movedir = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                movedir = Vector2.right;
            }
            if (movedir!= Vector2.zero)
            {
                if (CanMove(movedir))
                {
                    MovePlayer(movedir);
                    animator.SetTrigger("Move");
                }
                
                // 控制精灵的翻转
                FlipSprite(movedir);

                // 获取玩家控制的方块（这里假设玩家控制的方块上挂载了MoveBox脚本，可以通过合适方式获取，比如直接关联等）
                MoveBox playerMoveBox = GetComponent<MoveBox>();
                if (playerMoveBox!= null)
                {
                    playerMoveBox.MoveToBox(movedir);
                }
                
            }
            movedir = Vector2.zero;
        }

        public bool CanMove(Vector2 movedir)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movedir, 1.0f, layerMask); // 调整距离为 1.0f
            if (!hit)
            {
                return true;
            }
            else
            {
                MoveBox moveBox = hit.collider.gameObject.GetComponent<MoveBox>();
                if (moveBox != null)
                {
                    if (moveBox.boxID == gameControl.moveBoxID)
                    {
                        return moveBox.MoveToBox(movedir);
                    }
                    return false;
                }
            }
            return false;
        }


        public void MovePlayer(Vector2 movedir)
        {
            transform.Translate(movedir);
        }

        public Vector2 ReturnMoveDir()
        {
            return movedir;
        }
        
        private void FlipSprite(Vector2 movedir)
        {
            if (movedir.x < 0 && spriteRenderer != null && spriteRenderer.flipX)
            {
                // 向右移动，图像翻转为朝右
                spriteRenderer.flipX = false;
            }
            else if (movedir.x > 0 && spriteRenderer != null && !spriteRenderer.flipX)
            {
                // 向左移动，图像翻转为朝左
                spriteRenderer.flipX = true;
            }
        }
    }