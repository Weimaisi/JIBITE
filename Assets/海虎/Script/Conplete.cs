using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conplete : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保碰撞的是玩家，而不是其他物体
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
