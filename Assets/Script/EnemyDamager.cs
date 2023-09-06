using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    //总伤害数值
    public float damageAmount;

    //声明周期和速度
    public float lifeTime, growSpeed = 5f;

    private Vector3 targetSize;

    //���Ӹ������Ƿ��ܹ����˵��˵Ĳ���ֵ
    public bool shouldKnockBack;

    public bool destroyParent;

    //�������������
    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;

    //�������ڹ⻷��Χ�ڵĵ��˴����б���
    List<EnemyController> enemiesInRange = new List<EnemyController>();

    //�ɵ���ײ���ٶ�����ж�
    public bool destroyOnImpact;

    void Start()
    {
        //��ʼ��, �趨�����������������
        //Destroy(gameObject, lifeTime);
        //׼����������0�𽥳���ԭʼ��С
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //ʹ�����𽥱��targetSize
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        //���»����������ڼ�ʱ����ֵ
        lifeTime -= Time.deltaTime;
        //�����ʱС��0, ��ʾ������Ҫ������
        if (lifeTime <= 0)
        {
            //�趨�任Ŀ���С��������ʧ
            targetSize = Vector3.zero;
            //����������ʧ��ɾ����������
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                //�����ѡ��ɾ��������, ��ô����ɾ��
                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        //�������ʱ������˺�����ѡ
        if (damageOverTime == true)
        {
            //��ʼ����ʱ�������й���
            damageCounter -= Time.deltaTime;
            //��ʱ��������
            if (damageCounter <= 0)
            {
                //���ü�ʱ��
                damageCounter = timeBetweenDamage;
                //����Χ�ڵĵ��˱���һ��
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    //����б��еĵ��˴���
                    if (enemiesInRange[i] != null)
                    {
                        //���������˺�
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        //����(������)�Ƴ��õ��˶���
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

    }


    //��ײ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���û�й�ѡ��ʱ�������˺���ѡ��
        if (damageOverTime == false)
        {
            //��ô���������˺��ж����Ե��˲����˺�
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);

                //һ���ɵ���������ײ�˺�, ��������ɵ�����
                if (destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (collision.tag == "Enemy")
            {
                //��ȡ�ڷ�Բ��Χ�ڵĵ��˶���
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }

    //��������뿪�˹�����Χ, ��ô������б���ɾ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
