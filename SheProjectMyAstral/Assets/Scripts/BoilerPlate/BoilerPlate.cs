using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoilerPlate : Singleton<BoilerPlate>
{
    private bool _GAME_IS_PAUSED = false;
    private float _FORMER_TIME_SCALE;
    [SerializeField] private AudioClip click;
    
    public void GoToScene(string SceneName)
    {
        //if(playSFX)
        //{
        ///Hey Jason.Taking this out for error sake 
        //SoundFXManager.instance.playSoundFxClip(click, transform, 1f);
        //}
        print("Going to " + SceneName);
        SceneManager.LoadScene(SceneName);
        _GAME_IS_PAUSED = false;
        Time.timeScale = 1.0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        if (click != null) SoundFXManager.instance.playSoundFxClip(click, transform, 1f);
        StartCoroutine(QuitTimer());
        print("If you are seeing this the game should end right now.");
        //Hey future Ryan, if this breaks the build you are an unloveable chud and you should comment the line out below. 
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

    IEnumerator QuitTimer()
    {
        yield return new WaitForSeconds(click.length);
        Application.Quit();
    }

    public void PauseGame()
    {
        SoundFXManager.instance.playSoundFxClip(click, transform, 1f);
        if (_GAME_IS_PAUSED)
        {
            return;
        }

        _GAME_IS_PAUSED = true;
        _FORMER_TIME_SCALE = Time.timeScale;

        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        SoundFXManager.instance.playSoundFxClip(click, transform, 1f);
        if (!_GAME_IS_PAUSED)
        {
            return;
        }
        _GAME_IS_PAUSED = false;
        Time.timeScale = 1.0f;
    }

    public void ResumePreviousGameSpeed()
    {
        SoundFXManager.instance.playSoundFxClip(click, transform, 1f);
        if (!_GAME_IS_PAUSED)
        {
            return;
        }
        _GAME_IS_PAUSED = false;
        Time.timeScale = _FORMER_TIME_SCALE;
    }

    public void TogglePause()
    {
        SoundFXManager.instance.playSoundFxClip(click, transform, 1f);
        //_GAME_IS_PAUSED ? ResumeGame() : ResumePreviousGameSpeed();

        if (_GAME_IS_PAUSED)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
