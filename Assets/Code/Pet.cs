using System;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    public PetInfo Info = new PetInfo();
    public Image PetImage;

    public void Start()
    {
        if(Info.TimeWhenCreated < 0)
        {
            //This pet doesn't have a birthday yet!
            Info.TimeWhenCreated = DateTime.Now.ToFileTime();
        }
        Info.BasicStats.Initialize();
    }

    public void UpdateSprite(Sprite sprite)
    {
        PetImage.sprite = sprite;
    }
}
