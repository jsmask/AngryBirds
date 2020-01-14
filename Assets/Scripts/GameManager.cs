using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public static GameManager _interface;

    public List<Bird> birds;
    public List<Pig> pigs;
    public GameObject lose;
    public GameObject win;
    public GameObject pause;
    public GameObject pauseBtn;

    private Vector3 orginPos;
    private GameObject[] stars;

    private int totalNum = 12;
    private int starNum = 0;


    private void Awake()
    {
        _interface = this;
        if (birds.Count > 0)
        {
            orginPos = birds[0].transform.position;
        }
        stars = new GameObject[3];
        stars[0] = win.transform.Find("Image").Find("rightStar").gameObject;
        stars[0].SetActive(false);
        stars[1] = win.transform.Find("Image").Find("middleStar").gameObject;
        stars[1].SetActive(false);
        stars[2] = win.transform.Find("Image").Find("leftStar").gameObject;
        stars[2].SetActive(false);
    }

    private void init()
    {
        for (int i=0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = orginPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    /// <summary>
    /// 判断逻辑
    /// </summary>
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                init();
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            GameWin();
        }
    }

    void GameOver()
    {
        Debug.Log("失败");
        lose.SetActive(true);
    }

    void GameWin()
    {
        Debug.Log("胜利");
        win.SetActive(true);
    }

    public void showStar()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (; starNum < birds.Count + 1; starNum++)
        {
            if (starNum >= 3) break;
            stars[starNum].SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void ShowPause()
    {
        if (pigs.Count > 0)
        {
            Time.timeScale = 0;
            pauseBtn.SetActive(false);
            pause.SetActive(true);
        }
    }

    public void HidePause()
    {
        Time.timeScale = 1f;
        pauseBtn.SetActive(true);
        pause.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(2);
        SaveData();
    }

    public void BackHome()
    {
        SceneManager.LoadScene(1);
        SaveData();
    }

    public void SaveData()
    {
        if(starNum> PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNum);
        }

        int sum = 0;
        for (int i = 1; i <= totalNum; i++)
        {       
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }

    void Start()
    {
        init();
    }

    void Update()
    {
        
    }
}
