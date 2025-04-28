using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BasicPetStats;

public class GameController : MonoBehaviour
{

    public PlayerInfo Player;
    public GameObject DebugPanel;
   //MODULE 4: // public SettingsPanel SettingsPanel;

    public static GameController instance = null;
    public Pet CurrentPet;

    //Pets will lose 10% of their stats every second
    public float StatDecayRate = -0.10f;

    public List<PetLookupInfo> PetLookupInfo = new List<PetLookupInfo>();

     //MODULE 2: //public AudioSystem AudioSystem;

    public static GameController GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameController[] objs = GameObject.FindObjectsByType<GameController>(FindObjectsSortMode.None);
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (LoadSave())
        {
            CurrentPet.Info = Player.CurrentPetInfo;
            Sprite selectedPetSprite = GetPetLookupInfo(Player.CurrentPetInfo.PetType).Sprite;
            CurrentPet.UpdateSprite(selectedPetSprite);
        }
        else
        {
            Player = new PlayerInfo();
            CreatePet();
        }

        //MODULE 4: //SettingsPanel.Initialize();
        //MODULE 2: //AudioSystem.PlayMusic("BGM");
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private string _saveKey = "playerSave";
    public void SaveGame()
    {
        Player.CurrentPetInfo = CurrentPet.Info;
        PlayerPrefs.SetString(_saveKey, JsonUtility.ToJson(Player));
        UnityEngine.Debug.Log("Game has been saved!");
    }

    public bool LoadSave()
    {
        if (PlayerPrefs.HasKey(_saveKey))
        {
            try
            {
                Player = JsonUtility.FromJson<PlayerInfo>(PlayerPrefs.GetString(_saveKey));
                UnityEngine.Debug.Log("Game has been loaded!");
                return true;
            } catch (Exception e)
            {
                UnityEngine.Debug.Log("Issue with loading save: " + e);
                return false;
            }
        } else
        {
            UnityEngine.Debug.Log("Unable to load save.");
            return false;
        }
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteKey(_saveKey);
        Player = new PlayerInfo();
        CreatePet();
    }

    public void CreatePet()
    {
        int index = UnityEngine.Random.Range(0, PetLookupInfo.Count);
        Player.CurrentPetInfo.PetType = (PetType)index;

        Sprite selectedPetSprite = PetLookupInfo[index].Sprite;
        CurrentPet.UpdateSprite(selectedPetSprite);

        CurrentPet.Info = Player.CurrentPetInfo;
    }

    public void Update()
    {
        //calculate how many seconds have passed since this function was last called
        //and apply stat decay to the pet accordingly
         HandleStatDecay();
    }   

    public void HandleStatDecay()
    {
        if(CurrentPet != null)
        {
            CurrentPet.Info.BasicStats.CalculateStatDecay(StatDecayRate);
        }
    }

    public void UpdateStatDecay(float amt)
    {
        StatDecayRate = Mathf.Min(-0.01f, StatDecayRate - amt);
    }

    public void ChangeStat(PetStatType statType, float amt)
    {
        if (CurrentPet != null)
        {
            CurrentPet.Info.BasicStats.ChangeStat(statType, amt);
        }
    }

    public void ReplenishHunger()
    {
        UnityEngine.Debug.Log("Replenish hunger!");
        ChangeStat(PetStatType.HUNGER, 1);
        //MODULE 3://  AudioSystem.PlaySFX("ButtonClick", 0.8f, UnityEngine.Random.Range(0.98f, 1.02f));
    }
    public void ReplenishHappiness()
    {
        UnityEngine.Debug.Log("Replenish happiness!");
        ChangeStat(PetStatType.HAPPINESS, 1);
    }
    public void ReplenishEnergy()
    {
        UnityEngine.Debug.Log("Replenish energy!");
        ChangeStat(PetStatType.ENERGY, 1);
    }

    /* MODULE 1
    public void ReplenishThirst()
    {
        UnityEngine.Debug.Log("Replenish thirst!");
       ChangeStat(PetStatType.THIRST, 1);
    }*/


    public void ToggleDebugPanel()
    {
        DebugPanel.SetActive(!DebugPanel.activeSelf);
    }

    public PetLookupInfo GetPetLookupInfo( PetType type)
    {
        foreach(PetLookupInfo info in PetLookupInfo)
        {
            if(info.Type == type) return info;
        }
        return PetLookupInfo[0];
    }
}
