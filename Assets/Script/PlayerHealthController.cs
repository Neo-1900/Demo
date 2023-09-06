using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    //��̬����
    public static PlayerHealthController instance;

    //��ǰ����ֵ:
    public float currentHealth;
    //�������ֵ
    public float maxHealth;

    //��UI��������ֵ�ı仯
    public Slider healthSlider;

    //������������
    public GameObject deathEffect;

    //��ʼ����̬ʵ��
    private void Awake()
    {
        instance = this;
    }

    //��дStart����
    private void Start()
    {
        maxHealth = PlayerStatController.instance.health[0].value;

        currentHealth = maxHealth;
        //��ʼ��Ѫ��
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


    //���ڸ��µĺ���(���漰�������������ʱ��Update)
    private void Update()
    {
        //ֱ��ͨ������ķ�ʽ���в�׽(�˴���׽T��)
        /*  if (Input.GetKeyDown(KeyCode.T))
          {
              TakeDamage(10f);
              //��ʾ��ֵ
              print("current health = " + currentHealth);
          } */
    }

    public void TakeDamage(float damageTotake)
    {

        //���ڸ��½�ɫѪ��
        currentHealth -= damageTotake;

        //����Ϊ0ʱ����Ϸ��ɫ��ʧ
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //�����ؿ�
            LevelManager.instance.EndLevel();
            //������������
            Instantiate(deathEffect, transform.position, transform.rotation);

            //����������Ч
            SFXController.instance.PlaySFX(5);
        }
        //��Ѫ��UI��ʾʣ��Ѫ��
        healthSlider.value = currentHealth;
    }
}
