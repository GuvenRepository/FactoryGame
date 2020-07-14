using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Base
{
    private int ConveyorID;
    private static int conveyorCounter = 0;

    Conveyor()
    {
        ConveyorID = conveyorCounter;
        conveyorCounter++;
    }

    private void Start()
    {
        ID = 42;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (EditMode.isEditing)
            return;
        if (collision.tag == "item")
        {
            if(collision.GetComponent<Item>().onConveyor == ConveyorID)
                collision.transform.Translate(-0.05f * transform.up);
            else if(collision.GetComponent<Item>().onConveyor == -1)
                collision.GetComponent<Item>().onConveyor = ConveyorID;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "item")
        {
            collision.GetComponent<Item>().onConveyor = -1;
        }
    }
}
