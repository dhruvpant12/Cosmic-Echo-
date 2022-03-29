using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will be used to generate the level slowly. This is done to reduce the number of active objects in the scene and when the level
// design changes the layout , we donot want the whole level to be generated again . Instead , we will divide the level into sections. The
//arrangement of the coins in the scene are stored in a list  (their transform position in CoinData class) . We will access 25 elements at a 
//time every 2 seconds and populate the scene during gameplay. We will call each section as batches in the script.Each batch will be 
//send with a starting index and a last index , which will access a sub-set of the list. Also , we will keep track
// of the level design layout and pass this information as well , so that there is no overlap between batch population and level design when
// transition takes place. 
public class LevelDesigner : MonoBehaviour
{

     
    public GameObject Coin; //Coin in scene. Will instantiate it. 
    

    int[] _DesignIndexArray = new int[3] { 1,2,3};
    public static int _DesignIndex;


    int CoinCount ; //The number of coins for the scene. This is acquired from the list Coindata.
    Vector3 _LastCoinPosition; //Will use this for win condition. If player passes beyond this value, game is won. 

    int _StartingIndex; //This will be the starting index of the batch we will send to the level generator.
    int _EndingIndex;//This will be the last index of the batch for level generator.

    int _NoOfBatches;
    int _CoinsInLastBatch;
    const int IndexIncrement=24; // 25 elements of the batch. we will add +24 to the starting index of a batch to get the last index.
    int _BatchSize;
    int[,] _BatchElement; //We will store the starting index and last index of each batches in this array to pass to the level generator.

    Coroutine LevelCoroutine;
    WaitForSeconds timer=new WaitForSeconds(4f);
     
    private void Start()
    {
        _DesignIndex = _DesignIndexArray[0];
        
        if (SavingLevel.coinData.Count == 0) //If List is empty , level will not be designed.
        {
            Debug.LogError("List is empty.");

        }
        else
        {
            CoinCount = SavingLevel.coinData.Count;

            _LastCoinPosition.x = SavingLevel.coinData[CoinCount - 1].position[0]; //Fetching the X coordinate from the last element of the list.

            _StartingIndex = 0; //Initially , we are starting from the beginning of the list. Later on , this variable will be updated to be the starting index of the new sub-list.

            //We will create number of batches by dividing the length of the list with 25.This way we will know how many batches are to be 
            //produced. In most average cases , the length  of list will not be completly divisible by 25 , so there will be remainder. 
            //Therefore,we will tweak the last batch, so it will have less than 25 elements in it. 
            _NoOfBatches = CoinCount / 25; //This will tell us how many batches we have (excluding the remainder batch).

            _CoinsInLastBatch = CoinCount % 25;  // In case there is a remainder of less than 25 , this will be the last and shorter sub list.

            if(_NoOfBatches==0)  
            {
                //If the list has less that 25 elements in it, there will be only 1 batch to be produce.So we will initialize only an array of
                //size (1,2) so store the starting index that is 0 and the last index that would be the remainder.
                _BatchElement = new int[_NoOfBatches+1, 2]; // 0+1,2
                _BatchElement[0, 0] = _StartingIndex; // index value 0
                _BatchElement[0, 1] = _CoinsInLastBatch - 1; //
                _BatchSize = 1;

            }
            else if(_NoOfBatches!=0 && _CoinsInLastBatch!=0)
            {
                //If there is more than 1 batches to be produced and the last batch has less than 25 elements, we will initialize the array in
                // two seperate steps. First, all the batches except the last one will have 25 elements to them . So we will use a forloop 
                // to fill up the array . Then for the last batch , we will access the last element of the array and hard code the values to it.
                // It will have the _startingindex value from the forloop . The endingindex will be the startingindex + the remining coins. 
                //for example , there are 77 elements in the list . 77/25 =3 and 77%25 =2 . Therefore there are 4 batches in total , with the 
                // last batch only having 2 elements. So, we will quickly fill up the first 3 batches with 25 elemenets and assign value to the 
                // last batch directly. 
                _BatchSize = _NoOfBatches + 1;
                _BatchElement = new int[_NoOfBatches + 1, 2]; // N+1,2
                _EndingIndex = _StartingIndex + 24;
                for(int i=0;i<_NoOfBatches;i++)
                {
                    // Filling up batches with 25 elements.
                    _BatchElement[i, 0] = _StartingIndex;
                    _BatchElement[i, 1] = _EndingIndex;
                    _StartingIndex = _EndingIndex + 1;
                    _EndingIndex = _StartingIndex + 24;

                }
                // Last batch.
                _BatchElement[_NoOfBatches, 0] = _StartingIndex;
                _BatchElement[_NoOfBatches, 1] = _StartingIndex+_CoinsInLastBatch-1;


            }
            else if(_NoOfBatches!=0 && _CoinsInLastBatch ==0)
            {
                //This means that the number of elements in the list was completly divided by 25 and there is no remainder . Therefore , all the 
                // batches will have 25 elements to it. 
                _BatchSize = _NoOfBatches;
                _BatchElement = new int[_NoOfBatches , 2]; //N,2
                _EndingIndex = _StartingIndex + 24;
                for (int i = 0; i < _NoOfBatches; i++)
                {
                    // Filling up batches with 25 elements.
                    _BatchElement[i, 0] = _StartingIndex;
                    _BatchElement[i, 1] = _EndingIndex;
                    _StartingIndex = _EndingIndex + 1;
                    _EndingIndex = _StartingIndex + 24;

                }

            }

            /*Debug.Log(CoinCount);
            for (int i = 0; i < _BatchElement.GetLength(0); i++) //i<_BatchElement.GetLength(0) will give the number of elements in the first dimension.
            {
                Debug.Log(_BatchElement[i, 0] + "    " + _BatchElement[i, 1]);
            }*/

                     
            LevelCoroutine = StartCoroutine(LevelGenerator(_BatchElement));
        }
    }

    #region LevelLayout
    public void ChangeLevel(int i, int StartingIndex, int LastIndex)
    {
         
        switch(i)
        {
            case 1:
                 StraightLevel( StartingIndex,  LastIndex);
                    break;
            case 2:
                 StepsLevel(StartingIndex, LastIndex);
                    break;
            case 3:
                  CircleLevel(StartingIndex, LastIndex);
                    break;
            default:break;
        }

        
    }

    public void StraightLevel(int StartingIndex, int LastIndex)
    {
        Vector3 position = new Vector3(); 
        for (int i= StartingIndex; i<= LastIndex; i++)
        {
              position = new Vector3(SavingLevel.coinData[i].position[0], SavingLevel.coinData[i].position[1], SavingLevel.coinData[i].position[2]);
            Instantiate(Coin, position, Quaternion.identity);
        }
        
    }

   public void StepsLevel(int StartingIndex, int LastIndex)
    {
        Vector3 position = new Vector3()  ;
        for (int i = StartingIndex; i <= LastIndex; i++)
        {
            float x= SavingLevel.coinData[i].position[0], y=0, z=0;
           
            switch(SavingLevel.coinData[i].position[1])
            {
                case 0: y = 0; z = 0;break;
                case 15:y = 15f;z = 15f;break;
                case 30: y = 30f;z = 30f;break;
                case -15: y = 15f;z = -15f;break;
                case -30: y = 30f;z = -30f;break;
               /* case 0: y = -30; z = 0; break;
                case 15: y = -15f; z = 15f; break;
                case 30: y = 0f; z = 30f; break;
                case -15: y = -15f; z = -15f; break;
                case -30: y = 0f; z = -30f; break;*/
            }
             position = new Vector3(x,y,z);
            //Vector3 position = new Vector3(SavingLevel.coinData[i].position[0], SavingLevel.coinData[i].position[1], SavingLevel.coinData[i].position[2]);
            Instantiate(Coin, position, Quaternion.identity);
        }
        
    }

   public void CircleLevel(int StartingIndex, int LastIndex)
    {
        Vector3 position=new Vector3();
        for (int i = StartingIndex; i <= LastIndex; i++)
        {
            float x = SavingLevel.coinData[i].position[0], y = 0, z = 0;

            switch (SavingLevel.coinData[i].position[1])
            {
                case 0: y = 0; z = 0; break;
                case 15: y = -21f; z = 21f; break;
                case 30: y = 0f; z = 30f; break;
                case -15: y = -21f; z = -21f; break;
                case -30: y = 0f; z = -30f; break;
            }
              position = new Vector3(x, y, z);
            //Vector3 position = new Vector3(SavingLevel.coinData[i].position[0], SavingLevel.coinData[i].position[1], SavingLevel.coinData[i].position[2]);
            Instantiate(Coin, position, Quaternion.identity);
        }
        
    }
    #endregion

     IEnumerator LevelGenerator(int[,] _BatchElement)
    {


    
      int j = 0;
    int m = _DesignIndexArray.Length + 1;
    int K = 0; //Index to iterate through the array.
    int DesignIndex = 3; // We will increment the variable by 1 once every while loop . In the while loop , we will use modulus on it of 3.
        // If the remainder is 1 , we will go for a change in design layout. We have an array called _DesignIndex. We will iterate through it
        // and pass in which design layout to generate.
           
        while (_BatchSize>0)
        {
            DesignIndex++;
            if(DesignIndex%3==1)
            {
                _DesignIndex = _DesignIndexArray[j];
                
                j = (m) % _DesignIndexArray.Length; // This logic will loop us through the _designIndexArray again and again
                // The size of array is 3.The remainder is going to provide indexvalues as 0,1,2,0,1,2 and so on. 

                m++;

            }
                Debug.Log(_BatchSize);
                int startindex, lastindex;
                startindex = _BatchElement[K, 0];
                lastindex = _BatchElement[K, 1];
                ChangeLevel(_DesignIndex, startindex, lastindex);
                K++;
                _BatchSize--;
                yield return timer; //2f seconds

        }

        

    }

}
