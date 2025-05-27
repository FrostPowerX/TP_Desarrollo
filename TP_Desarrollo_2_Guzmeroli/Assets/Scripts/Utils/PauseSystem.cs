using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    [SerializeField] InputActionAsset inputActions;

    InputAction back;

    private void Awake()
    {
        inputActions.Enable();

        back = inputActions.FindAction("Back");

        back.started += PauseGame;
    }

    private void OnDestroy()
    {
        back.started -= PauseGame;
    }

    void PauseGame(InputAction.CallbackContext cont)
    {
        if (Time.timeScale >= 1.0f)
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }
}
