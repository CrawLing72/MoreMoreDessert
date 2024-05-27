using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterObjectController : MonoBehaviour
{

    [Header("Button Settings")]
    public GameObject InteractionButton;
    public TMP_Text ButtonText;


    [Header("Imported Windows")]
    public GameObject ImportedWindow_OR_Control;
    public IGMenuManager GMUIadmin;
    public ContentsManager CNTadmin;
    public int determine_window = 0;

    [Header("Showing Settings")]
    public string ShowingText;

    [Header("Chat Settings")]
    public int QuestID;
    public bool isTEXT = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ButtonText.text = ShowingText;
            InteractionButton.SetActive(true);
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.F)) && isTEXT)
        {
            GameManager.QuestIndex = QuestID;
            ChatManager.instance.ImportChats();
   
            InteractionButton.SetActive(false);
            ImportedWindow_OR_Control.SetActive(true);
            GameManager.isFrozen = true;
            /* Animator Setting */
        } else if((collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.F)) && !isTEXT)
        {
            switch (determine_window)
            {
                case 0: // ITEMSTORE
                    GMUIadmin.LoadItemSellScreen();
                    break;

                case 1: // SELLINGGOODS
                    CNTadmin.LoadSellingScreen();
                    break;

                case 2: // SELECTGOODS
                    CNTadmin.LoadMakingScreen();
                    break;
                case 3: // RECEIPERND
                    CNTadmin.LoadRNDScreen();
                    break;

                case 4: // SABO
                    CNTadmin.LoadSABOSelecting();
                    break;

                case 5: // BOOM
                    SaarlandController temp_cnt = ImportedWindow_OR_Control.transform.GetComponent<SaarlandController>();
                    gameObject.SetActive(false);
                    temp_cnt.SetDS();
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            InteractionButton.SetActive(false);
        }
    }

    private void Update()
    {

    }
}
