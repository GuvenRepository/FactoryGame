using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : Base
{
    protected float spawnTime = 1f;
    protected int spawnItem = -1;

    protected void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (!EditMode.isEditing && spawnItem != -1)
            {
                Instantiate(PrefabManager.IDToGameObject(spawnItem), transform.position + 2*Vector3.down + Vector3.back, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
