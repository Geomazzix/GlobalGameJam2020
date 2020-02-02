using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private PlayerControl m_playerController;

    private void Start()
    {
        m_playerController = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            DisableMenu();
    }

    public void ContinueGame()
    {
        DisableMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisableMenu()
    {
        m_playerController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
