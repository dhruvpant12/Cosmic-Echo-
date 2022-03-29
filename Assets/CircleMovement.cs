using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Class is a dummy class used to test Input controls . 
public class CircleMovement : MonoBehaviour
{
     
    
    public GameObject gb;
    Vector3 boundary;
    float speed = 35;
    bool left = false, right = false,center =true;
     

    private void Start()
    {
       /* StartCoroutine(Example());
        Debug.Log("check for exit");*/

    }
    void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftArrow))
            k++;*/
        /*#region AA
        boundary = transform.position;
        boundary.y = Mathf.Clamp(transform.position.y, -30f, 0f);
        boundary.z = Mathf.Clamp(transform.position.z, -30f, 30f);
        transform.position = boundary;

        if (transform.position.y <= -30)
            center = true;*/

        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(gb.transform.position, Vector3.right, -100 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(gb.transform.position, Vector3.right, 100 * Time.deltaTime);
        }
*/

        //  transform.Translate(Vector3.right);
        /*if (Direction)
            timeCounter += Time.deltaTime;
        else
            timeCounter -= Time.deltaTime;
        float x = Mathf.Cos(timeCounter * 10) *5;
        float y = Mathf.Sin(timeCounter* 10) * 5;
        float z = 0;
        transform.position = new Vector3(x, y, z);*/
       /* #endregion*/
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(gb.transform.position, Vector3.right, 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(gb.transform.position, Vector3.right, 100 * Time.deltaTime);
        }
            //Direction = false;
            /*  if (center)
              {
                  transform.Translate(0, speed * Time.deltaTime, speed * Time.deltaTime,Space.World);
                  center = false;
                  left = true;
                  right = false;
              }
              else if (left)
              {
                  transform.Translate(0, speed * Time.deltaTime, speed * Time.deltaTime,Space.World);

              }
              else if (right)
              {
                  transform.Translate(0, -speed * Time.deltaTime, speed * Time.deltaTime,Space.World);
              }

          }
          if (Input.GetKey(KeyCode.RightArrow))
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

              }*/

            //Direction = true;
            //  transform.RotateAround(gb.transform.position, Vector3.right, 100 * Time.deltaTime);

            //transform.Translate(0, speed * Time.deltaTime, -speed * Time.deltaTime);



        }

    }

   /* IEnumerator Example()
    {
        while (k < 10)
        { 
            yield return new WaitForSeconds(2f);
        Debug.Log(k);
        }
    }*/
