using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;


    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0; 
        Destroy(gameObject, gotHayDestroyDelay); 
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) // If the GameObject that hit this one has the Hay tag assigned and the sheep wasn't hit by hay already...

        {
            Destroy(other.gameObject); //Destroy the hay bale.
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }

    }

    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false; // Make the sheep's rigidbody non-kinematic so it gets affected by gravity.
        myCollider.isTrigger = false; // Disable the trigger so the sheep becomes a solid object
        Destroy(gameObject, dropDestroyDelay); // Destroy the sheep after the delay 
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
