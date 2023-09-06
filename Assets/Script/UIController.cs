using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    //����UI��������ֻ���
    public Slider explvlSlider;
    public TMP_Text expLvlText;

    //����һ��������ť����
    public LevelUpSelectionButton[] levelUpButton;

    //����һ��������
    public GameObject levelUpPanel;

    //������ʾ���UI
    public TMP_Text coinText;

    //�½�UI����������, ����PlayerStatController��������ʱ���ø���
    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay;

    //���Ӽ�ʱ��
    public TMP_Text timeText;

    //�ؿ��������
    public GameObject levelEndScreen;
    public TMP_Text endTimeText;

    //���ڷ������˵�
    public string mainMenuName;

    //��ͣ�˵�
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //����esp����ʱ�������ͣ/������Ϸ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpuse();
        }
    }

    //����ÿһ���ľ���ֵ
    public void UpdateExperience(int currentExp, int levelExp, int currentLvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        expLvlText.text = "Level : " + currentLvl;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        //�����ڳ��еĽ������ʾ����Ļ��
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }

    //��UI�������е���PlayerStatController�еĺ���, ���ڸ��������Ϣ, �ٽ�����ڰ�ť�¼���, �������뽡׳�Ը�ǿ
    //ʵ�ֹ�����
    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        //ÿ��ֻ������һ������
        SkipLevelUp();
    }
    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickUpRange()
    {
        PlayerStatController.instance.PurchasePickUpRange();
        SkipLevelUp();
    }

    //���ڸ�����ʾ��ǰʱ��
    public void updateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }


    //��ת�����˵�
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    //���¿�ʼ��Ϸ
    public void Restart()
    {
        //��ת����Ϸ��ʼ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    //�˳���Ϸ
    public void QuitGame()
    {
        Application.Quit();
    }

    //����/��ͣ��Ϸ
    public void PauseUnpuse()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            //�����ͣʱ
            pauseScreen.SetActive(false);

            //����������û�п���
            if (levelUpPanel.activeSelf == false)
            {
                //�ָ�ʱ������
                Time.timeScale = 1f;
            }

        }
    }

}