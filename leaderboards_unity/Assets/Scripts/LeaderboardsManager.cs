using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsManager : MonoBehaviour
{
    private static LeaderboardsManager _instance;

    public static LeaderboardsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LeaderboardsManager>();
            }

            return _instance;
        }
    }
    
    private List<EntryData> _entryDataList;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        _entryDataList = new List<EntryData>();
    }

    public void AddDataToList(EntryData data)
    {
        if (_entryDataList.Count > 0)
        {
            for (int i = 0; i < _entryDataList.Count; i++)
            {
                if (data.EntryName == _entryDataList[i].EntryName)
                {
                    return;
                }
            }
        }
        
        _entryDataList.Add(data);
    }

    public void GenerateEntries(GameObject entryPrefab, Transform parent)
    {
        _entryDataList.Sort((p1,p2) => p2.Score.CompareTo(p1.Score));

        for (int i = 0; i < _entryDataList.Count; i++)
        {
            var entry = Instantiate(entryPrefab, parent);
            entry.gameObject.SetActive(false);
            
            entry.GetComponent<Entry>().SetName(_entryDataList[i].EntryName);
            entry.GetComponent<Entry>().SetScore(_entryDataList[i].Score);
            entry.GetComponent<Entry>().SetPosition(i+1);
            
            entry.gameObject.SetActive(true);
        }

    }
}
