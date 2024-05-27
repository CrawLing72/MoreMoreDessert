using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem
{
    public int itemID, TileID, Bills, Price, StarDistricted;
    public string Name, Description;
    public float StarPoints, ConsumedTime;

    public void SettingObject(Dictionary<string, object> Data)
    {
        itemID = int.Parse(Data["ItemID"].ToString());
        TileID = int.Parse(Data["TileID"].ToString());
        Bills = int.Parse(Data["Bills"].ToString());
        Price = int.Parse(Data["Price"].ToString());
        StarDistricted = int.Parse(Data["StarDistricted"].ToString());
        Name = Data["Name"].ToString();
        Description = Data["Description"].ToString();
        StarPoints = float.Parse(Data["StarPoints"].ToString());
        ConsumedTime = float.Parse(Data["ConsumedTime"].ToString());
    }
}

public class NormalItem
{
    public int itemID, TileID, HP, MP, StarDistricted, Price;
    public string Name, Description;

    public void SettingObject(Dictionary<string, object> Data)
    {
        itemID = int.Parse(Data["ItemID"].ToString());
        TileID = int.Parse(Data["TileID"].ToString());
        HP = int.Parse(Data["HP"].ToString());
        MP = int.Parse(Data["MP"].ToString());
        StarDistricted = int.Parse(Data["StarDistricted"].ToString());
        Name = Data["Name"].ToString();
        Description = Data["Description"].ToString();
        Price = int.Parse(Data["Price"].ToString());
    }

}

public class CyberSABO
{
    public int SABOID, Price, AVGTyping, StarDistricted;
    public float StarPoints;
    public string Name, TypedWords, Description;

    public void SettingObject(Dictionary<string, object> Data)
    {
        SABOID = int.Parse(Data["SABOID"].ToString());
        Price = int.Parse(Data["Price"].ToString());
        AVGTyping = int.Parse(Data["AVGTyping"].ToString());
        StarDistricted = int.Parse(Data["StarDistricted"].ToString());
        StarPoints = float.Parse(Data["StarPoints"].ToString());
        Name = Data["Name"].ToString();
        TypedWords = Data["TypedWords"].ToString();
        Description = Data["Description"].ToString();
    }

}

public class ItemManager : MonoBehaviour
{
    // Objects
    public static ItemManager instance;

    [Header("FileSettings")]
    public string FoodFileName;
    public string ItempFileName;
    public string SABOFileName;


    // Datas;
    public List<Dictionary<string, object>> FoodInfoData;
    public Dictionary<int, FoodItem> FoodInfos = new Dictionary<int, FoodItem>();

    public List<Dictionary<string, object>> ItemInfoData;
    public Dictionary<int, NormalItem> ItemInfos = new Dictionary<int, NormalItem>();

    public List<Dictionary<string, object>> SABOData;
    public Dictionary<int, CyberSABO> SABOInfos = new Dictionary<int, CyberSABO>();

    public Sprite[] FoodSprites;

    //GM의 CurrentGoods, CurrentItems : id별 수량 체크, FoodInfos 및 ItemInfos : id별 instance specify;




    // Start is called before the first frame update
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

        // Info Data Loading
        FoodInfoData = CSVReader.Read(FoodFileName);
        ItemInfoData = CSVReader.Read(ItempFileName);
        SABOData = CSVReader.Read(SABOFileName);

        for (int i=0; i<FoodInfoData.Count; i++)
        {
            FoodItem temp_item = new FoodItem();
            temp_item.SettingObject(FoodInfoData[i]);
            FoodInfos.Add(temp_item.itemID, temp_item);
        }

        for (int i = 0; i < ItemInfoData.Count; i++)
        {
            NormalItem temp_item = new NormalItem();
            temp_item.SettingObject(ItemInfoData[i]);
            ItemInfos.Add(temp_item.itemID, temp_item);
        }

        for (int i = 0; i < SABOData.Count; i++)
        {
            CyberSABO temp_sabo = new CyberSABO();
            temp_sabo.SettingObject(SABOData[i]);
            SABOInfos.Add(temp_sabo.SABOID, temp_sabo);
        }



        if (DataManager.isNewGame) // 만약 새 게임이라면
        {
            GameManager.currentGoods = new Dictionary<int, int>(); // 현재 상품, 아이템 목록 초기화
            List<int> temp_keys = new List<int>(FoodInfos.Keys);
            for (int i = 0; i < FoodInfoData.Count; i++)
            {
                GameManager.currentGoods.Add(temp_keys[i], 0);
            }


            GameManager.currentItems = new Dictionary<int, int>();
            List<int> temp_keys2 = new List<int>(ItemInfos.Keys);
            for (int i = 0; i < ItemInfoData.Count; i++)
            {
                GameManager.currentItems.Add(temp_keys2[i], 1);
            }
        }
        else // 만약 기존 게임이라면
        {
            GameManager.currentGoods = DataManager.instance.nowPlayer.currentGoods;
            GameManager.currentItems = DataManager.instance.nowPlayer.currentItems;// 나중에 Gamemanger에서 awkae때 데이터 로딩 하게 할것                                                                  
        }

        // Food Sprite Importing
        FoodSprites = Resources.LoadAll<Sprite>("Tiles/FoodItems");

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
