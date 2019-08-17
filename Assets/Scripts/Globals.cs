using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public float rotationSave = 0f;
    public Dictionary<string, float> cooldowns = new Dictionary<string, float>(){{"Arrow", 8}, {"Sword", 1}, {"Shield", 12}, {"Fire", 0.1f}, {"Ice", 6}, {"Air", 1}, {"Time", 24}, {"Lightning", 1}};
}
