
using UnityEngine;

//We are using this Class to store serializable data. We are taking the coordinate values of the coins in the scene and saving them 
//in file using Binary Formatter.
[System.Serializable]
public class CoinData 
{
    public float[] position;

   public CoinData(float x,float y,float z)
    {
        position = new float[]
        {
            x,y,z
        };
    }
    
}
