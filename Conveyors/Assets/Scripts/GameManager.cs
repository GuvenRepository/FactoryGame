using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject conveyorPrefab;
    public GameObject salePrefab;
    
    public static float money = 0f;
    public static GameObject[,] world=new GameObject[10,10];

    private void Start()
    {
        StartCoroutine("SaveAuto");
        StartCoroutine("LoadGame");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<EditMode>().StopEdit();
            GetComponent<EditMode>().StopDelete();
        }

        if (EditMode.isEditing)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<EditMode>().StartEdit();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<EditMode>().StartDelete();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<EditMode>().StartPlacing(PrefabManager.IDToGameObject(3));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<EditMode>().StartPlacing(salePrefab);
        }
    }

    IEnumerator SaveAuto()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SaveManager.saveData();
        }
    }

    IEnumerator LoadGame()
    {
        while (PrefabManager.Instance == null)
        {
            Debug.Log("Loading");
            yield return new WaitForSeconds(0.1f);
        }
        SaveManager.loadData();
        Debug.Log("Load Complete");
    }
}
