using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{

    public EnemyDamager damager;

    private float spawnTime, spawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
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

        //��������
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);

        }

    }

    void SetStats()
    {
        //�˺���ֵ
        damager.damageAmount = stats[weaponLevel].damage;
        //����ʱ��
        damager.lifeTime = stats[weaponLevel].duration;
        //�˺����
        damager.timeBetweenDamage = stats[weaponLevel].speed;
        //�˺��뾶
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        //�ٴ����ɵ�ʱ��
        spawnTime = stats[weaponLevel].timeBetweenAttacks;
        //�趨һ��������������(�������汾������)
        spawnCounter = 0f;
    }
}