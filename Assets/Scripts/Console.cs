using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Console : MonoBehaviour
{
    string[] lines = new string[15];
    public Text text;
    public void Print(string message)
    {
        Shift();
        lines[0] = message;
        UpdateUI();

    }
    private void UpdateUI()
    {
        string sum = "";
        foreach(string line in lines) {
            if (line != null)
                sum += line + "\n";
        }
        text.text = sum;
    }
    private void Shift()
    {
        for(int i = lines.Length - 2; i >= 0; i--)
        {
            lines[i + 1] = lines[i];
        }
    }
}
