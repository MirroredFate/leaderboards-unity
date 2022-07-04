using UnityEngine;
using UnityEngine.UI;

public class SubmitButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private GameObject parentEntryObject;

    private Button _submitButton;
    private void Awake()
    {
        _submitButton = GetComponent<Button>();
        _submitButton.onClick.AddListener(AddEntry);
    }


    private void AddEntry()
    {
        //Creates Leaderboards-Entry-Object
        var entry = Instantiate(entryPrefab, parentEntryObject.transform, true);
        
        //Hide entry and fill with data of the input fields
        entry.gameObject.SetActive(false);
        
        
        
    }
}
