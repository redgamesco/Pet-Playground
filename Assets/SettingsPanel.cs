using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    
    public AudioMixerGroup MixerGroup;
    public Image AnimatedBackground;

    public Toggle MusicToggle;
    public Toggle SFXToggle;
    // MODULE 5:// public Toggle AnimatedBackgroundToggle;
    /* MODULE 4:
    public async void Initialize()
    {
        //Apply saved settings
        MusicToggle.SetIsOnWithoutNotify(GameController.GetInstance().Player.SettingsPreferences.MusicVol == 0);
        SFXToggle.SetIsOnWithoutNotify(GameController.GetInstance().Player.SettingsPreferences.SFXVol == 0);
        // MODULE 5:// AnimatedBackgroundToggle.SetIsOnWithoutNotify(GameController.GetInstance().Player.SettingsPreferences.AnimatedBackground);

        await Task.Delay(100);
        UpdateToggles();
    }

    public void ToggleSettingsPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void UpdateToggles()
    {
        UpdateMusic();
        UpdateSFX();
        // MODULE 5:// UpdateAnimatedBackground();
    }

    public float CalculateVolume(bool isOn)
    {
        return isOn ? 0f : -80f;
    }

    public void UpdateMusic()
    {
        GameController.GetInstance().Player.SettingsPreferences.MusicVol = CalculateVolume(MusicToggle.isOn);
        MixerGroup.audioMixer.SetFloat("MusicVol", CalculateVolume(MusicToggle.isOn));
    }

    public void UpdateSFX()
    {
        GameController.GetInstance().Player.SettingsPreferences.SFXVol = CalculateVolume(SFXToggle.isOn);
        MixerGroup.audioMixer.SetFloat("SFXVol", CalculateVolume(SFXToggle.isOn));
    }
    
    /* MODULE 5:
    public void UpdateAnimatedBackground()
    {
        GameController.GetInstance().Player.SettingsPreferences.AnimatedBackground = AnimatedBackgroundToggle.isOn;

        //The below line is a bit complex, so here's a little breakdown!
        //AnimatedBackground.material gets us an instance of the material being used on the image. We can safetly edit this.
        //SetInteger lets us change a variable in the shader of the material. We're requesting to change the value of _AnimatedBackground
        //The last part is a ternary operator, which is a condensed if-statement. If animatedBackground is true, the value is 1. If not, the value is 0.
        AnimatedBackground.material.SetFloat("_Animated", AnimatedBackgroundToggle.isOn ? 1 : 0);
    }*/

    [System.Serializable]
    public struct SettingPreferences
    {
        public float MusicVol;
        public float SFXVol;
        // MODULE 5:// public bool AnimatedBackground;

        public void SettingsPreferences()
        {
            MusicVol = 0;
            SFXVol = 0;
            // MODULE 5:// AnimatedBackground = true;
        }
    }
}
