using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    //��������ֵ��С
    public int expValue;
    //�Ƿ�������ƶ�, �ƶ��ٶ�
    private bool movingToPlayer;
    public float moveSpeed;

    //�趨��ʱ��, �������ܴ�, ��������ϣ������ʱ�������м���
    public float timeBetweenCheck = .2f;
    private float checkCounter;

    //׷����ҿ�����
    private PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        //��ȡ��ҽ�ɫʵ��
        player = PlayerController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //���movingToPlayerΪ��
        if (movingToPlayer == true)
        {
            //��ô��������ƶ�
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            //���movingToPlayerΪ��, ��Ҫ����Ƿ���Ҫ��������ƶ�
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                //���ü�ʱ����ֵ
                checkCounter = timeBetweenCheck;
                //���������ҵľ���С��ʰȡ����
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    //��ô����ʰȡ, �������ƶ��ٶ�+=����ƶ��ٶ�, ��֤����ֵ���Ա�����
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    //������ײ���, �����ҽӴ����˾���ֵ, �ͻ����Ӿ���ֵ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);

            Destroy(gameObject);
        }
    }
}