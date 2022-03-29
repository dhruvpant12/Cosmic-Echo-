using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private string CharacterName; //For future.
    public int HealthOfCharacter; //5 
    public int velocity; // In case we want to change the speed during runtime.
    public int Score; // This will so how many coins have been collected.
    
    public Vector3 StartingPosition;


    //Player swayed from side to side , giving an illusion that its surfing.
    #region OscillationOfCharacter
    /*Vector3 from = new Vector3(30f, 180f, 0f);
    Vector3 to = new Vector3(20f, 180f, 0f);
    float speed = 1f;*/
    #endregion

    private void Awake()
    {
        CharacterName = "Lobo";
        
        velocity = 50;
        Score = 0;
       
    }                          

    // Start is called before the first frame update
    void Start()
    {
        HealthOfCharacter = 5;
    }

    // Update is called once per frame
    void Update()
    {

        //Below code is causing issue with Player Controll for rotation. Temporarily disabled untill solution is found.
       /* #region OscillationOfCharacter
        float t = (Mathf.Sin(Time.time * speed * Mathf.PI * 2f) + 1f) / 2f;
        transform.eulerAngles = Vector3.Lerp(from, to, t);
        #endregion*/

       
    }
}
