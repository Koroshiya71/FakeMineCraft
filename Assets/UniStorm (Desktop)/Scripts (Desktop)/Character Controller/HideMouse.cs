using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HideMouse : MonoBehaviour
{
    private bool HideMouseToggle;

    private bool isMouseDown = false;

    public bool IsMouseDown
    {
        get
        {
            return isMouseDown;
        }
    }

    void Start()
    {
        HideMouseToggle = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        EventDispatcher.AddListener(E_MessageType.OpenGameUI, delegate
        {
            HideMouseToggle = false;
        });
        EventDispatcher.AddListener(E_MessageType.CloseGameUI, delegate
        {
            HideMouseToggle = true;
        });

        
    }

    void Update()
    {   

        if (Input.GetKeyDown(KeyCode.C))
        {
            HideMouseToggle = !HideMouseToggle;
        }

        if (HideMouseToggle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        if (!HideMouseToggle)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;            
        }

    }


}
