using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    Vector2 gridLimit1 = new Vector2(-10, -10);
    Vector2 gridLimit2 = new Vector2(10, 10);
    Vector2 start, end;
    public Material mat;
    float size = 0.05f;
    float xSpace = 2f;

    GameObject root;

    private void Start()
    {
        root = new GameObject();
        root.transform.SetParent(transform);
        root.name = "Grid";
        createGrid();
        hideGrid();
    }

    void createGrid()
    {
        //Create X Lines
        start = new Vector2(gridLimit1.x, gridLimit1.y);
        end = new Vector2(gridLimit1.x, gridLimit2.y);

        for (int i = 0; start.x <= gridLimit2.x; i++)
        {
            GameObject myLine = new GameObject();
            myLine.transform.SetParent(root.transform);
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = size;
            lr.endWidth = size;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            start += new Vector2(xSpace, 0);
            end += new Vector2(xSpace, 0);
        }
        //Create Y Lines
        start = new Vector2(gridLimit1.x, gridLimit1.y);
        end = new Vector2(gridLimit2.x, gridLimit1.y);

        for (int i = 0; start.y <= gridLimit2.y; i++)
        {
            GameObject myLine = new GameObject();
            myLine.transform.SetParent(root.transform);
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = size;
            lr.endWidth = size;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            start += new Vector2(0, xSpace);
            end += new Vector2(0, xSpace);
        }

    }

    public void showGrid()
    {
        root.SetActive(true);
    }

    public void hideGrid()
    {
        root.SetActive(false);
    }
}