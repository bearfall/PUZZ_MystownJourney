using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPGSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetGame()
    {
        RPGCharacterManager.instance.ReLoadGame();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {

    }
    public void OnExitGame()//定义一个退出游戏的方法
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
        #else
        Application.Quit();//否则在打包文件中
        #endif
    }
}
