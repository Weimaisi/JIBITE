using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTitle : MonoBehaviour
{
    public float floatAmplitude = 0.5f;  // 浮动的幅度
    public float floatSpeed = 1f;        // 浮动的速度
    private Vector3 startTitlePos;       // title的初始位置
    private Vector3 startOOPos;          // O_o的初始位置

    [Header("MoveGrid")]
    public GameObject title;
    public GameObject O_o;

    void Start()
    {
        // 在开始时记录title和O_o物体的初始位置
        startTitlePos = title.transform.position;
        startOOPos = O_o.transform.position;
    }

    void Update()
    {
        // 分别处理title和O_o的浮动
        FloatMove(title, startTitlePos);
        FloatMove(O_o, startOOPos);
    }

    public void FloatMove(GameObject target, Vector3 startPos)
    {
        // 计算浮动范围
        float newY = startPos.y + Mathf.PingPong(Time.time * floatSpeed, floatAmplitude * 2) - floatAmplitude;

        // 如果目标是title，x轴和y轴都需要浮动
        if (target == title)
        {
            float newX = startPos.x + Mathf.PingPong(Time.time * floatSpeed, floatAmplitude * 2) - floatAmplitude;
            target.transform.position = new Vector3(newX, newY, target.transform.position.z);
        }
        // 如果目标是O_o，y轴浮动方向与title反向
        else if (target == O_o)
        {
            // 反转眼睛的Y轴浮动
            target.transform.position = new Vector3(target.transform.position.x, startOOPos.y - (Mathf.PingPong(Time.time * floatSpeed, floatAmplitude * 2) - floatAmplitude), target.transform.position.z);
        }
    }


}