using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaarlandController : MonoBehaviour
{
    public GameObject SaarlandBagetteNR, SarrlandBagetteDS;
    public InterObjectController targetController;
    public int goal_questID;
    public GameObject ExplosionPrefab;

    private int previous_index = 0;

    public void SetNR()
    {
        SaarlandBagetteNR.SetActive(true);
        SarrlandBagetteDS.SetActive(false);
        ContentsManager.instance.isSaarlandOn = true;
        targetController.QuestID = previous_index;
        ContentsManager.instance.PhysicsSABO.SetActive(true);
    }

    public void SetDS()
    {
        Instantiate(ExplosionPrefab, SarrlandBagetteDS.transform);
        SaarlandBagetteNR.SetActive(false);
        SarrlandBagetteDS.SetActive(true);
        ContentsManager.instance.isSaarlandOn = false;
        targetController.QuestID = goal_questID;

        int determine_integer = UnityEngine.Random.Range(0, 1000);

        if (determine_integer < 100)
        {
            GameManager.stars *= (0.5f + Random.Range(0.25f, 0.3f));
        }
        else
        {
            GameManager.stars *= (0.5f + Random.Range(0.55f, 0.9f));
        }
        
        GameSoundManager.instance.PlayEffects("Explosion");

        Invoke("SetNR", 120f);
    }
    

    public void Start()
    {
        previous_index = targetController.QuestID;
    }
}
