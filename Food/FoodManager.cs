using UnityEngine;
using System.Collections;

public class FoodManager : MonoBehaviour {

    public GameObject PfbFood;
    
    public float minSpawnTime = 8;
    public float maxSpawnTime = 10;
    public float spawnForceSpeed = 5;
    public ForceMode spawnForceMode = ForceMode.Force;

    void Start()
    {
        StartCoroutine("DelaySpawnNewFood");
    }

    void SpawnFood()
    {
        // set up direction for the food to be thrown at when it spawns
        Vector3 spawnDir = (new Vector3(transform.position.x, transform.position.y + 50, transform.position.z) - transform.position);
        Vector3 randDir = Random.onUnitSphere;
        randDir.y = 0;
        spawnDir -= randDir;

        Vector3 spawnAt = transform.position;
        spawnAt.y += 1.5f;

        GameObject Food = (GameObject)Instantiate(PfbFood, spawnAt, transform.rotation);
        Food.rigidbody.AddForce(spawnDir * spawnForceSpeed, spawnForceMode);
    }

    IEnumerator DelaySpawnNewFood()
    {
        while (true)
        {
            SpawnFood();
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
