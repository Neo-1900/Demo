using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{

    public static PlayerStatController instance;

    private void Awake()
    {
        instance = this;
    }

    //������ÿһ����ҵ���Ӧ��ֵ
    public List<PlayerStatValue> moveSpeed, health, pickupRange;
    //��ߵȼ�
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    //Ŀǰ�ĵȼ�ˮƽ
    public int moveSpeedLevel, healthLevel, pickupRangeLevel;
    // Start is called before the first frame update

    //��ʼ������������Ե�ÿһ������,(�ֶ�����ǰ������ֵ, ֮����Զ�����)
    void Start()
    {
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            //�ٶ�ÿ��һ����Ҫ������, ����0.1���ٶ�ֵ
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            //�ٶ�ÿ��һ����Ҫ������, ����0.1���ٶ�ֵ
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            //�ٶ�ÿ��һ����Ҫ������, ����0.1���ٶ�ֵ
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�����������Ǳ������
        if (UIController.instance.levelUpPanel.activeSelf == true)
        {
            //���ϸ���������Ϣ
            UpdateDisplay();
        }
    }

    //��������������ű��е���ÿ�����ű�PlayerStatUpgradeDisplay�е�UpateDisplay����(����ÿ�����Ե���嶼��UIController���й�����, ֱ�����ü���)
    public void UpdateDisplay()
    {
        //���д���
        //���б߽������޸�
        //���û�е�����ߵȼ�
        if (moveSpeedLevel < moveSpeed.Count - 1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        else
        {
            //������ʾ�ѵ���ȼ�����
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }
        if (healthLevel < health.Count - 1)
        {
            UIController.instance.healthUpgradeDisplay.UpateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }
        if (pickupRangeLevel < pickupRange.Count - 1)
        {
            UIController.instance.pickupRangeUpgradeDisplay.UpateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        }
        else
        {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

    }

    //ʵ�ֹ�����
    public void PurchaseMoveSpeed()
    {
        //����
        moveSpeedLevel++;
        //�۳����
        CoinController.instance.SpendCoin(moveSpeed[moveSpeedLevel].cost);
        //���½����ʾ
        UpdateDisplay();

        //����������������������
        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;

    }
    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.SpendCoin(health[healthLevel].cost);
        UpdateDisplay();
        //����Ѫ������
        PlayerHealthController.instance.maxHealth = health[healthLevel].value;
        //���ӵȼ����Ѫ��
        PlayerHealthController.instance.currentHealth += (health[healthLevel].value - health[healthLevel - 1].value);
    }

    public void PurchasePickUpRange()
    {
        pickupRangeLevel++;
        CoinController.instance.SpendCoin(pickupRange[pickupRangeLevel].cost);
        UpdateDisplay();

        PlayerController.instance.pickupRange = pickupRange[pickupRangeLevel].value;
    }



}

[System.Serializable]
public class PlayerStatValue
{
    //����������Ҫ���ѵĽ��
    public int cost;
    //��������Ӧ��������
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        cost = newCost;

        value = newValue;
    }
}