using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private Vector3 lastPos;
    private float jumpTimer=2.5f;
    private Vector3 playerPos;
    private float speed = 0.5f;
    private Rigidbody enemyRig;
    private Vector3 originPos;
	void Start ()
    {
        playerPos = GameSceneManager.Instance.player.position;
        enemyRig = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        if (jumpTimer<=0)
        {
            enemyRig.AddForce(Vector3.up*300);
            jumpTimer = 2.5f;
            GameDebuger.Log("jump!!!");
        }

        jumpTimer -= Time.deltaTime;
        playerPos = GameSceneManager.Instance.player.position;
        transform.LookAt(playerPos);
        Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);

    }
	void Update () {
	    Move();
	}
}
