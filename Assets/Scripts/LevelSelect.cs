using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private bool isSelect;
    private GameObject lockBG;

    void Awake()
    {
        lockBG = transform.Find("lock").gameObject;
        lockBG.SetActive(true);
    }
    void Start()
    {
        
        if (transform.parent.GetChild(0).name == transform.name)
        {
            isSelect = true; 
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name.Replace("level", ""))-1;
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }

        if (isSelect)
        {
            lockBG.SetActive(false);
            int count = PlayerPrefs.GetInt(gameObject.name);
            transform.Find("star" + count).gameObject.SetActive(true);
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel", gameObject.name);
            SceneManager.LoadScene(2);
        }
    }

}
