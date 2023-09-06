using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    public Sprite icon;

    //�ڼ����������, ����״̬
    [HideInInspector]
    public bool statsUpdated;

    public void LevelUp()
    {
        //��������ȼ����趨����ߵȼ�С
        if (weaponLevel < stats.Count - 1)
        {
            //��������
            weaponLevel++;
            //����״̬����Ϊ��
            statsUpdated = true;

            //��������ȼ���������, �������ӽ����������б�, �������Ѽ����б���ɾ��
            if (weaponLevel >= stats.Count - 1)
            {
                PlayerController.instance.fullyLeveledWeapons.Add(this);
                PlayerController.instance.assignWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    //�����Ļ�������duration : ����ʱ��
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    //������Ϣ
    public string upgradeText;
}