using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("Basic Settings")]
    public Animator MenuAnimator;
    string menuParameter = "MenuSetting";

    public Button[] Buttonlist;
    public TextMeshProUGUI[] Textlist;

    public void backToMenu()
    {
        MenuAnimator.SetInteger(menuParameter, 0);
        GameSoundManager.instance.onClick();
    }

    public void goToLoad()
    {
        MenuAnimator.SetInteger(menuParameter, 1);
        GameSoundManager.instance.onClick();
    }

    public void goToSettings()
    {
        MenuAnimator.SetInteger(menuParameter, 2);
        GameSoundManager.instance.onClick();
    }

    public void startNewGame()
    {
        MenuAnimator.SetInteger(menuParameter, 3);
        DataManager.isNewGame = true;

        PlayerPrefs.SetString("NewScene", "MainGame");
        GameSoundManager.instance.onClick();
        Invoke("GotoGame", 1f);

    }

    public void loadGame(int slot)
    {
        DataManager.isNewGame = false;
        DataManager.now_slot = slot;

        Debug.Log(DataManager.now_slot);

        PlayerPrefs.SetString("NewScene", "MainGame");
        GameSoundManager.instance.onClick();
        Invoke("GotoGame", 0.5f);
    }

    private void GotoGame()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void SettingLoadButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// �����Ͱ� �ִ� ���
            {
                DataManager.now_slot = i;	// ������ ���� ��ȣ ����
                DataManager.instance.LoadData();	// �ش� ���� ������ �ҷ���
                Textlist[i].text = DataManager.instance.nowPlayer.lastly_saved_date + "\n" + DataManager.instance.nowPlayer.current_money + "�� ����";
                Buttonlist[i].interactable = true;
            }
            else	// �����Ͱ� ���� ���
            {
                Textlist[i].text = "������ �������� �ʽ��ϴ�.";
                Buttonlist[i].interactable = false;
            }
        }
        GameSoundManager.instance.onClick();
        DataManager.instance.DataClear();
    }



    /* ------- Unity Broadcast Functions --------- */

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
    }

    // Update is called once per frame
    void Update()
    {
    }
}
