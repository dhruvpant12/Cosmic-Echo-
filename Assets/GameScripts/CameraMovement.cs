using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	//public float smoothspeed = 15f;
	public Vector3 offset1;//Camera offset for straight level .
	public Vector3 offset2;  // Camera offset for Circle and Step level.
	bool CircleLevel,StepLevel; //Boolean checks for level design transition.

	
    private void Start()
    {
		offset1 = new Vector3(0, 0, -80); //Camera offset for straight level .
		offset2 = new Vector3(-50, 14, -2.5f); // Camera offset for Circle and Step level.
	}
    void LateUpdate()
	{
		if (LevelDesigner._DesignIndex == 1)
		{
			//0 0 -80
			//0 0 0
			if (transform.rotation.y != 0.0f)
			{
				transform.rotation = Quaternion.identity;
				//Debug.Log(transform.rotation);	
				target.position = new Vector3(target.transform.position.x, 0, 0);
				target.rotation = Quaternion.Euler(0, 180, 0);
				Debug.Log("Straight");
			}
			Vector3 targetvalues = new Vector3(target.position.x, 0, 0);
			Vector3 desiredposition = targetvalues + offset1;
			//Vector3 smoothposition = Vector3.Lerp (transform.position, desiredposition, smoothspeed  );
			transform.position = desiredposition;
			
		}
		else if(LevelDesigner._DesignIndex == 2) //Circle
		{//-105 15 0
		 // 0 90 0
			
			if (transform.rotation.eulerAngles.y != 90f ||CircleLevel )
			{
				StepLevel = true;
				CircleLevel = false;
				transform.rotation = Quaternion.Euler(0, 90.0f, 0);
				 					
				target.position= new Vector3(target.transform.position.x, 0, 0);
				target.rotation = Quaternion.Euler(0, 180, 0);

				Debug.Log("cirlce");
			}


			Vector3 targetvalues = new Vector3(target.position.x, 0, 0);
			Vector3 desiredposition = targetvalues + offset2;
			//Vector3 smoothposition = Vector3.Lerp (transform.position, desiredposition, smoothspeed  );
			transform.position = desiredposition;
			

		}
		else if(LevelDesigner._DesignIndex == 3)//CharacterControls.DesignIndex==3
		{
			
			if (transform.rotation.eulerAngles.y != 90f  ||StepLevel)
			{
				CircleLevel = true;
				StepLevel = false;
				transform.rotation = Quaternion.Euler(0, 90.0f, 0);
				 
				target.position = new Vector3(target.transform.position.x, 0, 0);
				target.rotation = Quaternion.Euler(0, 180, 0);
				Debug.Log("steps");

			}


			Vector3 targetvalues = new Vector3(target.position.x, 0, 0);
			Vector3 desiredposition = targetvalues + offset2;
			//Vector3 smoothposition = Vector3.Lerp (transform.position, desiredposition, smoothspeed  );
			transform.position = desiredposition;
		}
		 

	}
}
