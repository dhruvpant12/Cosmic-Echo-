using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


//We are using this class to Store and Load the coins in the scene . After generating coins in the editor using ExecuteInEditMode , the coins
// will be visible in the Hierachy. This class has two functions called SavingLevel and LoadingLevel. We will use SavingLevel to save the
// coins in the scene into a file . We do this by just clicking play in the gamemode and the coins will be automatically saved to a file. After
// that we can disable the function . During gameplay, we want to coins to load into the game slowly and not at once. So, when the actual game
// is played , the LoadingLevel function will be called which will access the stored file and retrieve all the coordinates of the coins in the
// scene. We are goin to use BinaryFormatter to store and load data from the files. 
public class SavingLevel : MonoBehaviour
{
    public GameObject Coin; //Reference to prefab Coin.
    private GameHead GH; //Refernce to the Class GameHead. GameHead class has the name of the scene as string to be saved to file or loaded from file.
    string FileName; //Name of the file of the  scene.
    string _CoinCount; //Will store number of coins in a file for loading reference.
    int NoOfCoins;//This will hold the number of coins in the scene.

    List<GameObject> CoinInScene; // To store coins in scene.
     GameObject temp; // To be used in sorting algorithm as a temporary holder during swaps.

    public static List<CoinData> coinData; //List used to load Coin in the scene

    private void Awake()
    {
        GH = GameObject.FindObjectOfType<GameHead>();
        FileName = GH.FileName;

        // SaveCoinsInScene() function will be called when we want to save the coins in the scene to a file. Otherwise it will stay disbabled during game play.
        //SaveCoinsInScene(); 

        //Below function will retrieve data from the saved file and this data will be saved in a list . The list will be used to generate coins in the scene.
        LoadCoinsInScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    #region Sorting The List.

    //We are using Quick Sort Here.
    void Sort(List<GameObject> Coin, int left, int right)  
    {
        if(left<right)
        {
            int pIndex = Partition(Coin, left, right); //pindex is the Pivot Index.
            
            Sort(Coin, left, pIndex - 1);
            Sort(Coin, pIndex + 1, right);
        }
    }

    int Partition(List<GameObject> Coin, int left, int right)
    {
        float pivot = Coin[right].transform.position.x; // Passing the x coordinate of the coin.
        int i = left - 1;
        for(int j=left;j<right;j++)
        {
            if(Coin[j].transform.position.x < pivot)
            {
                i++;
                Swap(Coin, i, j);
            }
        }

        Swap(Coin, i + 1, right);
        return i+1;
    }

    void Swap(List<GameObject> Coin, int i, int j)
    {
        temp = Coin[i];
        Coin[i] = Coin[j];
        Coin[j] = temp;
    }
    #endregion


    #region SavingScene
    void SaveCoinsInScene()
    {
        _CoinCount = FileName + "Coin Count"; //This file name has the number of coins in the scene.
        CoinInScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin")); // Adding all coins in the scene with Tag as "Coin".
        NoOfCoins = CoinInScene.Count; //This will hold the number of coins in the scene.

        #region Debug
        /*foreach (GameObject coin in CoinInScene) //Checking if storing is successful.
           {
               Debug.Log(coin.transform.position);
               counter++;

           }
        int counter;
           Debug.Log(counter);*/
        #endregion


        //The data stored in List CoinInScene are in random order. We require the list to have objects stored in it
        // in an ascending order of their X Coordinate. Otherwise if we instantiate from an unsorted list , the coins 
        //will be spawn in an non-sequential pattern which will ruin the level design.
        Sort(CoinInScene, 0, CoinInScene.Count - 1); //Using QuickSort.

        //Using Binary Formatter to store the coins in the list to a file.

        BinaryFormatter formatter = new BinaryFormatter();
        string filepath = Application.persistentDataPath + FileName + SceneManager.GetActiveScene().buildIndex; // Primary name of file for storing coin coordinates.

        //Store Count of Coins in file.
        string CoinCount= Application.persistentDataPath + _CoinCount + SceneManager.GetActiveScene().buildIndex; //Name of file to store number of coins in the list.
        FileStream CoinStream = new FileStream(CoinCount, FileMode.Create);
        formatter.Serialize(CoinStream, CoinInScene.Count);
        CoinStream.Close();

        //Storing coordinates of each coin in files.
        for (int i=0;i<NoOfCoins;i++)
        {
            //Storing each coin's coordinates in different files. 
            FileStream fileStream = new FileStream(filepath+i, FileMode.Create);
            //CoinData data=new CoinData(CoinInScene[i].transform.position);
            CoinData data = new CoinData(CoinInScene[i].transform.position.x, CoinInScene[i].transform.position.y, CoinInScene[i].transform.position.z);
            formatter.Serialize(fileStream,data);
            fileStream.Close();
            Debug.Log("filed saved for coin " + i + "at" + CoinInScene[i].transform.position.x);

        }


    }

    #endregion


    #region Loading The FIle.
    void LoadCoinsInScene()
    {
        coinData = new List<CoinData>(); //list to load coins into the scene.
        _CoinCount = FileName + "Coin Count";// String to get number of coins from the saving phase.

        BinaryFormatter formatter = new BinaryFormatter();
        string filepath = Application.persistentDataPath + FileName + SceneManager.GetActiveScene().buildIndex; //Name of the file to be loaded.
        string CoinCount = Application.persistentDataPath + _CoinCount + SceneManager.GetActiveScene().buildIndex;//Name of the file needed for number of coins.

        int coincount=0;

        
        if (File.Exists(CoinCount))
        {
            FileStream coinstream = new FileStream(CoinCount, FileMode.Open);
            coincount = (int) formatter.Deserialize(coinstream);
            coinstream.Close();
            //No of coins in scene read from saved file.
        }
        else
        {
            Debug.LogError("Count Stream not found");
        }

        for (int i = 0; i < coincount; i++)
        {

            if(File.Exists(filepath+i)) //Accessing file names for each coin in the list.
            {
                FileStream filestream = new FileStream(filepath + i, FileMode.Open);
                CoinData data = formatter.Deserialize(filestream) as CoinData;
                filestream.Close();
                //File read for a coin.

                coinData.Add(data); //CoinData object saved to list for loading . This is not the same as the Coin object.It contains coordinates of the position.
                
                
                
            }
            else
            {
                Debug.LogError("File Stream not found at" + i);
            }
        }

       
    }

    #endregion


}
