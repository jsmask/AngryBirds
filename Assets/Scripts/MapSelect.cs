using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public int starNum = 0;

    public GameObject lockBox;
    public GameObject starBox;
    public GameObject panelBox;
    public GameObject mapBox;
    public GameObject back;
    public int LevelNum = 1;
    public int mapLevel = 1;

    private bool isSelect = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("totalNum", 0) >= starNum) {
            isSelect = true;
        }
        lockBox.SetActive(!isSelect);
        starBox.SetActive(isSelect);

        if (isSelect)
        {
            int sum = 0;
           
            for (int i= (mapLevel-1)*LevelNum +1;i<=mapLevel * LevelNum; i++)
            {
                sum += PlayerPrefs.GetInt("level" + i.ToString());
            }
            starBox.transform.Find("Text").GetComponent<Text>().text = sum + "/" + LevelNum*3;
        }
        else
        {
            lockBox.transform.Find("Text").GetComponent<Text>().text = starNum.ToString();
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            mapBox.SetActive(false);
            back.SetActive(true);
            panelBox.SetActive(true);
        }
    }

    

}
