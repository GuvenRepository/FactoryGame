using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text moneyText;
    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + System.Math.Round(GameManager.money,2).ToString();
    }

    public void buyItem(int ID)
    {
        GetComponent<EditMode>().StartPlacing(PrefabManager.IDToGameObject(ID));
    }

    public void deleteMode()
    {
        GetComponent<EditMode>().StartDelete();
    }

    public void escape()
    {
        GetComponent<EditMode>().StopEdit();
        GetComponent<EditMode>().StopDelete();
    }
}
