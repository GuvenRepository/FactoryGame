using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : Base
{
    protected Dictionary<int, int> itemBank = new Dictionary<int, int>();
    protected Dictionary<int, int> inputItems = new Dictionary<int, int>();
    protected int outputItem;


    protected void Start()
    {
        StartCoroutine("Produce");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "item" && !EditMode.isEditing)
        {
            int key = collision.GetComponent<Item>().ID;
            if (key != outputItem)
            {
                if (itemBank.ContainsKey(key))
                    itemBank[key]++;
                else
                    itemBank.Add(key, 1);

                StartCoroutine("getSmallerAndDestroy", collision.transform);
            }
        }
    }

    IEnumerator getSmallerAndDestroy(Transform obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (obj.localScale.x < 0.03f)
            {
                Destroy(obj.gameObject);
                break;
            }
            obj.localScale -= obj.localScale * 0.1f;
        }
        yield return null;
    }

    IEnumerator Produce()
    {
        bool enoughItems;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!EditMode.isEditing)
            {
                enoughItems = true;
                // Control of items are sufficient to produce
                foreach (int item in inputItems.Keys)
                {
                    //if bank doesn't have item or not enough
                    if (!itemBank.ContainsKey(item) || itemBank[item] < inputItems[item])
                        enoughItems = false;
                }

                if (enoughItems)
                {
                    //Remove used Items
                    foreach (int item in inputItems.Keys)
                    {
                        itemBank[item] -= inputItems[item];
                    }

                    Instantiate(PrefabManager.IDToGameObject(outputItem), transform.position + 2*Vector3.down + Vector3.back, Quaternion.identity);
                }
            }
        }
    }
}
