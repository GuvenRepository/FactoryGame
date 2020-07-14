using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMode : MonoBehaviour
{

    public GameObject farmPrefab;
    private GameObject objectOnHand;
    public Grid gridMan;

    public static bool isEditing = false;
    private bool editModeEnable = false;
    private bool deleteModeEnable = false;
    

    void Update()
    {

        if (isEditing)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 truncatedPosition = mousePosition;
            truncatedPosition.x -= truncatedPosition.x % 2;
            truncatedPosition.y -= truncatedPosition.y % 2;
            truncatedPosition += Vector3.one;
            if (mousePosition.x < 0)
                truncatedPosition.x -= 2;
            if (mousePosition.y < 0)
                truncatedPosition.y -= 2;

            truncatedPosition.z = -1;

            if (editModeEnable)
            {

                objectOnHand.transform.position = truncatedPosition;

                if (Input.mouseScrollDelta.y > 0f)
                {
                    objectOnHand.transform.Rotate(0, 0, 90);
                }
                else if (Input.mouseScrollDelta.y < 0f)
                {
                    objectOnHand.transform.Rotate(0, 0, -90);
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    int[] index = PositionToIndexes(truncatedPosition);
                    if (index != null)
                    {
                        if (GameManager.world[index[0], index[1]] == null)
                        {
                            truncatedPosition.z = 0;
                            GameObject placedObject = Instantiate(objectOnHand, truncatedPosition, objectOnHand.transform.rotation);
                            GameManager.world[index[0], index[1]] = placedObject;
                            StopEdit();
                        }
                    }
                }
            }
            if (deleteModeEnable)
            {

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    int[] index = PositionToIndexes(truncatedPosition);
                    if (index != null)
                    {
                        if (GameManager.world[index[0], index[1]] != null)
                        {
                            Destroy(GameManager.world[index[0], index[1]]);
                            GameManager.world[index[0], index[1]] = null;
                        }
                    }

                }
            }
        }
        
    }

    private int[] PositionToIndexes(Vector2 truncatedPosition)
    {
        int[] indexes = new int[2];
        truncatedPosition += new Vector2(10, 10);
        truncatedPosition /= 2;

        if (truncatedPosition.x < 0 || truncatedPosition.x >= 10 ||
            truncatedPosition.y < 0 || truncatedPosition.y >= 10)
            return null;
        truncatedPosition.x = Mathf.Clamp(truncatedPosition.x, -10, 10);
        truncatedPosition.y = Mathf.Clamp(truncatedPosition.y, -10, 10);
        indexes[0] = (int)truncatedPosition.x;
        indexes[1] = (int)truncatedPosition.y;
        
        return indexes;
    }

    public void StartEdit()
    {
        isEditing = true;
        editModeEnable = true;
        gridMan.showGrid();
        objectOnHand = Instantiate(farmPrefab);
    }

    public void StopEdit()
    {
        Destroy(objectOnHand);
        gridMan.hideGrid();
        editModeEnable = false;
        isEditing = false;
    }

    public void StartPlacing(GameObject itemToPlace)
    {
        isEditing = true;
        editModeEnable = true;
        gridMan.showGrid();
        objectOnHand = Instantiate(itemToPlace);
    }

    public void StopPlacing()
    {

        Destroy(objectOnHand);
        gridMan.hideGrid();
        editModeEnable = false;
        isEditing = false;
    }

    public void StartDelete()
    {
        isEditing = true;
        deleteModeEnable = true;
        gridMan.showGrid();
    }

    public void StopDelete()
    {
        gridMan.hideGrid();
        deleteModeEnable = false;
        isEditing = false;
    }


}
