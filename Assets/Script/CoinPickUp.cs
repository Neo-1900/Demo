using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    //默认金币价值为1
    public int coinAmount = 1;
    //朝向玩家移动
    private bool movingToPlayer;
    public float moveSpeed;

    //检测距离运算量大, 所以按照时间间隔检测
    public float timeBetweenCheck = .2f;
    private float checkCounter;

    //获取玩家实例
    private PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
       //如果当前距离需要朝向玩家移动
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            //否则
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenCheck;
                //如果经验值在拾取范围内
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    //那么加速向玩家前进(因为玩家在动, 所以加上速度保证不会拾取不到)
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    //判定, 如果被拾取产生碰撞就销毁金币对象
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinController.instance.AddCoins(coinAmount);

            Destroy(gameObject);
        }
    }
}
