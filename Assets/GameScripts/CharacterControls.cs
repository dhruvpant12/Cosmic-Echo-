using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Class has the controlls for Player and Developer seperatly. Mobile input for player and PC controls for developers. Use Dev controls for making the level design.
public class CharacterControls : MonoBehaviour
{
    private MainCharacter MC; // Reference to Main Character Class.
     
    private Vector3 boundary = new Vector3(); // We will use to to clamp player , so it doesnt shoot off the screen.

    public static int DesignIndex;

    //For Step Level.
    bool center = true, left = false, right = false;
    float speed = 35;


    //For Circular Rotation.
    public GameObject gb; //Point around which player will rotate.
    float RotateMax = 90f, RotateMin = -90f; //Values to clamp player rotation.

    
    //For User input.
    bool LeftInput = false, RightInput = false;// Boolean checks for input control.

    //Developer Code for Level Design. //DONOT DELETE.
    // private CoinGeneration CG; //Reference to Coin Generation class. It will Instantiate coins during test run and save coin coordinates in a playerpref.

    private void Awake()
    {
        DesignIndex = 1;

    }

    // Start is called before the first frame update
    void Start()
    {

        MC = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();
        


        //Developer Code for Level Design. //DONOT DELETE.
        // CG = GameObject.FindObjectOfType<CoinGeneration>(); //Enable to create coins during test runs.


    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.E))
             DesignIndex = 1;
         if (Input.GetKeyDown(KeyCode.R))
             DesignIndex = 2;
         if (Input.GetKeyDown(KeyCode.T))
         {
             DesignIndex = 3;            
         }*/

        DesignIndex = LevelDesigner._DesignIndex;

        //Clamping player position to stay on screen.
        #region Clamping Player 

        switch (DesignIndex)
        {
            case 1:
                boundary = transform.position;
                boundary.y = Mathf.Clamp(transform.position.y, -30f, 30f);
                transform.position = boundary;
                break;

            case 2:
               /* if(transform.rotation.eulerAngles.x>90f)
                {                     
                    transform.rotation = Quaternion.Euler(90, 180, 0);
                }
                if(transform.rotation.eulerAngles.x <- 90f)
                {
                    transform.rotation = Quaternion.Euler(-90, 180, 0);
                }*/

                break;

            case 3:
                boundary = transform.position;
                boundary.y = Mathf.Clamp(transform.position.y, 0f, 30f);
                boundary.z = Mathf.Clamp(transform.position.z, -30f, 30f);
                transform.position = boundary;
                
                break;


        }
        #endregion
        if (transform.position.y <= 0)
            center = true;
        movement();       


        //Character Input for Developer for Generating Coins in Test Run for Level design.
        #region Character Input for Developer  
        if (Input.GetKeyDown(KeyCode.A)) //Move Up 15 units on Y axis.
        {
            if (transform.position.y == 30) //This will check if Designer at the uppermost end of the playable boundary . If yes , then move 15 units down.
                                            //This is done to avoid accidently move off screen and creating a coin , which will get saved in playerpref. 

            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 15, transform.position.z);

                //Developer Code for Level Design. //DONOT DELETE.
                // CG.InputCoinGeneration(this.gameObject); //Passing player as a reference to use its transform.

            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);

                //Developer Code for Level Design. //DONOT DELETE.
                // CG.InputCoinGeneration(this.gameObject);
            }
        }


        if (Input.GetKeyDown(KeyCode.L)) //Move Down 15 units on Y axis
        {
            if (transform.position.y == -30)//This will check if Designer at the bottommost end of the playable boundary . If yes , then move 15 units up.
                                            //This is done to avoid accidently move off screen and creating a coin , which will get saved in playerpref. 

            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);

                //Developer Code for Level Design. //DONOT DELETE.
                //CG.InputCoinGeneration(this.gameObject);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 15, transform.position.z);

                //Developer Code for Level Design. //DONOT DELETE.
                // CG.InputCoinGeneration(this.gameObject);
            }

        }

        //Developer Code for debugging. //DONOT DELETE.
        if (Input.GetKeyDown(KeyCode.D))
        {
            MC.HealthOfCharacter = 0;
        }
        #endregion


    }

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * MC.velocity);
        transform.Translate(new Vector3(1f, 0f, 0f), Space.World); //Moving along the positive X axis.
    }

    #region MobileInput
    public void MoveUp()
    {
        if (!PauseMenu.IsGamePause && DesignIndex == 1) //If game is not paused.
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, new Vector3(transform.position.x, 30, 0), .1f), 50);
       
        else if (!PauseMenu.IsGamePause && DesignIndex == 2)
                 transform.RotateAround(gb.transform.position, Vector3.right, 100 * Time.deltaTime);
       
        else if (!PauseMenu.IsGamePause && DesignIndex == 3)
        {
            if (center)
            {
                transform.Translate(0, speed * Time.deltaTime, -speed * Time.deltaTime, Space.World);
                center = false;
                left = false;
                right = true;
            }
            else if (left)
            {
                transform.Translate(0, -speed * Time.deltaTime, -speed * Time.deltaTime, Space.World);

            }
            else if (right)
            {
                transform.Translate(0, speed * Time.deltaTime, -speed * Time.deltaTime, Space.World);

            }
        }

    }

    public void MoveDown()
    {
        if (!PauseMenu.IsGamePause && DesignIndex == 1) //If game is not paused.
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, new Vector3(transform.position.x, -30, 0), .1f), 50);
        
        else if (!PauseMenu.IsGamePause && DesignIndex == 2)
                transform.RotateAround(gb.transform.position, Vector3.right, -100 * Time.deltaTime);
        
        else if (!PauseMenu.IsGamePause && DesignIndex == 3)
        {
            if (center)
            {
                transform.Translate(0, speed * Time.deltaTime, speed * Time.deltaTime, Space.World);
                center = false;
                left = true;
                right = false;
            }
            else if (left)
            {
                transform.Translate(0, speed * Time.deltaTime, speed * Time.deltaTime, Space.World);

            }
            else if (right)
            {
                transform.Translate(0, -speed * Time.deltaTime, speed * Time.deltaTime, Space.World);
            }
        }

    }
    #endregion

    void movement()
    {
        if (LeftInput)
            MoveDown();
        if (RightInput)
            MoveUp();
    }

    public void RightPointerDown()
    {
        RightInput = true;
    }
    public void LeftPointerDown()
    {
        LeftInput = true;
    }
    public void RightPointerUp()
    {
        RightInput = false;
    }
    public void LeftPointerUp()
    {
        LeftInput = false;
    }

    
}
