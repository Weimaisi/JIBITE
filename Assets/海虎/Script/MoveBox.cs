using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    public LayerMask decectlayer;
    public LayerMask decectlayerAll;
    public int boxID;
    // 重点依据这个变量来进行同步分组
    public int mapCorrespondenceID;
    
    // 引用Sprite Renderer
    private SpriteRenderer spriteRenderer;

    // 可在Inspector中设置的Sprite
    public Sprite newSprite;
    //旧图
    public Sprite oldSprite;
    
    public Animator animator;

    private void Awake()
    {
        // 调用BoxManager的注册方法，传入mapCorrespondenceID来进行分组注册
        BoxManager.RegisterBox(mapCorrespondenceID, this.gameObject);
    }

    private void Start()
    {
        // 获取物体上的Sprite Renderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (boxID != GameControl.Instance.moveBoxID)
        {
            // 如果Sprite Renderer存在，则更换为新的雪碧图
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
        else
        {
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = oldSprite;
            }
        }
    }

    public bool MoveToBox(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.5f, decectlayer); // 调整距离为 1.0f
        if (!hit && boxID == GameControl.Instance.moveBoxID)
        {
            List<GameObject> sameMapCorrespondenceIDObjects = BoxManager.GetBoxesWithID(mapCorrespondenceID);
            foreach (GameObject obj in sameMapCorrespondenceIDObjects)
            {
                MoveBox otherMoveBox = obj.GetComponent<MoveBox>();
                if (otherMoveBox != null && otherMoveBox.boxID == this.boxID)
                {
                    Vector2 offset = dir.normalized * 0.54f; // 调整偏移为 0.51f

                    RaycastHit2D hitall = Physics2D.Raycast((Vector2)transform.position + offset, dir, 0.5f, decectlayerAll); // 调整距离为 0.5f
                    Debug.DrawRay((Vector2)transform.position + offset, dir * 0.5f, Color.red, 5f);

                    if (hitall && hitall.collider != null && hitall.collider.tag == "Box")
                    {
                        return false;
                    }
                    otherMoveBox.transform.Translate(dir);
                    animator.SetTrigger("IsPush");
                }
            }
            return true;
        }

        return false;
    }

}