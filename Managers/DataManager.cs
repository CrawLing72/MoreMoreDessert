using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class PlayerData
{
    public int current_money = 4000;
    public int hp;
    public int mp;
    public float stars = 0.1f;

    public Dictionary<int, int> currentItems;
    public Dictionary<int, int> currentGoods;

    // Data Management Related.
    public string lastly_saved_date;
}

public class DataManager : MonoBehaviour
{
    // Objects
    public static DataManager instance;

    // Class Variables
    public static bool isNewGame = true; // Gamemanager Awake때 데이터 불러오기 유무 구분용!
    public static int now_slot = -1;

    // Data Related Variables
    public string path, ScriptPath;

    // Player Data Class
  

    // Data Class Instances
    public PlayerData nowPlayer = new PlayerData();

    void Awake()
    {
        // SingleTone Pattern Implemention
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // Getting File Path
        path = Application.persistentDataPath + "/save";
    }

    public void SaveData()
    {
        string data = JsonConvert.SerializeObject(nowPlayer);
        File.WriteAllText(path+now_slot.ToString(), data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + now_slot.ToString());
        nowPlayer = JsonConvert.DeserializeObject<PlayerData>(data);
    }
    public void DataClear()
    {
        now_slot = -1;
        nowPlayer = new PlayerData();
    }

    void Start()
    {
        if (isNewGame)
        {

        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
