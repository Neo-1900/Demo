using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    //�趨�����˺�
    public EnemyDamager damager;

    //�趨�����ٶ�
    public Projectile projectile;

    private float shotCounter;

    public float weaponRange;
    public LayerMask whatIsEnemy;

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

        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].timeBetweenAttacks;
            //��ȡ�ɵ���ⷶΧ�ڵĵ�����ײ�����
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);

            if (enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    //��ȡ���˳���
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    //ȷ����������
                    Vector3 direction = targetPosition - transform.position;

                    //��������ת���ɽǶ�
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    //�Ƕ�����
                    angle -= 90;
                    //��������ĽǶ�Ӧ�õ���������
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    //�������ɷɵ����󲢼���
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }

                //�ɵ�������Ч
                SFXController.instance.PlaySFXPitched(0);
            }
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

        shotCounter = 0;

        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}