using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;
    //��������ֵ���丱��
    public ExpPickup pickup;

    //��������ֵ(�ȼ�)����, �͵�ǰ�ȼ�
    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;

    //����ȷ��������������ʾ������������
    public List<Weapon> weaponsToUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            //������С���趨ֵʱ, ������β������һ���µľ���ֵ�ȼ�
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�ۼƾ���ֵ
    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
        //����ֽ׶ξ���ֵ�����������辭��ֵ
        if (currentExperience >= expLevels[currentLevel])
        {
            //��������
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);

        //�õ�����ֵʱ�����Ч����
        SFXController.instance.PlaySFXPitched(4);
    }

    //���ɾ���ֵ����
    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    //ʵ����������
    void LevelUp()
    {
        //ȥ����������ľ���ֵ
        currentExperience -= expLevels[currentLevel];
        //����
        currentLevel++;
        //�����ǰ�ȼ������趨����ĳ���
        if (currentLevel >= expLevels.Count)
        {
            //ʹ��ǰ�ȼ���Զ�ǵȼ�����
            currentLevel = expLevels.Count - 1;

        }

        //�������ʱ, �ô�ʱ���װ������������
        //�������������߼�, ��Ϊʹ���������
        //PlayerController.instance.activeWeapon.LevelUp();

        //�������ʱ���������������
        UIController.instance.levelUpPanel.SetActive(true);

        //�����������ʱ��ͣ��Ϸ
        Time.timeScale = 0f;

        //��UI��������������ť��1��λ�ô�����ҵ�ǰװ����������Ϣ������ʾ
        //�������װ������ϵͳ
        // UIController.instance.levelUpButton[1].UpdateButtonDisPlay(PlayerController.instance.activeWeapon);
        //��0��������ť����ʾ��ҳ�ʼ��õ��������������Ϣ
        //UIController.instance.levelUpButton[0].UpdateButtonDisPlay(PlayerController.instance.assignWeapons[0]);

        //UIController.instance.levelUpButton[1].UpdateButtonDisPlay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.levelUpButton[2].UpdateButtonDisPlay(PlayerController.instance.unassignedWeapons[1]);

        //�������չʾ�б�
        weaponsToUpgrade.Clear();

        //�½���չʾ�����б�
        List<Weapon> availableWeapons = new List<Weapon>();

        //���Ƚ��Ѽ��������ȫ�������չʾ�����б�
        availableWeapons.AddRange(PlayerController.instance.assignWeapons);

        //�����չʾ�����б���Ϊ��
        if (availableWeapons.Count > 0)
        {
            //���ѡȡһ����չʾ����
            int selected = Random.Range(0, availableWeapons.Count);
            //�������չʾ�б�, ���¿��������б�, ����֮�����ѡ�������������ʱ�ظ�չʾͬһ������
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        //����Ѽ����б��е��������Ѿ��������������������������趨����, ����������������������ȥ
        if (PlayerController.instance.assignWeapons.Count + PlayerController.instance.fullyLeveledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            //��һ�����ѡ��֮��, ������δ����������Ҳ���뵽��չʾ�б���
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        //ѡ���������������չʾ������
        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            //�����չʾ�����б���Ϊ��
            if (availableWeapons.Count > 0)
            {
                //���ѡȡһ����չʾ����
                int selected = Random.Range(0, availableWeapons.Count);
                //�������չʾ�б�, ���¿��������б�, ����֮�����ѡ�������������ʱ�ظ�չʾͬһ������
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        //������������չʾ�б��е�����
        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButton[i].UpdateButtonDisPlay(weaponsToUpgrade[i]);
        }

        //������������������, �����������Ѿ�����������, ��ô�����İ�ť�������������Ƴ�
        for (int i = 0; i < UIController.instance.levelUpButton.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButton[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButton[i].gameObject.SetActive(false);
            }
        }

        //ȷ����������ʱ����ҵ����������Ϣ����ʾ��UI��
        PlayerStatController.instance.UpdateDisplay();
    }
}