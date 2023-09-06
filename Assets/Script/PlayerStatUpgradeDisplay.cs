using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatUpgradeDisplay : MonoBehaviour
{
    public TMP_Text ValueText, costText;

    public GameObject upgradeButton;

    //��������о���չʾ��ֵ�ĸ���
    public void UpateDisplay(int cost, float oldValue, float newValue)
    {
        //������������ĸ�����������ʾ
        ValueText.text = "Value: " + oldValue.ToString("F1") + "=>" + newValue.ToString("F1");
        costText.text = "Cost: " + cost;

        //�������㹻��������
        if (cost <= CoinController.instance.currentCoins)
        {
            //�����ť
            upgradeButton.SetActive(true);
        }
        else
        {
            //������ü��ť
            upgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLevel()
    {
        ValueText.text = "Max Level";
        costText.text = "Max Level";
        upgradeButton.SetActive(false);
    }
}
