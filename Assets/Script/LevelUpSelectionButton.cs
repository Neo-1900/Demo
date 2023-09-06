using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    //��ť�ϵĸ���Ԫ��
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    //���ܵ�������Ϣʱ, �洢����������ĸ�����
    private Weapon assignedWeapon;

    //���մ��������ĸ�����Ϣ����ʾ����Ļ��
    public void UpdateButtonDisPlay(Weapon theWeapon)
    {

        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;
            //����������������
            nameLevelText.text = theWeapon.name + " - lvl " + theWeapon.weaponLevel;
        }
        else
        {
            upgradeDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        //�����ݴ浱ǰ���������������
        assignedWeapon = theWeapon;

    }

    //ʵ��ѡ����������Ч��
    public void SelectedUpgrade()
    {
        //�����ѡ�����������
        if (assignedWeapon != null)
        {
            //�����Ϸ�����Ѽ���
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                //��������
                assignedWeapon.LevelUp();
            }
            else
            {
                //���������������(����)
                PlayerController.instance.Addweapon(assignedWeapon);
            }
            //�ر����, �ָ���Ϸ����
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}