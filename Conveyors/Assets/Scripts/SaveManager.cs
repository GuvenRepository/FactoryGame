using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private static SaveWorld saveWorld;    // the Dictionary used to save and load data to/from disk
    protected static string savePath;
    /**
     * Saves the save data to the disk
     */
    public static void saveData()
    {
        savePath = Application.persistentDataPath + "/save.dat";
        saveWorld = new SaveWorld();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(savePath, FileMode.Create);
        bf.Serialize(file, saveWorld);
        file.Close();
    }

    /**
     * Loads the save data from the disk
     */
    public static void loadData()
    {
        savePath = Application.persistentDataPath + "/save.dat";
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(savePath, FileMode.Open);
            saveWorld = (SaveWorld)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Debug.Log(saveWorld.rotations[i, j]);
                    if (saveWorld.IDs[i, j] != -1)
                    {
                        GameManager.world[i, j] = Instantiate(PrefabManager.IDToGameObject(saveWorld.IDs[i, j]),
                                                             new Vector3(saveWorld.positions[i, j, 0], saveWorld.positions[i, j, 1], 0f), 
                                                             Quaternion.Euler(0f,0f, saveWorld.rotations[i, j])) as GameObject;
                    }
                }
            }
        }
    }


    [System.Serializable]
    public class SaveWorld
    {
        public int[,] IDs = new int[10, 10];
        public float[,,] positions = new float[10, 10, 2];
        public float[,] rotations = new float[10, 10];

        public SaveWorld()
        {
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject obj2save = GameManager.world[i, j];
                    if (obj2save != null)
                    {
                        //Debug.Log(i.ToString() + "," + j.ToString());
                        IDs[i, j] = obj2save.GetComponent<Base>().ID;
                        positions[i, j, 0] = obj2save.transform.position.x;
                        positions[i, j, 1] = obj2save.transform.position.y;
                        rotations[i, j] = obj2save.transform.eulerAngles.z;
                    }
                    else
                    {
                        IDs[i, j] = -1;
                    }
                }
            }
        }
    }
}
