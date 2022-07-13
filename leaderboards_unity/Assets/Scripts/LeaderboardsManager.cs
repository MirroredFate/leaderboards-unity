using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private GameObject yourEntryObject;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField scoreInput;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private GameObject parentEntryObject;
    [SerializeField] private GameObject crownImage;

    private EntryData _yourData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        crownImage.SetActive(false);
        LoadSaveData();
    }
    

    public void AddDataToList(EntryData data)
    {
        if (_entryDataList.Count > 0)
        {
            for (var i = 0; i < _entryDataList.Count; i++)
            {
                if (data.EntryName == _entryDataList[i].EntryName)
                {
                    NotificationManager.Instance.ShowError();
                    return;
                }
            }
        }
        
        _entryDataList.Add(data);
        _yourData = data;
    }

    public void GenerateEntries(GameObject entryPrefab, Transform parent)
    {
        _entryDataList.Sort((p1,p2) => p2.Score.CompareTo(p1.Score));
        crownImage.SetActive(true);

        for (var i = 0; i < _entryDataList.Count; i++)
        {
            var entry = Instantiate(entryPrefab, parent);
            entry.gameObject.SetActive(false);
            
            entry.GetComponent<Entry>().SetName(_entryDataList[i].EntryName);
            entry.GetComponent<Entry>().SetScore(_entryDataList[i].Score);
            entry.GetComponent<Entry>().SetPosition(i+1);

            entry.gameObject.SetActive(true);

            if (_entryDataList[i].EntryName != _yourData.EntryName) continue;
            entry.GetComponent<Image>().color = Color.yellow;
                
            yourEntryObject.GetComponent<Entry>().SetName(_entryDataList[i].EntryName);
            yourEntryObject.GetComponent<Entry>().SetScore(_entryDataList[i].Score);
            yourEntryObject.GetComponent<Entry>().SetPosition(i+1);
            yourEntryObject.GetComponent<Image>().color = Color.yellow;
                
            _yourData.Position = i + 1;
        }

        if (_yourData.Position >= 7)
        {
            yourEntryObject.gameObject.SetActive(true);
        }
    }

    public void OverwriteEntry()
    {
        foreach (Transform child in parentEntryObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        for (var i = 0; i < _entryDataList.Count; i++)
        {
            if(_entryDataList[i].EntryName != nameInput.text) continue;

            _entryDataList.RemoveAt(i);
            var newData = new EntryData
            {
                EntryName = nameInput.text,
                Score = Convert.ToInt32(scoreInput.text)
            };

            _entryDataList.Add(newData);
            _yourData = newData;
            GenerateEntries(entryPrefab, parentEntryObject.transform);
        }
    }

    public List<EntryData> GetEntryList()
    {
        return _entryDataList;
    }
    

    private void LoadSaveData()
    {
        var data = SaveSystem.Load();
        
        _entryDataList = new List<EntryData>();

        if (data == null) return;
        for (var i = 0; i < data.entryAmount; i++)
        {
            var entryData = new EntryData
            {
                Score = data.entryScores[i],
                EntryName = data.entryNames[i]
            };

            _entryDataList.Add(entryData);
        }
        GenerateEntries(entryPrefab, parentEntryObject.transform);
    }
}
