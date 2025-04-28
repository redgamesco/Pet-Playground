using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


[CreateAssetMenu(fileName = "PushNotifsList", menuName = "ScriptableObjects/PushNotifsList", order = 1)]
public class PushNotifsList : ScriptableObject
{
    public List<string> PushNotifs;
}
