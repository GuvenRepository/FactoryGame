using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sale : Base
{
    private void Start()
    {
        ID = 5;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "item" && !EditMode.isEditing)
        {
            GameManager.money += collision.GetComponent<Item>().price;
            StartCoroutine("getSmallerAndDestroy", collision.transform);
        }
    }

    IEnumerator getSmallerAndDestroy(Transform obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            obj.localScale -= obj.localScale * 0.1f;
            if (obj.localScale.x < 0.07f)
            {
                Destroy(obj.gameObject);
                break;
            }
        }
        yield return null;
    }
}
