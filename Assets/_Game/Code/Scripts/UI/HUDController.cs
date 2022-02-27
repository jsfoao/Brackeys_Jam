using System;
using UnityEngine;
using UnityEngine.Events;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu;
    public bool isPaused;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnEnd;

    private PostFXController _postFXController;
    

    public void CheckPause()
    {
        if (isPaused)
        {
            SetResume();
        }
        else
        {
            SetPause();
        }
    }

    public void PausedBehaviour()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void SetEnd()
    {
        PausedBehaviour();
        endMenu.SetActive(true);
        OnEnd.Invoke();
    }
    public void SetPause()
    {
        PausedBehaviour();
        pauseMenu.SetActive(true);
        OnPause.Invoke();
    }

    public void SetResume()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        OnResume.Invoke();
    }

    private void Awake()
    {
        _postFXController = FindObjectOfType<PostFXController>();
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);

        if (_postFXController != null)
        {
            OnPause.AddListener(_postFXController.LoadPauseProfile);
            OnResume.AddListener(_postFXController.LoadResumeProfile);
            OnEnd.AddListener(_postFXController.LoadPauseProfile);
        }
    }
}
