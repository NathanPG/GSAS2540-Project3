using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

/// <summary>
/// This script controls scenes, including buttons, 
/// menus, messages, switching scenes.
/// </summary>

public class SceneController : MonoBehaviour
{
    public Information info;
    public bool isGameStart = false;
    public bool isFirstScene = true;
    public bool isGameend = false;
    
    public GameObject regularui;
    public GameObject pauseemenu;
    public GameObject mainmenu;
    public GameObject smsgobject;
    public GameObject fmsgobject;
    public GameObject wmsgobject;
    public GameObject EndMenu;
    public GameObject HSmenu;
    public GameObject Ship;
    
    //click start button
    public void GameStart()
    {
        isFirstScene = true;
        SceneManager.LoadScene("PlayingScene");
        regularui.SetActive(true);
        mainmenu.SetActive(false);
        info.score = 0;
        info.life = 3;
        info.updatescore();
        info.updatelife();
    }
    //click quit button
    public void GameQuit()
    {
        Application.Quit();
    }

    //click high score button
    public void DisplayHighScore()
    {
        info.DisplayScore();
        HSmenu.SetActive(true);
    }

    //click cross button in highscore menu
    public void closeHS()
    {
        HSmenu.SetActive(false);
    }

    //click pause button
    public void PauseMenu()
    {
        Time.timeScale = 0;
        pauseemenu.SetActive(true);
    }

    //click cross button in pause menu
    public void resumegame()
    {
        Time.timeScale = 1;
        pauseemenu.SetActive(false);
    }

    //click restart button in the last scene
    public void Restart()
    {
        smsgobject.SetActive(true);
        regularui.SetActive(true);
        SceneManager.LoadScene("PlayingScene");
        info.score = 0;
        info.life = 3;
        info.time = 60;
        info.updatescore();
        info.updatelife();
        isFirstScene = true;
        HSmenu.SetActive(false);
        EndMenu.SetActive(false);
        wmsgobject.SetActive(false);
        fmsgobject.SetActive(false);
    }

    //click menu button in the last scene
    public void BacktoMenu()
    {
        SceneManager.LoadScene("TempMenu");
        HSmenu.SetActive(false);
        mainmenu.SetActive(true);
        regularui.SetActive(false);
        EndMenu.SetActive(false);
        wmsgobject.SetActive(false);
        fmsgobject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        HSmenu.SetActive(false);
        pauseemenu.SetActive(false);
        regularui.SetActive(false);
        smsgobject.SetActive(false);
        wmsgobject.SetActive(false);
        fmsgobject.SetActive(false);
        isGameStart = false;
        isFirstScene = false;
        EndMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((info.time <= 0 || info.life == 0) && isGameStart)
        {
            SceneManager.LoadScene("EndScene");
            EndMenu.SetActive(true);
            if(info.life == 0)
            {
                fmsgobject.SetActive(true);
            }
            else
            {
                wmsgobject.SetActive(true);
            }
            info.UpdateScore(info.score);
            info.DisplayScore();
            //EnemySpawner destroied
            info.respawning = false;
            info.clearscreen = false;
            HSmenu.SetActive(true);
            regularui.SetActive(false);
            isGameStart = false;
        }
        //first start
        else if (isFirstScene)
        {
            info.time = 60;
            smsgobject.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                isGameStart = true;
                isFirstScene = false;
                smsgobject.SetActive(false);
            }
        }
    }
}
