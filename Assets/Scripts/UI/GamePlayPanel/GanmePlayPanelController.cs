using UnityEngine;
using UnityEngine.UI;

public class GanmePlayPanelController : MonoBehaviour
{
    public Button settingButton,pauseButton;
    
    public GameObject settingsPanel;
    private void OnEnable()
    {
        settingButton.onClick.AddListener(SettingButtonFunc);
        pauseButton.onClick.AddListener(PauseButtonFunc);
    }

    public void SettingButtonFunc()
    {
        settingsPanel.SetActive(true);
    }

    public void PauseButtonFunc()
    {

    }
}
