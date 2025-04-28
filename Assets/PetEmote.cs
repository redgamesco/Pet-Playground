using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using static BasicPetStats;

[RequireComponent(typeof(Pet))]
public class PetEmote : MonoBehaviour
{
    public List<Emote> SupportedEmotes;
    public Image EmoteDisplayIcon;
    public Animator Animator;

    private Emote? _displayedEmote = null;
    private bool _emoteShowing = false;
    public enum EmoteCondition
    {
        NONE,
        LOW_STAT,
    }

    [System.Serializable]   
    public struct Emote
    {
        public EmoteCondition TriggerCondition;
        public string TriggerKeyword; //just a way to simplify, so we can use a keyword to help identify what emote to display
        public Sprite EmoteIcon;
        
    }

    private Pet _pet;
    public void Start()
    {
        _pet = GetComponent<Pet>();
    }

    public void Update()
    {
        foreach (Emote emote in SupportedEmotes)
        {
            if(emote.TriggerCondition == EmoteCondition.LOW_STAT)
            {
                Enum.TryParse(emote.TriggerKeyword.Trim().ToUpper(), out PetStatType statType);
                bool triggered = CheckEmoteForLowStat(statType, emote.EmoteIcon);
                if (triggered)
                {
                    _displayedEmote = emote;
                    EmoteDisplayIcon.sprite = emote.EmoteIcon;
                    break;
                } else if (_displayedEmote != null && _displayedEmote.Value.EmoteIcon == emote.EmoteIcon)
                {
                    _displayedEmote = null;
                }
            }
        }

        if (_displayedEmote != null && !_emoteShowing)
        {
            _emoteShowing = true;
            DisplayEmote();
        } else if (_displayedEmote == null && _emoteShowing)
        {
            _emoteShowing = false;
            HideEmote();
        }
    }

    public bool CheckEmoteForLowStat(PetStatType petStatType, Sprite icon)
    {
        var statValue = _pet.Info.BasicStats.Stats[petStatType];
        if(statValue < 0.3f)
        {
            return true;
        } 
        return false;
    }

    public void DisplayEmote()
    {
        Animator.ResetTrigger("Hide");
        Animator.SetTrigger("Show");
    }

    public void HideEmote()
    {
        Animator.ResetTrigger("Show");
        Animator.SetTrigger("Hide");
    }
}
