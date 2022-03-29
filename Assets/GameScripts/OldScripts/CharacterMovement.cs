using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Score score;
    public Rigidbody rb;
    public Vector3 force;
    public Vector3 velocity; //Remember to disable this.
    Vector3 boundry;
    // Start is called before the first frame update
    void Start()
    {
        score = gameObject.AddComponent<Score>();

    }

    // Update is called once per frame
    void Update()
    {

        /*boundry = transform.position;
        boundry.x = Mathf.Clamp(transform.position.x, -40f, 40f);
        boundry.z = Mathf.Clamp(transform.position.z, -40f, 40f);
        boundry.y = Mathf.Clamp(transform.position.y, 0f, 3f);
        transform.position = boundry;*/
    }

    private void FixedUpdate()
    {
        // rb.velocity=force; //rb is kinematic . cannot use forces.         
        rb.MovePosition(rb.position + force * Time.deltaTime); //Since rb is kinematic , we are using Moveposition.
         velocity = rb.velocity;//Remember to disable this.

       
        
        //User input for character movement.
        #region
        
        #endregion

    }


   /* private void OnCollisionEnter(Collision collision) //Would have used oncollisionenter if we wanted the player to experience physics on collision with items.
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            score.score++;
        }
    }*/



    void OnTriggerEnter(Collider other) // Using OntriggerEnter because we dont want any physics to take place when player collides with items.We just require trigger events.
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);

            

        }

        if (other.gameObject.CompareTag("Asteriods"))
        {
            Destroy(other.gameObject);

            

        }

    }



}
