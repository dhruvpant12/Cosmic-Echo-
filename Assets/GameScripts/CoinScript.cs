using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is attached to the prefab of Coin Object. It will cause the coin to rotate on the Y axis. 
public class CoinScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       transform.Rotate(0f , 100f*Time.deltaTime , 0f);

        
    }
}
