using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
[System.Serializable]

public class BasicPetStats
{
    [HideInInspector] public long LastTimeUpdated = -1;

    //Each stat below this line should be between 0-1 (0% - 100%)

    public SerializedDictionary<PetStatType, float> Stats = new SerializedDictionary<PetStatType, float>() {
          { PetStatType.HUNGER, 1.00f},
          { PetStatType.HAPPINESS, 1.00f},
          { PetStatType.ENERGY, 1.00f},
         //MODULE 1:// { PetStatType.THIRST, 1.00f},
    };

    public void Initialize()
    {
        FixLegacyStats();
        if(LastTimeUpdated < 0)
        {
            LastTimeUpdated = DateTime.UtcNow.ToFileTime();
        }
    }

    public void FixLegacyStats()
    {
        //If any stat is missing from our pet, add it in!
        if (!Stats.ContainsKey(PetStatType.HUNGER))
        {
            Stats.Add(PetStatType.HUNGER, 1.00f);
        }
        if (!Stats.ContainsKey(PetStatType.HAPPINESS))
        {
            Stats.Add(PetStatType.HAPPINESS, 1.00f);
        }
        if (!Stats.ContainsKey(PetStatType.ENERGY))
        {
            Stats.Add(PetStatType.ENERGY, 1.00f);
        }
        /*MODULE 1:
        if (!Stats.ContainsKey(PetStatType.THIRST))
        {
            Stats.Add(PetStatType.THIRST, 1.00f);
        }*/
    }

    [System.Serializable]
    public enum PetStatType{
        NONE,
        HUNGER = 1,
        HAPPINESS = 2,
        ENERGY = 3,
        //MODULE 1:// THIRST = 4,
    }


    public void CalculateStatDecay(float decayRate)
    {
        var lastUpdatedTime = DateTime.FromFileTime(LastTimeUpdated);
        double timePassedMilliseconds = (DateTime.Now - lastUpdatedTime).TotalMilliseconds;
        
        LastTimeUpdated = DateTime.Now.ToFileTime();

        Dictionary<PetStatType, float> dictionaryCopy = Stats.ToDictionary(k => k.Key, v => v.Value); ;
        foreach (var keyValuePair in dictionaryCopy)
        {
            //Apply stat decay to each stat, but don't let them drop below 0% or above 100%
            float secondsPassed = ((float)timePassedMilliseconds) / 1000;
            Stats[keyValuePair.Key] = Mathf.Clamp(keyValuePair.Value + (secondsPassed * decayRate), 0.0f, 1.0f);
        }
    }

    public void ChangeStat(PetStatType statType, float amt)
    {
        if (Stats.ContainsKey(statType))
        {
            Stats[statType] = Mathf.Clamp(Stats[statType] + amt, 0.0f, 1.0f);
        }
    }
}
