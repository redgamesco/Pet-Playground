using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[System.Serializable]
public class PetInfo
{
    public PetType PetType;
    public long TimeWhenCreated = -1;
    public BasicPetStats BasicStats = new BasicPetStats();
}

[System.Serializable]
public enum PetType
{
    LUCKY, //duck pet
}

//Information about pets based on the pet type
[System.Serializable]
public struct PetLookupInfo
{
    public PetType Type;
    public Sprite Sprite;
}
