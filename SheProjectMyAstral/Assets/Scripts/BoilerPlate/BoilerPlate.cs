using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoilerPlate : Singleton<BoilerPlate>
{
    private bool _GAME_IS_PAUSED = false;
    private float _FORMER_TIME_SCALE;
    
    public void GoToScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("If you are seeing this the game should end right now.");
        //Hey future Ryan, if this breaks the build you are an unloveable chud and you should comment the line out below. 
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

    public void PauseGame()
    {
        if(_GAME_IS_PAUSED)
        {
            return;
        }

        _GAME_IS_PAUSED = true;
        _FORMER_TIME_SCALE = Time.timeScale;

        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        if(!_GAME_IS_PAUSED)
        {
            return;
        }
        _GAME_IS_PAUSED = false;
        Time.timeScale = 1.0f;
    }

    public void ResumePreviousGameSpeed()
    {
        if (!_GAME_IS_PAUSED)
        {
            return;
        }
        _GAME_IS_PAUSED = false;
        Time.timeScale = _FORMER_TIME_SCALE;
    }

    public void TogglePause()
    {
        //_GAME_IS_PAUSED ? ResumeGame() : ResumePreviousGameSpeed();
        
        if(_GAME_IS_PAUSED)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
