using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "ScriptableObjects/Conversation", order = 2)]
public class Conversation : ScriptableObject
{
    public string ConversationName;
    public List<string> Text;
}
