using System;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    private static NotificationManager _instance;

    public static NotificationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<NotificationManager>();
            }

            return _instance;
        }
    }

    [SerializeField] private GameObject notificationObject;

    [SerializeField] private Button notificationYesButton;
    [SerializeField] private Button notificationNoButton;

    private void Awake()
    {
        notificationObject.SetActive(false);
        
        notificationNoButton.onClick.AddListener(HideNotification);
        notificationYesButton.onClick.AddListener(OverwriteName);
    }

    public void ShowError()
    {
        notificationObject.SetActive(true);
    }

    private void OverwriteName()
    {
        LeaderboardsManager.Instance.OverwriteEntry();
        
        HideNotification();
    }

    private void HideNotification()
    {
        notificationObject.SetActive(false);
    }
}