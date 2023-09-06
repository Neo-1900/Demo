using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�ƶ��ٶ���ʼֵΪ1
    public float moveSpeed = 1f;

    //���ӵ��˸��������ƶ����ɵĴ���
    public static PlayerController instance;

    //ʰȡ����ֵ�ľ���
    public float pickupRange = 1.5f;

    //�������װ������ϵͳ
    // public Weapon activeWeapon;
    //�½������б�
    public List<Weapon> unassignedWeapons, assignWeapons;

    //�趨����������ж�������
    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLeveledWeapons = new List<Weapon>();

    //��Instance����ʵ����.Awake ������������Start������ʼ����֮ǰ���г�ʼ��.
    private void Awake()
    {
        //��ȡPlayerControllerʵ��(this)��ֵ��Instance,����EnemySpawner�е�Start
        instance = this;
    }

    private void Start()
    {
        //��ʼ��ѡȡ���� ��δѡ�����б������ѡ��һ����������������Ϊ��ʼ����
        //int�ͱ�������ȡ�����ֵ, ���Կ��Է������ѡȡ
        if (assignWeapons.Count == 0)
        {
            AddWeapon(UnityEngine.Random.Range(0, unassignedWeapons.Count));
        }

        //��ʼ���������������ֵ
        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickupRange[0].value;
    }

    //������Ϸ����ת,ÿһ֡��������������,��֤�������ݵĲ�׽
    private void Update()
    {

        //��ά����,����һ���ƶ�����
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        //��ʾ�����ƶ�,��ȡ������ˮƽ���������
        moveInput.x = Input.GetAxisRaw("Horizontal");
        //����
        moveInput.y = Input.GetAxisRaw("Vertical");

        //��һ��,��֤б���ƶ����ٶ�һ��
        moveInput.Normalize();

        transform.position += moveInput * moveSpeed * Time.deltaTime;
    }

    //����ҷ����ʼ����
    public void AddWeapon(int weaponNumber)
    {

        if (weaponNumber < unassignedWeapons.Count)
        {
            //�����������װ�ص���װ���б���
            assignWeapons.Add(unassignedWeapons[weaponNumber]);

            //���������󼤻���δװ���б���ɾ��
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    //���ڽ�����������
    public void Addweapon(Weapon weaponToAdd)
    {
        //��������
        weaponToAdd.gameObject.SetActive(true);
        //�����������Ѽ����б�
        assignWeapons.Add(weaponToAdd);
        //��δ�����б���ɾ��������
        unassignedWeapons.Remove(weaponToAdd);
    }
}
