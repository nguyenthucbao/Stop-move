using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject UIPanel;
    public GameObject InGamePanel;
    public GameObject SettingsPanel;
    public GameObject indicatorPanel;
    public GameObject awardPanel;
    public GameObject shopPanel;

    public Button playGame;
    public Button mainMenu;
    public Button quitSettings;
    public Button shopButton;

    public Button settingButton;
    public JoystickControl joystick;

    

    public TextMeshProUGUI goldText;
    //public GameObject awardPanel;


    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() => InitGameState(2));
        InitGold();

        settingButton.onClick.AddListener(() => { SettingsPanel.SetActive(true); });
        mainMenu.onClick.AddListener(() => MainMenuClick());
        quitSettings.onClick.AddListener(() => { SettingsPanel.SetActive(false); });
        shopButton.onClick.AddListener(() => OpenShop());
    }

    public void OpenShop()
    {
        UIPanel.SetActive(false);
        shopPanel.SetActive(true);
        CameraFollower.Instance.ChangeState(3);
        ShopController.Instance.CreateItem("Weapons");
    }

    public void OpenAwardUI(int gold)
    {
        awardPanel.SetActive(true);
        awardPanel.GetComponent<AwardUI>().InitAwardUI(gold, GameController.Instance.bots.Count + 1);
    }


    public void MainMenuClick()
    {
        awardPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        InitGameState(1);
        GameController.Instance.ReplayGame();
        InitGold();
    }

    public void InitGold()
    {
        goldText.text = GameController.Instance.gold.ToString();
    }

    public void InitGameState(int state)
    {
        UIPanel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2);
        joystick.gameObject.SetActive(state == 2);
        shopPanel.SetActive(false);
        if (state == 2)
        {
            indicatorPanel.SetActive(true);
            GameController.Instance.StartGame();
            //SoundManager.Instance.PlayBackgroundMusic(SoundList.BGMainMenu);
        }
        else if (state == 1)
        {
            indicatorPanel.SetActive(false);
        }
        CameraFollower.Instance.ChangeState(state);
    }
}