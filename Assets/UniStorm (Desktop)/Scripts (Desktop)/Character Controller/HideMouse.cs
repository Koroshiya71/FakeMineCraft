using UnityEngine;
using System.Collections;
using GameCore;

public class HideMouse : MonoBehaviour 
{
	private bool HideMouseToggle;

	void Start () 
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

	void Update () 
	{
        
		if (Input.GetKeyDown(KeyCode.C))
		{
			HideMouseToggle = !HideMouseToggle;
		}

		if(HideMouseToggle)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
		}

		if(!HideMouseToggle)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
    void OnMouseDown()
    {
        GameDebuger.Log("dasdas");
    }

    void OnMouseUp()
    {
        GameDebuger.Log("qweqwrqrw");
    }
}
