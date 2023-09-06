using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
   
    public static CoinController instance;


    private void Awake()
    {
        instance = this;
    }

    //现有金币总数
    public int currentCoins;

    //声明拾取物品
    public CoinPickUp coin;

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        //调用UI控制器更新金币数量
        UIController.instance.UpdateCoins();

        //搜集金币时发出音效
        SFXController.instance.PlaySFXPitched(4);
    }

    public void DropCoin(Vector3 position, int value)
    {
        //和经验值分散开掉落
        CoinPickUp newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);

        //生成一个新的硬币对象
        newCoin.coinAmount = value;
        //激活游戏对象(防止非预制体失效)
        newCoin.gameObject.SetActive(true);
    }

   //花费金币功能
    public void SpendCoin(int coinToSpend)
    {
        currentCoins -= coinToSpend;

        UIController.instance.UpdateCoins();
    }
}
