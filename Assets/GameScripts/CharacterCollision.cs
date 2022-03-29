using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This class will handle Collision in the game and update Score and Health of player. 
public class CharacterCollision : MonoBehaviour
{
    MainCharacter MC;       //Reference to MainCharacter Class.
    public Text ScoreText; //This is to show score in the UI Canvas during gameplay.
    public Text MCHealth;  //This is to show Health of player in the UI Canvas during gameplay.

    private void Awake()
    {
        MC = gameObject.GetComponent<MainCharacter>();
        

    }
    // Start is called before the first frame update
    void Start()
    {      
        
        ScoreText.text = "Score : " + MC.Score.ToString();  //Show current score on screen.
        MCHealth.text = "Lives : " + MC.HealthOfCharacter.ToString();  //Show current health on screen.

    }

    #region Collision Detection.
    private void OnTriggerEnter(Collider other) //Other is the object the Player collides with.
    {
        
        if(other.gameObject.CompareTag("Coin"))
        {
           // Debug.Log("Collision with coin");
            Destroy(other.gameObject);   //Destroy coin.
            MC.Score += 5;               //Increase Score by 5.        
            ScoreText.text = "Score : "+MC.Score.ToString(); //Shows score on screen as an UI
           // Debug.Log(MC.Score);
        }

        if (other.gameObject.CompareTag("Asteroids"))
        {
            // Debug.Log("Collision with Asteroids");
            Destroy(other.gameObject);  //Destroy Asteroids.
            MC.HealthOfCharacter -= 1;  //Decrease health by 1 everytime u hit an asteroid.
            MCHealth.text = MC.HealthOfCharacter.ToString();  //Update Life UI text.

            //Debug.Log(MC.HealthOfCharacter);
        }
    }
    #endregion
}
