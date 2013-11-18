using UnityEngine;
using System.Collections;

public class TechManager : MonoBehaviour {

    public GameObject PfbTech;

    public float minSpawnTime = 8;
    public float maxSpawnTime = 10;
    public float spawnForceSpeed = 5;
    public ForceMode spawnForceMode = ForceMode.Force;

    void Start()
    {
        StartCoroutine("DelaySpawnNewTech");
    }

    void SpawnTech()
    {
        // set up direction for the Tech to be thrown at when it spawns
        Vector3 spawnDir = (new Vector3(transform.position.x, transform.position.y, transform.position.z) - transform.position);
        Vector3 randDir = Random.onUnitSphere;
        randDir.y = 0;
        spawnDir -= randDir;

        Vector3 spawnAt = transform.position;
        spawnAt.y += 1.5f;

        GameObject Tech = (GameObject)Instantiate(PfbTech, spawnAt, transform.rotation);
        Tech.rigidbody.AddForce(spawnDir * spawnForceSpeed, spawnForceMode);
    }

    IEnumerator DelaySpawnNewTech()
    {
        while (true)
        {
            SpawnTech();
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
