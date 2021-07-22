using System;
using UnityEngine;
using System.Collections;

public class DropCube : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool isChecking=false;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Vector3 flyDir = Vector3.Normalize(transform.position- GameSceneManager.Instance.player.position  );
        rigidbody.AddForce(flyDir*500);
        Invoke("StartCheckGet",0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (isChecking)
            {
                //TODO:获取方块到背包

                Destroy(this.gameObject);
            }
        }
    }

    private void StartCheckGet()
    {
        isChecking = true;
    }
}
