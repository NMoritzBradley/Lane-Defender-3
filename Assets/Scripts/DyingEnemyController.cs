using System.Collections;
using UnityEngine;

public class DyingEnemyController : MonoBehaviour
{
    /// <summary>
    /// Starts the Die coroutine
    /// </summary>
    void Start()
    {
        StartCoroutine(Die());
    }

    /// <summary>
    /// Destroys the DyingEnemy after .3 seconds
    /// </summary>
    /// <returns></returns>
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
