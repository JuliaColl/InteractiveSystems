using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; // As long as this stays true, the script will keep spawning sheep.
    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>(); // Positions from where the sheep will be spawned. 
    public float timeBetweenSpawns;
    private List<GameObject> sheepList = new List<GameObject>(); // A list of all the sheep alive in the scene.

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; // Random spawn point available
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); // Create a new sheep and add it to the scene
        sheepList.Add(sheep); // Add a reference to the sheep to the list of sheeps.
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    private IEnumerator SpawnRoutine() 
    {
        while (canSpawn) 
        {
            SpawnSheep(); // Spawn a new sheep.
            yield return new WaitForSeconds(timeBetweenSpawns); // Pause the execution of this coroutine for a specified amount of seconds 
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) 
        {
            Destroy(sheep); 
        }

        sheepList.Clear();
    }

}
