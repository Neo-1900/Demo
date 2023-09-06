using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;

    public float lifetime;
    private float lifecounter;

    public float floatSpeed = 1f;

    /*void Start()
     {
         lifecounter = lifetime;
     }*/

    // Update is called once per frame
    void Update()
    {
        //被调用时更新数字对象的生命周期
        if (lifecounter > 0)
        {
            lifecounter -= Time.deltaTime;
            if (lifecounter <= 0)
            {
                //显示伤害数值
                //Destroy(gameObject);
                DamageNumberController.instance.PlaceInPool(this);
            }
        }
     
        /* if (Input.GetKeyDown(KeyCode.U))
          {
              Setup(45);
          }*/
        //添加动态向上效果
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

    }

    public void Setup(int damageDisplay)
    {
        //被调用时更新数字对象的生命周期
        lifecounter = lifetime;
        //显示伤害数值
        damageText.text = damageDisplay.ToString();
    }
}
