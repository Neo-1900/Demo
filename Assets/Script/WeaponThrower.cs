using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{
    public EnemyDamager damager;

    private float throwCounter;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�ж������Ƿ�������
        if (statsUpdated == true)
        {
            //������������״̬
            statsUpdated = false;
            //����SetStats����ʵװ���º������
            SetStats();
        }

        throwCounter -= Time.deltaTime;
        if (throwCounter <= 0)
        {
            throwCounter = stats[weaponLevel].timeBetweenAttacks;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
            }

            //ÿһ���������ڲ��Ÿ�����Ч
            SFXController.instance.PlaySFXPitched(3);
        }




    }

    void SetStats()
    {
        //�˺���ֵ
        damager.damageAmount = stats[weaponLevel].damage;
        //����ʱ��
        damager.lifeTime = stats[weaponLevel].duration;

        //�˺��뾶
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        throwCounter = 0;
    }
}