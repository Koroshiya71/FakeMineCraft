using System;
using UnityEngine;
using System.Collections;
using GameCore;
public class DayAndNight : UnitySingleton<DayAndNight>
{
    private bool isActive = false;
    private float changeSpeed = 0.03f;
    private Light sunLight;
    private Light reflectLight;
    private float currentColorRate = 0.0f;
    private Material dayMtl;
    private Material duskMtl;
    private Material nightMtl;
    private Material dawnMtl;
    private bool cursorLocked=true;
    private void UpdateCursorLock()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            cursorLocked = !cursorLocked;
        }

        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
 
    private void Start()
    {
        isActive = false;
        dayMtl = ResourcesManager.Instance.LoadResources<Material>(GameDefine.skyBoxPath[E_SkyBoxType.Day]);
        duskMtl = ResourcesManager.Instance.LoadResources<Material>(GameDefine.skyBoxPath[E_SkyBoxType.Dusk]);
        nightMtl = ResourcesManager.Instance.LoadResources<Material>(GameDefine.skyBoxPath[E_SkyBoxType.Night]);
        dawnMtl = ResourcesManager.Instance.LoadResources<Material>(GameDefine.skyBoxPath[E_SkyBoxType.Dawn]);

        EventDispatcher.AddListener(E_MessageType.EnterGameScene, delegate
        {
            sunLight = GameObject.Find("SimpleSun").GetComponent<Light>();
            reflectLight = GameTool.GetTheChildComponent<Light>(sunLight.gameObject, "reflected");  
            isActive = true;
        });
    }

    private void Update()
    {
        if (isActive)
        {
            UpdateLightColor();

        }
        UpdateCursorLock();

    }

    private void UpdateLightColor()
    {
        currentColorRate += Time.deltaTime * changeSpeed;

        if (currentColorRate>=1.0f || currentColorRate <= 0)
        {
            changeSpeed = -changeSpeed;
        }
        if (currentColorRate >= 0.8f)
        {
            Camera.main.GetComponent<Skybox>().material = nightMtl;

        }
        else if (currentColorRate >= 0.5f)
        {
            Camera.main.GetComponent<Skybox>().material = duskMtl;
        }
        else if(currentColorRate>=0.2f)
        {
            Camera.main.GetComponent<Skybox>().material = dawnMtl;

        }
        else
        {
            Camera.main.GetComponent<Skybox>().material = dayMtl;

        }
        sunLight.color = Color.Lerp(new Color32(255,242,199,1), new Color32(0, 0, 0,1),currentColorRate);
        reflectLight.color = Color.Lerp(new Color32(126, 234, 255, 1), new Color32(0, 0, 0, 1), currentColorRate);
    }
}
