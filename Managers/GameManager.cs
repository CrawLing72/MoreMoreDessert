using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    // Objects
    public static GameManager instance;
    [Header("ENV Settings")]
    public int MP_MAX = 150;
    public int HP_MAX = 100;
    public int init_MP;
    public int init_HP;

    // Rainyk Movements;
    public static float Player_X;
    public static float Player_Y;
    public static bool isRainykMove;
    public static bool isFrozen = false;

    // ENV Vars;
    public static bool isSideView = true;
    public static int QuestIndex = 0;

    // Game Contetns Static Vars -> Saving Target!
    public static int current_money = 4000;
    public static int hp;
    public static int mp;
    public static float stars = 0.1f;
    
    public static Dictionary<int, int> currentItems;
    public static Dictionary<int, int> currentGoods;

    private void Awake()
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

        hp = init_HP;
        mp = init_MP;
    }

    public PlayerData getPlayerData()
    {
        PlayerData temp_data = new PlayerData();

        temp_data.current_money = current_money;
        temp_data.hp = hp;
        temp_data.mp = mp;
        temp_data.stars = stars;
        temp_data.currentItems = currentItems;
        temp_data.currentGoods = currentGoods;

        DateTime currentDate = DateTime.Now;
        temp_data.lastly_saved_date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

        return temp_data;
    }

    public void setPlayerData()
    {
        DataManager.instance.LoadData();
        PlayerData inputed_data = DataManager.instance.nowPlayer;

        current_money = inputed_data.current_money;
        hp = inputed_data.hp;
        mp = inputed_data.mp;
        stars = inputed_data.stars;
        currentItems = inputed_data.currentItems;
        currentGoods = inputed_data.currentGoods;
    }



    /* Unity Broadcast Functions */

    // Start is called before the first frame update
    void Start()
    {
        if (!DataManager.isNewGame)
        {
            setPlayerData();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
