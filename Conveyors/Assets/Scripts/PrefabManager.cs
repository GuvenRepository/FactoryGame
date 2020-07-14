using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; set; }
    public GameObject wheat;
    public GameObject flour;
    public GameObject farm;
    public GameObject furnace;
    public GameObject conveyor;
    public GameObject sale;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    public static GameObject IDToGameObject(int ID)
    {
        switch (ID)
        {
            case 0:
                return Instance.wheat;
            case 1:
                return Instance.flour;
            case 2:
                return Instance.farm;
            case 3:
                return Instance.furnace;
            case 42:
                return Instance.conveyor;
            case 5:
                return Instance.sale;
            default:
                Debug.Log("No item ID:" + ID.ToString());
                return null;
        }
    }

}
