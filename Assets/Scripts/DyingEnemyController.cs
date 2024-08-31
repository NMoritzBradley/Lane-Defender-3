using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
