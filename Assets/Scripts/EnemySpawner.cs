using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Snake;
    public GameObject Slime;
    public GameObject Snail;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;
    public GameObject SpawnPoint5;
    private int enemyType;
    private int spawnLocation;
    
    /// <summary>
    /// Starts Spawning
    /// </summary>
    void Start()
    {
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// After 2 seconds, randomly determines an enemy to spawn, then calls the enemy spawn function, then calls
    /// itself to start the coroutine again
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        enemyType = Random.Range(1, 4);
        if(enemyType == 1)
        {
            Invoke("CreateSnake", .1f);
        }
        else if (enemyType == 2)
        {
            Invoke("CreateSlime", .1f);
        }
        else if (enemyType == 3)
        {
            Invoke("CreateSnail", .1f);
        }
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// Randomly chooses a SpawnPoint, then spawns a Snake there
    /// </summary>
    void CreateSnake()
    {
        spawnLocation = Random.Range(1, 6);

        if (spawnLocation == 1)
        {
            transform.position = SpawnPoint1.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 2)
        {
            transform.position = SpawnPoint2.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 3)
        {
            transform.position = SpawnPoint3.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 4)
        {
            transform.position = SpawnPoint4.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 5)
        {
            transform.position = SpawnPoint5.GetComponent<Rigidbody2D>().position;
        }

        Instantiate(Snake, (transform.position), Quaternion.identity);
    }

    /// <summary>
    /// Randomly chooses a SpawnPoint, then spawns a Slime there
    /// </summary>
    void CreateSlime()
    {
        spawnLocation = Random.Range(1, 6);

        if (spawnLocation == 1)
        {
            transform.position = SpawnPoint1.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 2)
        {
            transform.position = SpawnPoint2.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 3)
        {
            transform.position = SpawnPoint3.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 4)
        {
            transform.position = SpawnPoint4.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 5)
        {
            transform.position = SpawnPoint5.GetComponent<Rigidbody2D>().position;
        }

        Instantiate(Slime, (transform.position), Quaternion.identity);
    }

    /// <summary>
    /// Randomly chooses a SpawnPoint, then spawns a Snail there
    /// </summary>
    void CreateSnail()
    {
        spawnLocation = Random.Range(1, 6);

        if (spawnLocation == 1)
        {
            transform.position = SpawnPoint1.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 2)
        {
            transform.position = SpawnPoint2.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 3)
        {
            transform.position = SpawnPoint3.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 4)
        {
            transform.position = SpawnPoint4.GetComponent<Rigidbody2D>().position;
        }
        else if (spawnLocation == 5)
        {
            transform.position = SpawnPoint5.GetComponent<Rigidbody2D>().position;
        }

        Instantiate(Snail, (transform.position), Quaternion.identity);
    }
}
