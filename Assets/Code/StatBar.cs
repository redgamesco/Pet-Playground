using UnityEngine;
using UnityEngine.UI;
using static BasicPetStats;

public class StatBar : MonoBehaviour
{
    public PetStatType StatType;
    private Slider Slider;

    public void Start()
    {
        Slider = GetComponent<Slider>();
    }

    public void Update()
    {
        if (Slider != null && 
            GameController.instance.CurrentPet != null &&
            GameController.instance.CurrentPet.Info.BasicStats.Stats.ContainsKey(StatType))
        {
            //Sliders are percentage from 0f - 1f, like our Stats
            Slider.value = GameController.instance.CurrentPet.Info.BasicStats.Stats[StatType];
        }
    }
}
