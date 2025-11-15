using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    public Button startButton,optionsButton;
    public Button exitButton,makersButton;
    
    public GameObject settingsPanel;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartButtonFunc);
        optionsButton.onClick.AddListener(OptionsButtonFunc);
        exitButton.onClick.AddListener(ExitButtonFunc);
        makersButton.onClick.AddListener(MakerButtonFunc);
    }

    public void StartButtonFunc()
    {
        SceneLoadManager.Instance.LoadGameScene();
    }
    
    public void OptionsButtonFunc()
    {
        settingsPanel.SetActive(true);
    }
    
    public void MakerButtonFunc()
    {
        //TODO:制作人员界面函数
    }
    
    public void ExitButtonFunc()
    {
        Application.Quit();
    }
}
