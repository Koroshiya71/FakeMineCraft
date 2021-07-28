using UnityEngine;
using System.Collections;
using GameCore;

public class EnemyManager : UnitySingleton<EnemyManager>
{
    private GameObject rabbitPrefab;
    private int spawnTimer = 20;
    private Vector3 playerPos;
    
    void Start()
    {
        //添加事件监听
        EventDispatcher.AddListener(E_MessageType.EnterGameScene, delegate
        {
            playerPos = GameSceneManager.Instance.player.position;
            rabbitPrefab = ResourcesManager.Instance.LoadResources<GameObject>("GamePrefab/" + "Enemy");
            StartSpawn();
        });
    }
    //开始生成
    private void StartSpawn()
    {
        StartCoroutine(SpawnRabbit());
    }
    //创建Rabbit敌人
    private void CreateRabbit()
    {
        //随即在玩家周围生成
        Vector3 spawnPos = new Vector3(Random.Range(playerPos.x - 50, playerPos.x + 50), 60, Random.Range(playerPos.z - 50, playerPos.z + 50));
        Instantiate(rabbitPrefab, spawnPos, Quaternion.identity);
    }
    void Update()
    {
        //实时更新玩家位置
        if (GameSceneManager.Instance.IsInGame)
        {
            playerPos = GameSceneManager.Instance.player.position;
        }
    }
        
    IEnumerator SpawnRabbit()
    {
        while (true)
        {
            //随机生成间隔
            spawnTimer = Random.Range(20, 120);
            Invoke("CreateRabbit",5);
            yield return new WaitForSeconds(spawnTimer);
        }
        
    }
}