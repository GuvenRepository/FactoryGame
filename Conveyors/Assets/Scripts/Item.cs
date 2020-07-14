using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Base
{
    public int onConveyor = -1;
    public float price = 0f;


    protected void Start()
    {
        StartCoroutine("freshnessCheck");
    }

    private IEnumerator freshnessCheck()
    {
        int freshness = 50;
        while (true)
        {
            if (onConveyor == -1)
                freshness--;
            else
                freshness = 50;

            if (freshness == 0)
            {
                StopCoroutine("freshnessCheck");
                Destroy(gameObject);
            }
                

            yield return new WaitForSeconds(0.25f);
        }
    }
}
