using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    [Header ("Script Data Setting")]
    public string FileName;

    [Header("Target Object Setting")]
    public GameObject MessageBox;
    public GameObject InteractionButton;
    public GameObject UIButtons;
    public TMP_Text CharName;
    public TMP_Text Message;
    public Image CharImg;

    [Header("Message Resources Setting")]
    public Dictionary<int, string> CharPairs = new Dictionary<int, string>() { { 0, "레이니크" }, { 1, "카세우스" }, { 2, "???" } };
    public Dictionary<int, string> CharIndexs = new Dictionary<int, string>() { { 0, "Rainyk" }, { 1, "Caseus" }, { 2, "NULL" } };

    [Header("ETC ENV Setting")]
    public bool isMessageAlive = false;


    //Private Assets
    private List<Dictionary<string, object>> ChatData;
    private Dictionary<int, string> ProcessedData = new Dictionary<int, string>();

    int cost = 0;
    int current_Index;
     
    bool checkKeys(int questNum, int MSSNum)
    {
        foreach (int elem in ProcessedData.Keys)
        {
            string elem_fir = elem.ToString().Substring(0, 2);
            int elem_lat = elem % 100;
            if ((elem_fir == questNum.ToString()) && (elem_lat == MSSNum))
            {
                current_Index = elem;
                return true;
            }
        }
        return false;

    }

    public void ImportChats()
    {
        UIButtons.SetActive(false);
        isMessageAlive = true;

        current_Index = GameManager.QuestIndex;
        current_Index += cost;

        int QuestNum = current_Index / 10000;
        int MSSIndex = (current_Index % 100);
        if (checkKeys(QuestNum, MSSIndex))
        {
            int CharIndex = (current_Index / 1000) - (QuestNum * 10);
            int FaceIndex = (current_Index / 100) - (QuestNum * 100) - (CharIndex * 10);

            CharName.text = CharPairs[CharIndex];
            Message.text = ProcessedData[current_Index];

            if (CharIndex != 2)
            {
                CharImg.color = new Color(255, 255, 255, 255);
                string address = "Images/Chars/" + CharIndexs[CharIndex] + "_" + FaceIndex.ToString();
                CharImg.sprite = Resources.Load<Sprite>(address);
            }
            else
            {
                CharImg.color = new Color(0, 0, 0, 0);
            }

            GameSoundManager.instance.startCharVoice(current_Index);
        }
        else
        {
            MessageBox.SetActive(false);
            InteractionButton.SetActive(true);
            UIButtons.SetActive(true);

            GameManager.isFrozen = false;
            isMessageAlive = false;

            cost = 0;
        }
    }


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

        ChatData = CSVReader.Read(FileName);
        for (int i =0; i < ChatData.Count; i++)
        {
            ProcessedData.Add(int.Parse(ChatData[i]["QuestID"].ToString()), ChatData[i]["Description"].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMessageAlive && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Implemented!");

            cost++;
            ImportChats();
        }
    }
}
