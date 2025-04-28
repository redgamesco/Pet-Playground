using UnityEngine;

public class AudioSystem : MonoBehaviour
{

    public SoundLookup soundLookup;
    public GameObject sfxPrefab;
    public GameObject musicPrefab;

    public void PlaySFX(string clip)
    {
        PlaySound(sfxPrefab, clip, -1, float.MaxValue);
    }

    public void PlaySFX(string clip, float volume)
    {
        PlaySound(sfxPrefab, clip, volume, float.MaxValue);
    }

    public void PlaySFX(string clip, float volume, float pitch)
    {
        PlaySound(sfxPrefab, clip, volume, pitch);
    }

    public void PlayMusic(string clip)
    {
        PlaySound(musicPrefab, clip, -1, float.MaxValue);
    }

    public void PlayMusic(string clip, float volume)
    {
        PlaySound(musicPrefab, clip, volume, float.MaxValue);
    }


    public void PlaySound(GameObject prefab, string clip, float volume, float pitch)
    {
        GameObject obj = Instantiate(prefab, this.transform);

        if (prefab == sfxPrefab)
        {
            obj.name = "SFX - " + clip;
        } else
        {
            obj.name = "Music - " + clip;
        }

        AudioSource audioSource = obj.GetComponent<AudioSource>();
        SoundLookup.Sound snd = soundLookup.GetSound(clip);
        if (snd == null)
        {
            Debug.Log($"Warning: Sound Effect {clip} is not registered.");
            return;
        }
        audioSource.clip = snd.clip;

        if (volume > 0)
        {
            audioSource.volume = volume * snd.volume;
        } else
        {
            audioSource.volume = snd.volume;
        }

        if (pitch < float.MaxValue)
        {
            audioSource.pitch = pitch;
        }

        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length * (Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
    }

}
