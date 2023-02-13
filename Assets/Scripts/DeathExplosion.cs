using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("DestroyObject");
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
