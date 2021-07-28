using System;
using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Rabbit : MonoBehaviour
{
    private float jumpTimer=2.5f;
    private Vector3 playerPos;
    private float speed = 1.5f;
    private Rigidbody enemyRig;
    private Vector3 originPos;
    private float rotateSpeed = 60;
    private float hp = 150;

    public float Hp
    {
        get { return hp; }
    }

    void Start ()
    {
        playerPos = GameSceneManager.Instance.player.position;
        enemyRig = GetComponent<Rigidbody>();
        originPos = transform.position;
    }

    public void TakeDamage(float damage)
    {
        Vector3 flyDir = Vector3.Normalize(transform.position - GameSceneManager.Instance.player.position);
        GetComponent<Rigidbody>().AddForce(flyDir * 200);
        hp -= damage;
        if (hp<=0)
        {
            RabbitDie();
        }
        GameDebuger.Log(hp);
    }

    private void RabbitDie()
    {
        //掉落奶糖
        GameObject dropGo = Instantiate(ResourcesManager.Instance.LoadAsset("GamePrefab/" + "DropPrefab"),
            new Vector3(transform.position.x, transform.position.y + 0.1f , transform.position.z), Quaternion.identity) as GameObject;
        var dropCube = dropGo.GetComponent<DropCube>();
        dropCube.InitMtl(19);

        //掉落奶茶
        GameObject dropGo2 = Instantiate(ResourcesManager.Instance.LoadAsset("GamePrefab/" + "DropPrefab"),
            new Vector3(transform.position.x,transform.position.y+0.2f,transform.position.z), Quaternion.identity) as GameObject;
        var dropCube2 = dropGo2.GetComponent<DropCube>();
        dropCube2.InitMtl(20);
        Destroy(gameObject);

    }
    private void Move()
    {
        if (jumpTimer <= 0)
        {
            if (Vector3.Distance(originPos, transform.position) <= 1)
            {
                enemyRig.AddForce(Vector3.up * 350);
            }
            jumpTimer = Random.Range(2.5f, 6f);
            originPos = transform.position;

        }
        playerPos = GameSceneManager.Instance.player.position;

        jumpTimer -= Time.fixedDeltaTime;
        Transform transform1;
        (transform1 = transform).rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(playerPos.x, 0, playerPos.z) - transform.position), rotateSpeed * Time.fixedDeltaTime);

        transform1.position += transform1.forward * (speed * Time.fixedDeltaTime);
    }
    void Update () {
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Player")
        {
            EventDispatcher.TriggerEvent<int>(E_MessageType.Hurt,1);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
}
