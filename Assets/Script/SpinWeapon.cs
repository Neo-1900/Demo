using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    //�趨��ת�ٶ�
    public float ratateSpeed;
    //����һ���µĿն���, ������ת����, �����������
    public Transform holder, fireballToSpawn;
    //�����������µĻ���
    public float timeBetweenSpawn;
    //��ʱ��
    private float spawnCounter;

    //��������������Ϣ
    public EnemyDamager Damager;

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��������Ϣ
        SetStats();
        //������������������ת
        //UIController.instance.levelUpButton[0].UpdateButtonDisPlay(this);
    }

    // Update is called once per frame
    void Update()
    {
        //����z�����ת��, ����holder��Ϊ��������ת�����������ת

        //  holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + ratateSpeed * Time.deltaTime);
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + ratateSpeed * Time.deltaTime * stats[weaponLevel].speed);
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;
            //��¡��Ϊholder���Ӷ���, ������
            //Fireball Holder����fireballToSpawn, ��Ϊ�м乤�ߴ洢��ת�����ĵ�����
            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);


            //���»�������ʱ���������Ĺ���
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;

                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);

                //������Ч
                SFXController.instance.PlaySFX(2);
            }
        }

        //�ж������Ƿ�������
        if (statsUpdated == true)
        {
            //������������״̬
            statsUpdated = false;
            //����SetStats����ʵװ���º������
            SetStats();
        }

    }

    //��������״̬
    public void SetStats()
    {
        Damager.damageAmount = stats[weaponLevel].damage;

        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        Damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}