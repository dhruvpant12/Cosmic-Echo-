using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is used to pause the game during gameplay.
public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePause; //Static because we just want to refer this variable and not the whole script. Will be used to stop the music.
    public GameObject PauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
        IsGamePause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame() //Resume the game. Resume the audio as well.
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        IsGamePause = false;
    }

    public void PauseGame() //Pause the game. Stops the audio as well.
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
        IsGamePause = true;
    }

    public void Restart() // Level is restarted from the beginning. Reference to the Retry button in the pause panel.
    {
        Time.timeScale = 1;
        

        Invoke(nameof(ChangeScene),1.5f); //Using Invoke to give some delay between scene transition. 
    }

    public void ExitToSongMenu() // Exit scene and switch to scene with song menu. Reference to the ExitToSongMenu button in the pause panel.
    {
       // Time.timeScale = 1;
        SceneManager.LoadScene("SongMenu");
    }

    public void ExitToMenu() // This will cause the user to exit to the Main Menu. Reference to the MainMenu button in the pause panel.
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameMenu");
    }

    public void ChangeScene()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
