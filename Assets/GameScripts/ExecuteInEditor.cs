using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is only active during development of level design.Otherwise during actual gameplay is it disabled.
//We use this class to generate coins into the Editor Heirachy using the playerpref stored in CoinGeneration Class. 


[ExecuteInEditMode] //This will execute in the editor.
public class ExecuteInEditor : MonoBehaviour
{
    public GameObject Coin; //Reference to the prefab coin.

    //We are going to read from the two playerpref strings containing the X and Y coordinates and save these values in Xcord and Ycord.
    struct Coord
    {
        public float  Xcord; // X coordinate.
        public float Ycord; // y coordinate.

        public  Coord(string Xcord,string Ycord)
        {
            this.Xcord = float.Parse(Xcord); //String values obtained from the playerpref.
            this.Ycord = float.Parse(Ycord); //String values obtained from the playerpref.
        }

        public void printStruct()  //Function for debugging.
        {
            Debug.Log("Xcord: " + Xcord + "   Ycord: " + Ycord);
        }
    }

    int counterX, counterY; //variables for checking bugs.

    private List<Coord> leveldesign; // After objects of Struct Coord are initialised , they are saved into a list for access.

    string Xpathing, Ypathing; // Xpathing is for playerpref containing X coordinates . Ypathing is for Y coordinates.
    // Start is called before the first frame update
    void Start()
    {
         
        leveldesign = new List<Coord>();

        Xpathing = PlayerPrefs.GetString("Xcor"); //Playerpref Xcor from Class CoinGeneration.
        Ypathing = PlayerPrefs.GetString("Ycor"); //Playerpref ycor from Class CoinGeneration.

        ReadingFromFile(Xpathing, Ypathing); 

        GeneratingTheScene();

        printPlayer();
    }

    //This function reads through the playerpref and takes out each coordinate. The playerpref string is seperated by commas.So, we will iterate
    // through both the playerpref strings and get values at each comma seperation and initialze an object of Struct Coord. 
    void ReadingFromFile(string Xstring,string Ystring)
    {
        int i=0, j=0;

        string XCordToStruct="0", YCordToStruct="0";  //SubStrings to initialize with Coordinates.    



        bool buildingList = true;

        while(buildingList)
        {
            //WOrking on X coordinate PlayerPref string.
            while(i<Xstring.Length) //Reading a character at a time
            {
                //If comma is encountered, we have read through a value. The loop will break and the string will be used to initialize an object of the Struct Coord along with its Y coordinate value.
                if (Xstring[i] == ',')
                {
                    i++;
                    break;                    
                }
                else
                {
                    XCordToStruct = XCordToStruct + Xstring[i]; 
                    i++;
                    counterX++;
                }
            }

            //Working on Y coordinate PlayerPref string.
            while (j<Ystring.Length)
            {
                if(Ystring[j]==',')
                {
                    j++;
                    break;
                }
                else
                {
                    YCordToStruct = YCordToStruct + Ystring[j];
                    j++;
                    counterY++;
                }
            }

            if (XCordToStruct == "," && YCordToStruct == ",")
            {
                //DO nothing.
            }
            else
            {
                //Once Commas are encountered in each playerpref string , we have our X and Y coordinates . We will initiaze an object of the struc now.
                leveldesign.Add(new Coord(XCordToStruct, YCordToStruct)); //Adding to list.
            }


            XCordToStruct = "";YCordToStruct = ""; // Making string empty and ready from new value after the comma.

            if (i == Xstring.Length && j == Ystring.Length) //Checking if both strings are read . If yes , we can exit the loop.
                buildingList = false;
        }

        //Debug to check is succesful.
        /*if (counterX == counterY)
            Debug.Log("Updating Struct succcesful :" + counterX);*/

    }

    //This function will generate coins in the Scene and the Editor. 
    void GeneratingTheScene()
    {
        foreach(Coord nodes in leveldesign)
        {
            Instantiate(Coin, new Vector3(nodes.Xcord, nodes.Ycord, 0f), Quaternion.identity);
        }
    }

    void printPlayer()
    {
        Debug.Log(Xpathing);Debug.Log(Ypathing);
        foreach(Coord element in leveldesign)
        {
            Debug.Log(element.Xcord + " " + element.Ycord);
        }
    }
}
