using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    //�ؿ���ʱ
    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //�����Ϸ��������, ��һֱ��ʱ
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            //����UI��ʾ����updateTimer��ʾʱ��
            UIController.instance.updateTimer(timer);
        }
    }

    public void EndLevel()
    {
        gameActive = false;

        //��ʼЭͬ����
        StartCoroutine(EndLevelco());
    }

    IEnumerator EndLevelco()
    {

        //��ʱ��ʾ
        yield return new WaitForSeconds(waitToShowEndScreen);

        //�ڽ����������UI��ʾ����ʱ��
        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        UIController.instance.levelEndScreen.SetActive(true);
    }
}