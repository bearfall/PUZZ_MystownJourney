using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text endGameTime;
    private float startTime;
    private bool isRunning = false;

    void Start()
    {
        // 在游戏开始时启动计时器
        
    }

    void Update()
    {
        if (isRunning)
        {
            // 计算已经过去的时间
            float elapsedTime = Time.time - startTime;

            // 格式化时间为分钟、秒和毫秒
            string minutes = ((int)elapsedTime / 60).ToString();
            string seconds = (elapsedTime % 60).ToString("f2"); // 保留两位小数

            // 更新显示
            timerText.text = minutes + ":" + seconds;
        }
        
    }

    public void StartTimer()
    {
        // 启动计时器
        startTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        // 停止计时器
        isRunning = false;
        endGameTime.text = timerText.text.ToString();

    }
}
