using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is attached to an Empty Gameobject that operates outside the screen of the gameplay. It moves behind the player. It is used to 
// destroy Coins and Asteroids once they are passed the player screen. This is done to reduced the number of active game objects in the scene.
public class DestroyerScript : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       transform.Translate(new Vector3(1f, 0f, 0f), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
          //  Debug.Log("Collision with coin Destroyer ");
            Destroy(other.gameObject);
         
            
        }

        if (other.gameObject.CompareTag("Asteroids"))
        {
            Debug.Log("Collision with ass");
            Destroy(other.gameObject);
            
        }
    }

    

}
