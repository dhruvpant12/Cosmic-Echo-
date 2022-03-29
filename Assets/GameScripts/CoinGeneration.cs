using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is only active during the development of level deisgn.Otherwise for actual gameplay , it is always disabled.
//This class is used to generate coins in the scene during gameplay. We are using to trick to decrese the time it takes us to level the design.
// Since the coins have to placed in the scene precisely with the rhythm of the song , manually putting coins in the scene using the Editor
// is a very unproductive task. So many adjustments have to be done . By using below method, all we have to do is only have the player in the scene
// and play the song during gameplay . We can use the player to move around the scene as per the rhythm of the song. THe function InputCoinGeneration
// is called whenever the designer presses an input key. A coin is instantiated at the position of the character when the designer presses 
// the input key. The coordinates of the coins are then saved into a playerpref. This prefab is used in another class called ExecuteInEditor where
// ExeciteInEditorMode is active . This generates all the coins into the hierachy by reading from the playerpref.
public class CoinGeneration : MonoBehaviour
{
    public GameObject Coin;
    private string XCor ="", YCor="";
    float x, y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void InputCoinGeneration(GameObject gb)
    {
        Instantiate(Coin, gb.transform.position, Quaternion.identity); //This is called in CharacterControls Class whenever the designer presses an input key during gameplay.
        x = (float)System.Math.Round(gb.transform.position.x,2); //X coord of coin.
        y = (float)System.Math.Round(gb.transform.position.y,2); //Y coord of coin.

        XCor =XCor + "," +  x.ToString(); //Adding the X coord of coins into a string seperated by a coin . This is to differentiate between different coins.
        YCor = YCor + "," + y.ToString();

        PlayerPrefs.SetString("Xcor", XCor); //Saving to Playerprefs.
        PlayerPrefs.SetString("Ycor", YCor);
    }
}
