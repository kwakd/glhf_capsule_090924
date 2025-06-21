using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;


public class saveSystem_script : MonoBehaviour
{
    [SerializeField] testA_script testCharA_prefab;

    public static List<testA_script> testCharAlist = new List<testA_script>();

    const string TESTCHARA_SUB = "/testCharA";
    const string TESTCHARA_COUNT_SUB = "/testCharA.count";

    void Awake()
    {
        LoadTestCharA();
    }

    void OnApplicationQuit()
    {
        SaveTestCharA();
    }

    void SaveTestCharA()
    {
        Debug.Log("SaveTestCharA hit");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + TESTCHARA_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + TESTCHARA_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, testCharAlist.Count);
        countStream.Close();

        for(int i=0; i<testCharAlist.Count; i++)
        {
            FileStream streamTestCharA = new FileStream(path + i, FileMode.Create);
            testCharData dataCharA = new testCharData(testCharAlist[i]);

            formatter.Serialize(streamTestCharA, dataCharA);
            streamTestCharA.Close();
        }
    }

    void LoadTestCharA()
    {
        Debug.Log("LoadTestCharA hit");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + TESTCHARA_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + TESTCHARA_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int testCharACount = 0;

        if(File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            testCharACount = (int)formatter.Deserialize(countStream);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Path not found in " + countPath);
        }

        for(int i=0; i<testCharAlist.Count; i++)
        {
            if(File.Exists(path + i))
            {
                FileStream streamTestCharA = new FileStream(path + i, FileMode.Open);
                testCharData testCharAData = formatter.Deserialize(streamTestCharA) as testCharData;

                streamTestCharA.Close();

                int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
                int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

                Vector3 position = new Vector3(testSpawnLocationX, testSpawnLocationY, 0);

                testA_script testAcharAInst = Instantiate(testCharA_prefab, position, Quaternion.identity);

                testAcharAInst.charName = testCharAData.charName;
                testAcharAInst.charPassiveMoney = testCharAData.charPassiveMoney;
                testAcharAInst.passiveMoneyTimeCount = testCharAData.passiveMoneyTimeCount;

            }
            else
            {
                Debug.LogError("Path not found in " + path + i);
            }
        }
    }
}
