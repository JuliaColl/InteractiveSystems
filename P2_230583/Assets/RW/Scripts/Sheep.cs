using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;
    private bool isDestroyed;


    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;

    public float heartOffset; 
    public GameObject heartPrefab; 


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

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);

        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; // Add a TweenScale component
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay; // TweenScale will take to the same time it takes to destroy the sheep

        SoundManager.Instance.PlaySheepHitClip(); // Play sound

        GameStateManager.Instance.SavedSheep();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) // If the GameObject that hit this one has the Hay tag assigned and the sheep wasn't hit by hay already...

        {
            Destroy(other.gameObject); //Destroy the hay bale.
            HitByHay();
        }
        else if (other.CompareTag("DropSheep") && !isDestroyed)
        {
            Drop();
        }

    }

    private void Drop()
    {
        // Debug.Log("drop");
        GameStateManager.Instance.DroppedSheep();

        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false; // Make the sheep's rigidbody non-kinematic so it gets affected by gravity.
        myCollider.isTrigger = false; // Disable the trigger so the sheep becomes a solid object
        Destroy(gameObject, dropDestroyDelay); // Destroy the sheep after the delay
        isDestroyed = true;

        SoundManager.Instance.PlaySheepDroppedClip(); // Play sound

    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
