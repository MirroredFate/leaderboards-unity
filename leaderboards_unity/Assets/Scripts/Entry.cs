using System;
using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour
{
    private EntryData _data;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI positionText;

    public void SetName(string name)
    {
        _data.EntryName = name;
        
        nameText.SetText(_data.EntryName);
    }
    
    public void SetScore(int entryScore)
    {
        _data.Score = entryScore;
        
        scoreText.SetText(_data.Score.ToString());
    }
    
    public void SetPosition(int entryPosition)
    {
        _data.Position = entryPosition;
        
        positionText.SetText(_data.Position.ToString());
    }
}