using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 

//This class will be responsible for providing the fileName of the scene . This information is retrieved from the inspector. Every time a scene 
//(new or old) is loaded , the filename will help to track down the actual name of the file that contains the coordinates of the coins 
//in the scene.Also, this class handles the Audio, Pause and Death logic as well.
public class GameHead : MonoBehaviour
{

    

    // Scene Name,File Name and PlayerPref
    // SCENE NAME           FILE NAME          PLAYERPREF
    // DrFeelGood         /DrFeelGood          DrFeelGood



    public string FileName; //This variable is used by the SavingLevel class to save and load the level and populate the scene with coins.
    public string PrefName; //This will hold the Highscore for the particular game level. Value retrieved from the inspector


    GameObject Player;
    MainCharacter MC; // Reference to Player class called Main Character.
    CharacterControls CC; //Reference to the Player movement scripts called CharacterControls.
    public Text HighScoreText; //UI to show Highscore.


    AudioSource AudioClip; // Reference to the song to be played.

    public GameObject UICanvas; //Canvas that has user input and scores.

    public GameObject WinCanvas; // Canvas that becomes active wehen the player win the level.
    public Text WinScoreText; //UI to be shown upon player winning. SHows current score. 
    public Text WinHighScoreText; //UI to be shown upon player winning. SHows High Score.

    public GameObject DeathCanvas;  //When Player dies , Canvas comes up.
    public Text DeathScoreText; // UI to be shown upon player death. Shows current score.
    public Text DeathHighScoreText;// UI to be shown upon player death. Shows high score.

    public static bool PlayAudio=true; // We will first check if nessecary objects are loaded first and scene is ready to be generated with coins. Once approved , audio will play to start the game.
    private bool PlayOnce; //This will ensure Audio is played only once . 

    private bool deathtrigger; //This is a loop check to remove a bug.
    private bool wintrigger;//This is a loop check to remove a bug.

    [SerializeField] private int HighScore; //Highscore retrived from the playerpref of the scene.

    Vector3 LastCoinPosition = new Vector3();
    private void Awake()
    {

        //Check if PlayerPref by the name exists or not.
        if (PlayerPrefs.HasKey(PrefName))
        {
            //PlayerPref exists.
        }
        else
        {
            PlayerPrefs.SetInt(PrefName, 0); //Create one if it doesnt exist.
        }


        MC = GameObject.FindObjectOfType<MainCharacter>();
        CC= GameObject.FindObjectOfType<CharacterControls>();
        Player = GameObject.FindGameObjectWithTag("Player");
        AudioClip = GetComponent<AudioSource>(); //Attaching the audio clip attached to gameoject.


    }
    private void Start()
    {
        UICanvas.SetActive(true); //PLayer Input UI and Score active at the start of the game.
        DeathCanvas.SetActive(false);
        WinCanvas.SetActive(false);
        CC.enabled = true;
        HighScore = PlayerPrefs.GetInt(PrefName); //Highscore retrieved from the inspector.
        HighScoreText.text = "HighScore: " + HighScore.ToString();
        MC.HealthOfCharacter = 5;
        PlayOnce = true; //Trigger to control Audio.
        deathtrigger = false;//Trigger to control a bug.
        wintrigger = false;
        LastCoinPosition.x = SavingLevel.coinData[SavingLevel.coinData.Count - 1].position[0];
        LastCoinPosition.x = 0;

    }

    private void Update()
    {
       
        if (PauseMenu.IsGamePause) // If Game is paused, pause the song playing.
        {
            AudioClip.Pause();
        }
        else
        {
            AudioClip.UnPause();
        }
        if (PlayAudio==true && PlayOnce==true) //Music starts and is only played once.
        {
            AudioClip.Play(); //Music starts in game . Game will start as well with character moving.
            PlayOnce = false;
            
        }
       
        if(MC.Score>HighScore) //Checking if new Highscore is reached.
        {
            HighScore = MC.Score;
            PlayerPrefs.SetInt(PrefName, HighScore);
            HighScoreText.text = "HighScore: "+HighScore.ToString();
        }


        if(MC.HealthOfCharacter==0 && !deathtrigger) //If Player dies , game is stoped and DeathCanvas is made active.
        {
            Time.timeScale = 0;
            
            AudioClip.Stop();
            UICanvas.SetActive(false);
            DeathCanvas.SetActive(true);
            DeathHighScoreText.text = "HighScore : "+HighScore.ToString();
            DeathScoreText.text = "Your Score :"+MC.Score.ToString();
            deathtrigger = true;

        }
    }

    private void FixedUpdate()
    {
        //If the player crosses the last coin in the scene. The game is won. The game will pause and a Win panel will show up.
        if (Player.transform.position.x > LastCoinPosition.x + 10 && !wintrigger)
        {
            AudioClip.Stop();
            Time.timeScale = 0;
            UICanvas.SetActive(false);
            WinCanvas.SetActive(true);
            WinHighScoreText.text = "HighScore : " + HighScore.ToString();
            WinScoreText.text = "Your Score :" + MC.Score.ToString();
            wintrigger = true;
        }
    }

    public void RestartLevel()  //Function to restart the level. Reference to the Retry button in the deathpanel
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ExitToSongMenu()//Function to change scene and to go songmenu. Reference to the ExitTOSongMenu button in the deathpanel
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SongMenu");
    }
}
