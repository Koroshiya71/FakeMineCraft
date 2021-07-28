using System;
using UnityEngine;
using System.Collections;
using GameCore;

public class DropCube : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool isChecking=false;

    private int thisCubeId;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Vector3 flyDir = Vector3.Normalize(transform.position- GameSceneManager.Instance.player.position  );
        rigidbody.AddForce(flyDir*200);
        //延时启用拾取检测
        Invoke("StartCheckGet",0.5f);
    }
    //初始化材质纹理
    public void InitMtl(ushort id)
    {
        if (id==70)
        {
            id = 7;
        }
        GetComponent<Renderer>().material.mainTexture
            = ResourcesManager.Instance.LoadResources<Texture>(GameDefine.texDicPath[id]);

        thisCubeId = id;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isChecking)
            {
                GameDebuger.Log(thisCubeId);
                AllCompose.Instance.GetStuff(thisCubeId, BagData.Instance.BagCuts);
                Destroy(this.gameObject);
            }
        }
    }

    private void StartCheckGet()
    {
        isChecking = true;
    }
}
