using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private GameObject parentEntryObject;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField scoreInput;

    private Button _submitButton;
    private void Awake()
    {
        _submitButton = GetComponent<Button>();
        _submitButton.onClick.AddListener(AddEntry);
    }


    private void AddEntry()
    {
        if (nameInput.text == "" || scoreInput.text == "") return;
        if(Convert.ToInt32(scoreInput.text) >= Int32.MaxValue || Convert.ToInt32(scoreInput.text) <= Int32.MinValue) return; //Maybe Score too high error message?

        DeleteEntryObjects();

        var data = new EntryData
        {
            EntryName = nameInput.text,
            Score = Convert.ToInt32(scoreInput.text)
        };

        LeaderboardsManager.Instance.AddDataToList(data);
        SaveSystem.Save();
        
        LeaderboardsManager.Instance.GenerateEntries(entryPrefab, parentEntryObject.transform);
    }

    private void DeleteEntryObjects()
    {
        foreach (Transform child in parentEntryObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
