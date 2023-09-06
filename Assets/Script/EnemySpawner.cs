using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //��Ϸ����:����Ԥ����.
    public GameObject enemyToSpawn;
    //��ʾ���೤ʱ��ȥ����һ������.
    public float timeToSpawn;
    //������ʱ.
    private float spawnCounter;

    //���е�ת������,����λ����Ϣ,������Ϣ(����λ�ö���)
    public Transform minSpawn, maxSpawn;

    //���ӵ��˸������λ�����ɵĴ���
    //��������λ�ñ���
    private Transform target;
    //ɾ����Ұ��ĵ���
    private float despawnDistance;
    //ÿ������һ�����˵�ʱ��, �����ӵ�����б���
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int enemyToCheck;

    //������������
    public List<WaveInfo> waves;

    //������ǰ�����±�Ͳ��γ���ʱ��
    private int currentWave;
    private float waveCounter;


    private void Start()
    {
        //�õ�targetλ��(�����Ǿ�̬����, ���Կ���ֱ��ͨ����������)
        target = PlayerController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;
        //��ʼ����һ�����˵�����
        currentWave = -1;
        GoToNextWave();
    }

    private void Update()
    {
        //���µ������ɻ���, ʹ����԰���������
        //��spawnCounter���и���,Time.deltaTime��ʾ����Update��������֮���ʱ���.
        //����Updateÿһ֡���ᱻ����,  ���Կ���ͨ�������жϼ�ʱ����ֵ����ʱ����Ƶ��˵�����.
        /*spawnCounter -= Time.deltaTime;

        //��spawnCounter�����ж�(һ���Ǵ���0��,д1����ÿ1������һ��),���<= 0, ��ʾ��ʱ����,��Ҫȥ���ɵ���.
        if (spawnCounter <= 0)
        {
            //��������spawnCounter��ֵ.
            spawnCounter = timeToSpawn;
            //����һ���µĵ��˶���:  ��¡Enemy��Ϸ����, ѡ������������Ϣ, ��ת��Ϣ
            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);

            spawnedEnemies.Add(newEnemy);
        }*/

        //�����ҵ�����ֵû��������0, �������ɵ���
        if (PlayerHealthController.instance.gameObject.activeSelf)
        {
            //���±껹û�ߵ��б���ͷ
            if (currentWave < waves.Count)
            {
                //��ʼ���в��μ�ʱ
                waveCounter -= Time.deltaTime;
                //��ʱ������ʱ, ��ǰ���ɲ���ֹͣ
                if (waveCounter <= 0)
                {
                    //������һ�ֵ��˵Ĳ�������
                    GoToNextWave();
                }
                //��ʼ���ɵ���
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    //����ÿ�ִε��˵ľ������ɼ���������ü�ʱ����ֵ
                    spawnCounter = waves[currentWave].timeBetweenSpawns;
                    //��¡����һ���µĵ���
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    //������������˶�������(���ڼ������ҵľ���)
                    spawnedEnemies.Add(newEnemy);
                }
            }
        }


        //��Update�ж����������и�ֵ.transform����ֱ��ʹ��,��GameObject�����õ�����
        //�����ǵ�λ�ø�ֵ��������, ��������λ�þͻᷢ����Ա仯
        transform.position = target.position;

        //������ʱ����, checkTarget��ʾ�ڵ�����������Ҫ��鵽�����һ���±�,enemyToCheck��ʾ��Ҫ���ĵ�һ���±�
        int checkTarget = enemyToCheck + checkPerFrame;
        //��û�м�鵽ÿһ֡��Ҫ����Ŀ��ʱ
        while (enemyToCheck < checkTarget)
        {
            //������ڼ��ĵ����±� С�ڵ�������ĳ���
            if (enemyToCheck < spawnedEnemies.Count)
            {
                //�����鵽�ĵ��˶���Ϊ��
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    //��ô�ж����ͽ�ɫ(�������ɾ��ο�����EnemySpawner) �ľ��� �Ƿ������Ż�����(��Զ)
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        //�������, ��ɾ����ǰ���˶���
                        Destroy(spawnedEnemies[enemyToCheck]);
                        //��������ȥ����Ԫ��
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        //��ǰ֡�Ѿ������һ������, ����β���±��1
                        checkTarget--;
                    }
                    //����������Ż�����
                    else
                    {
                        //���ǰ�±��1, �����һ������(������䳤����С)
                        enemyToCheck++;
                    }
                }
                //�����ǰ���ĵ��˶���Ϊ��
                else
                {
                    //�Ƴ���ǰ���˶���
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    //���ڵ�ǰ֡�����һ������, β���±��1
                    checkTarget--;
                }
            }
            //����������Ż������ĵ��˶����ۼƹ���, ʹ�õ�ǰ��Ҫ���ĵ����±�enemyToCheck�������鳤��spawnedEnemies.Count
            //����ǰ�±�����С��checkTarget, ��ô��һֱ����ѭ����β, ������ѭ��
            //���Ե���һ�ε���ѭ����β��ʱ��, ʹenemyToCheck����, ���������Ѿ������������, ����checkTargetҲ����(û����Ҫ����Ԫ����)
            //�����ֶ�ֹͣwhileѭ��, ������һ֡������
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }
    private Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        //�ж��߽�,�Ƿ���������Χ��.
        //����һ����0 ��1 �������, �����ж����ں�������������������
        bool spawnVerticalEdge = UnityEngine.Random.Range(0f, 1f) > .5f;
        //�ж��������Χ
        if (spawnVerticalEdge)
        {
            //������������0.5, �����������ֵ, ��Ҫ��y���趨�߽�,�߽��ڽ������λ�õ�ѡ��
            //����������������������ɵ��ѡȡ
            spawnPoint.y = UnityEngine.Random.Range(minSpawn.position.y, maxSpawn.position.y);
            //�ٴ�����һ�������,�����ж����������滹��������
            if (UnityEngine.Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        //�����Ǻ������ֵ, ��Ҫ��x���趨�߽�,�߽��ڽ������λ�õ�ѡ��.
        //����������������������ɵ��ѡȡ
        else
        {
            spawnPoint.x = UnityEngine.Random.Range(minSpawn.position.x, maxSpawn.position.x);
            //�ٴ�����һ�������,�����ж����������滹������
            if (UnityEngine.Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    }

    //����ÿ�ֲ�����Ϣ
    public void GoToNextWave()
    {
        //������һ��
        currentWave++;
        //����±�������鳤��, ά�ֲ��������һ��
        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        //���²���ʱ�������ɼ����Ϣ
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }
}


//�����������˲��ε���������
//���л�, �����д���ת����ʽ, ʹ��������unity������ʾ����
[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
