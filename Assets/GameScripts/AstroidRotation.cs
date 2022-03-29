using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is attached to the astroid prefab. It is used to rotate the asteroids on the Y axis.
public class AstroidRotation : MonoBehaviour
{
    float x;
    float y;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(50f, 100f);
        y = Random.Range(50f, 100f);
        z = Random.Range(50f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
    }
}
