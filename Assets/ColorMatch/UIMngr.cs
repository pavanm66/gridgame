using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMngr : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject choosePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public Text rowText;
    public Text colText;
    public Grid2d gridManager;

    void Start()
    {
        panelsOff();
        startPanel.SetActive(true);
    }
    public void StartBtn()  
    {
        panelsOff();
    }
    public void CustomizeBtn()
    {
        panelsOff();
        choosePanel.SetActive(true);
    }
    public void CreateGridBtn()
    {
        panelsOff();
        int x = int.Parse(rowText.text);
        int y = int.Parse(colText.text);
        gridManager.CreateGridBtn(x, y);
    }
    private void panelsOff()
    {
        startPanel.SetActive(false);
        choosePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void ReplayBtn()
    {
        panelsOff();
        gridManager.ResetColor();
    }
    public void GameOver(string value)
    {
        if (value == "You Win")
        {
            GameWin();
        }
        if (value == "You Lost")
        {
            GameLost();
        }
    }
    public void GameWin()
    {
        panelsOff();
        winPanel.SetActive(true);
    }
    public  void GameLost()
    {
        panelsOff();
        losePanel.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
