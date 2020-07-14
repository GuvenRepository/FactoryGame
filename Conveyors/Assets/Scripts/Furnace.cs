using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Processor
{
    private void Start()
    {
        base.Start();
        inputItems.Add(0, 20);
        outputItem = 1;
        ID = 3;
    }
}
