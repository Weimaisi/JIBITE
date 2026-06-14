using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{// 当玩家进入金币的碰撞体时触发
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保触发的是玩家，而不是其他物体（如地面检测碰撞器）
        if (other.CompareTag("Player"))
        {
            // 增加金币计数
            GameControl.Instance.CoinCount++;

            // 销毁金币
            Debug.Log("获得一个金币！");
            Destroy(gameObject);  // 销毁当前的金币对象
        }
    }
}
