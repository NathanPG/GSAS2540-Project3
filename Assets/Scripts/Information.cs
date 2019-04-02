using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is to control scores
/// </summary>
public class Information : MonoBehaviour
{
    public Text HighScoreText;
    public int score;
    public int life;
    public float time;
    public Text scoredisplay;
    public Text lifedisplay;
    public Text timedisplay;
    bool addstart = false;
    public SceneController scene;
    public bool respawning = false;
    public GameObject Ship;
    public bool spawnenemyagain = false;
    public GameObject AsteroidContainer;
    public GameObject ShipExplosion;
    public GameObject ShipRevive;
    public bool clearscreen = false;

    //Add score by 10
    public void addscore()
    {
        score += 10;
    }
    
    //Lose one life
    public void loselife()
    {
        life -= 1;
    }
    //Update the score text obejct
    public void updatescore()
    {
        scoredisplay.text = string.Format("scores:{0,4}", score);
    }
    public void GainScoreForTime()
    {
        addscore();
        updatescore();
    }

    //Update the life text object
    public void updatelife()
    {
        lifedisplay.text = string.Format("life:{0,4}/3", life);
    }

    //Update playerpref of score
    public void UpdateScore(int currentscore)
    {
        string tempstring = "";
        if (PlayerPrefs.HasKey("High Score 1: "))
        {
            for (int i = 1; i <= 10; i++)
            {
                tempstring = "High Score " + i + ": ";
                if (PlayerPrefs.GetInt(tempstring) < currentscore)
                {
                    PlayerPrefs.SetInt(tempstring, currentscore);
                    return;
                }
            }
        }
        else
        {
            for(int i = 1; i<=10; i++)
            {
                tempstring = "High Score " + i + ": ";
                PlayerPrefs.SetInt(tempstring, 0);
            }
            PlayerPrefs.SetInt("High Score 1: ", currentscore);
        }
    }

    

    //Update the score text object
    public void DisplayScore()
    {
        string tempstring = "";
        HighScoreText.text = "";
        for (int i = 1; i <= 5; i++)
        {
            tempstring = "High Score " + i + ": ";
            HighScoreText.text += "High Score " + i + ": " + PlayerPrefs.GetInt(tempstring)+"\n";
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3.0f);
        InvokeRepeating("GainScoreForTime", 1f, 1f);
        Ship.SetActive(true);
        Ship.transform.position = new Vector3(960f, 540f, -10f);
        Ship.transform.localEulerAngles = new Vector3(0, 0, 0);
        Instantiate(ShipRevive, Ship.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        timedisplay.text = string.Format("Time:{0:00.00}/60", time);
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
        }
        if (scene.isGameStart)
        {
            
            if (!addstart)
            {
                InvokeRepeating("GainScoreForTime", 1f, 1f);
                addstart = true;
            }
            else
            {
                if (respawning && life > 0)
                {
                    Ship = GameObject.FindGameObjectWithTag("Ship");
                    spawnenemyagain = true;
                    Instantiate(ShipExplosion, Ship.transform.position, Quaternion.identity);
                    Ship.SetActive(false);
                    CancelInvoke();
                    respawning = false;
                    StartCoroutine(Respawn());
                }
            }
        }
        else
        {
            if (addstart)
            {
                addstart = false;
            }
            CancelInvoke();
        }
    }
}
