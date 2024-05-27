using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class IGMenuManager : MonoBehaviour
{
    public static IGMenuManager instance;

    [Header ("UI 요소")]
    public GameObject InterObject;
    public GameObject Buttons;
    public GameObject InventoryScreen;
    public GameObject ItemSellingScreen;

    [Header ("Save Objects")]
    public TextMeshProUGUI[] SaveTextlist;
    public GameObject[] LoadButtonsOnUIs;
    public GameObject LoadingButtons;

    [Header("Setting Objects")]
    public GameObject SettingButtons;

    [Header ("Inventory Related Objects")]
    public GameObject ItemBoxPrefab;
    public GameObject ItemContent;
    public TMP_Text AlertText;
    public TMP_Text RatingText;

    public GameObject HPSlider;
    public GameObject MPSlider;
    public GameObject CHSlider;

    [Header("ItemSelling Related Objects")]
    public GameObject SellBoxPrefab;
    public GameObject SellBoxContent;
    public GameObject SellingItemContent;
    public TMP_Text GobblinText;


    [Header("Update needed resources")]
    public TextMeshProUGUI Money;
    public TextMeshProUGUI StatusText;

    private bool wasIBOn = true;
    private bool wasInv1stOpened = true;
    private bool wasSelling1stOpened = true;

    // SAVE
    public void SettingLoadButtons()
    {
        AlertText.text = "";
        AlertText.color = Color.white;

        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// 데이터가 있는 경우
            {
                DataManager.now_slot = i;	// 선택한 슬롯 번호 저장
                DataManager.instance.LoadData();
                SaveTextlist[i].text = DataManager.instance.nowPlayer.lastly_saved_date + "\n" + DataManager.instance.nowPlayer.current_money + "원 모음";
                LoadButtonsOnUIs[i].SetActive(true);
            }
            else	// 데이터가 없는 경우
            {
                SaveTextlist[i].text = "파일이 존재하지 않습니다.";
                LoadButtonsOnUIs[i].SetActive(false);
                Debug.Log("Implemented!");
            }
        }
        Debug.Log("Implemented!");
        DataManager.instance.DataClear();
        GameManager.isFrozen = true;

        if (InterObject.activeSelf == true)
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        LoadingButtons.SetActive(true);
        GameSoundManager.instance.onClick();
    }

    public void OutSaveScreen()
    {
        GameManager.isFrozen = false;
        if (wasIBOn) InterObject.SetActive(true);
        Buttons.SetActive(true);
        LoadingButtons.SetActive(false);
        GameSoundManager.instance.onClick();
    }

    public void SaveData(int slot_num)
    {
        DataManager.now_slot = slot_num;
        DataManager.instance.nowPlayer = GameManager.instance.getPlayerData();
        DataManager.instance.SaveData();
        SettingLoadButtons();
        GameSoundManager.instance.onClick();
    }

    public void ReloadingGame(int slot_num)
    {
        MenuManager.instance.loadGame(slot_num);
        GameManager.instance.setPlayerData();
        GameSoundManager.instance.onClick();
    }

    // SETTING

    public void LoadSettingScreen()
    {
        if (InterObject.activeSelf == true)
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        GameManager.isFrozen = true;


        Buttons.SetActive(false);
        SettingButtons.SetActive(true);
        GameSoundManager.instance.onClick();
    }

    public void OutSettingScreen()
    {
        GameManager.isFrozen = false;
        if (wasIBOn) InterObject.SetActive(true);

        Buttons.SetActive(true);
        SettingButtons.SetActive(false);
        GameSoundManager.instance.onClick();
    }

    //INV

    public void LoadInventoryScreen()
    {

        if (InterObject.activeSelf == true)
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        InventoryScreen.SetActive(true);
        GameManager.isFrozen = true;

        if (wasInv1stOpened)
        {
            SettingInventoryScreen();
            wasInv1stOpened = false;
        }

        SettingInventoryScreen();
        GameSoundManager.instance.onClick();
    }

    public void OutInventoryScreen()
    {
        if (wasIBOn) InterObject.SetActive(true);
        Buttons.SetActive(true);
        InventoryScreen.SetActive(false);
        GameManager.isFrozen = false;
        GameSoundManager.instance.onClick();
    }

    public void SettingInventoryScreen()
    {
        if (wasInv1stOpened)
        {
            for (int i = 0; i < ItemManager.instance.ItemInfoData.Count; i++)
            {
                Instantiate(ItemBoxPrefab, new Vector2(0, 0), Quaternion.identity, ItemContent.transform);
            }
        }
        
        for (int i = 0; i < ItemManager.instance.ItemInfos.Count; i++)
        {
            Transform ItemBoxTrans = ItemContent.transform.GetChild(i);

            Transform imageTrans = ItemBoxTrans.transform.GetChild(0);
            Transform ITNameTrans = ItemBoxTrans.transform.GetChild(1);
            Transform ITContentTrans = ItemBoxTrans.transform.GetChild(2);
            Transform ButtonTrans = ItemBoxTrans.transform.GetChild(3);

            Image ItemImg = imageTrans.GetComponent<Image>();
            TMP_Text ItemName = ITNameTrans.GetComponent<TMP_Text>();
            TMP_Text ItemText = ITContentTrans.GetComponent<TMP_Text>();
            Button UseButton = ButtonTrans.GetComponent<Button>();
            TMP_Text ButtonText = ButtonTrans.transform.GetChild(0).GetComponent<TMP_Text>();
            InvenButtonController buttonController = ButtonTrans.GetComponent<InvenButtonController>();

            // 0. Get Item Object
            NormalItem TargetItem = ItemManager.instance.ItemInfos[i];

            // 1. Setting Unders
            ItemImg.sprite = ItemManager.instance.FoodSprites[TargetItem.TileID];
            ItemName.text = TargetItem.Name;
            ItemText.text = TargetItem.Description;
            buttonController.InvButtonSettings(i);

            // 2. Setting Sldiers
            Slider HPS = HPSlider.transform.GetComponent<Slider>();
            Slider MPS = MPSlider.transform.GetComponent<Slider>();
            Slider CHS = CHSlider.transform.GetComponent<Slider>();

            TMP_Text HPText = HPSlider.transform.GetChild(0).GetComponent<TMP_Text>();
            TMP_Text MPText = MPSlider.transform.GetChild(0).GetComponent<TMP_Text>();
            TMP_Text CHText = CHSlider.transform.GetChild(0).GetComponent<TMP_Text>();

            HPS.maxValue = GameManager.instance.HP_MAX;
            MPS.maxValue = GameManager.instance.MP_MAX;
            CHS.maxValue = 100;

            HPS.value = GameManager.hp;
            MPS.value = GameManager.mp;
            CHS.value = (GameManager.current_money / 4294967259 * 100);

            HPText.text = $"{GameManager.hp}/{GameManager.instance.HP_MAX}";
            MPText.text = $"{GameManager.mp}/{GameManager.instance.MP_MAX}";
            CHText.text = $"{CHS.value}%";

        }
        

    }

    //SELL

    public void LoadItemSellScreen()
    {

        if (InterObject.activeSelf == true)
        {
            wasIBOn = true;
            InterObject.SetActive(false);
        }
        else wasIBOn = false;

        Buttons.SetActive(false);
        ItemSellingScreen.SetActive(true);
        GameManager.isFrozen = true;

        if (wasSelling1stOpened)
        {
            SetItemSellScreen();
            wasSelling1stOpened = false;
        }
        else
        {
            SetItemSellScreen();
        }

        GameSoundManager.instance.onClick();
    }

    public void OutItemSellScreen()
    {
        if (wasIBOn) InterObject.SetActive(true);
        Buttons.SetActive(true);
        ItemSellingScreen.SetActive(false);
        GameManager.isFrozen = false;
        GameSoundManager.instance.onClick();
    }
    public void SetItemSellScreen()
    {
        if (wasSelling1stOpened)
        {
            for (int i = 0; i < ItemManager.instance.ItemInfoData.Count; i++)
            {
                Instantiate(SellBoxPrefab, new Vector2(0, 0), Quaternion.identity, SellBoxContent.transform);
            }
        }

        GobblinText.text = $"자네가 가지고 있는 돈은 현재 {GameManager.current_money}원이라네.";

        for (int i = 0; i < ItemManager.instance.ItemInfos.Count; i++)
        {
            Transform SellBoxTrans = SellingItemContent.transform.GetChild(i).transform;

            Image ItemImg = SellBoxTrans.GetChild(0).transform.GetComponent<Image>();
            TMP_Text ItemName = SellBoxTrans.GetChild(1).transform.GetComponent<TMP_Text>();
            TMP_Text ItemText = SellBoxTrans.GetChild(2).transform.GetComponent<TMP_Text>();
            Button button = SellBoxTrans.GetChild(3).transform.GetComponent<Button>();
            ItemSellingController buttonController = button.GetComponent<ItemSellingController>();

            // 0. Get Item Object
            NormalItem TargetItem = ItemManager.instance.ItemInfos[i];

            // 1. Setting Unders
            ItemImg.sprite = ItemManager.instance.FoodSprites[TargetItem.TileID];
            ItemName.text = TargetItem.Name + $"({TargetItem.Price}원)";
            ItemText.text = TargetItem.Description;
            buttonController.InvButtonSettings(i);
        }
    }

    // UNDER : SUBFunctions

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update needed Objects
        Money.text = "잔고: " + string.Format("{0:#,###}", GameManager.current_money) + "원";
        StatusText.text = "현재 평판: " + GameManager.stars.ToString("F3") + " / MP: " + GameManager.mp + " & HP: " + GameManager.hp;
        RatingText.text = $"평판:{GameManager.stars}";
    }
}
