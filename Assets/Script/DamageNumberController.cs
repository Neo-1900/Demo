using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    //DamageNumberController脚本, 每次玩家对敌人造成伤害的时候, 我们都希望调用这个脚本
    //所以进行静态变量的声明, 以及初始化, 方便调用DamageNumberController中的方法
    public static DamageNumberController instance;

    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;
    //创建数字池, 优化数字对象的产生所占用的内存
    private List<DamageNumber> numberPool = new List<DamageNumber>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        /*if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnDamage(57f, new Vector3(4, 3, 0));
        }*/

    }
    //克隆数字
    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        //将伤害值转换成整数
        int rounded = Mathf.RoundToInt(damageAmount);

        //DamageNumber newDamage =  Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);

        DamageNumber newDamage = GetFromPool();
        //调用DamageNumber中的显示函数, 将数值传入
        newDamage.Setup(rounded);
        //激活文字对象
        newDamage.gameObject.SetActive(true);
        //在传入位置显示
        newDamage.transform.position = location;
    }

    //返回一个伤害数字DamageNumber类型的变量
    public DamageNumber GetFromPool()
    {
        //将需要返回的值预设为null
        DamageNumber numberToOutput = null;
        //如果对象池中没有数字对象, 则新生成一个
        if (numberPool.Count == 0)
        {
            //在函数中设定生成的数字对象, 和希望在哪个父对象下生成
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        }
        else
        {
            //如果对象池中有数字对象, 将位置0处的数字对象返回显示, 并将其移出对象池
            numberToOutput = numberPool[0];
            numberPool.RemoveAt(0);
        }

        return numberToOutput;
    }

    //当数字的生命周期走完时, 不再销毁对象, 而是将它失活后存入对象池中
    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);

        numberPool.Add(numberToPlace);
    }
}
