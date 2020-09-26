using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;

    public static string DisplayName { get; private set; }

    private const string PLayerPrefsNameKey = "PlayerName";

    private void Start() => SetInputField();

    private void SetInputField() {

        if (!PlayerPrefs.HasKey("PlayerName")) { return; }
        string defaultName = PlayerPrefs.GetString("PlayerName");
        nameInputField.text = defaultName;
        SerPLayerName(defaultName);
    }
    public void SerPLayerName(string name) {
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }
    public void SavePlayerName() {
        DisplayName = nameInputField.text;
        PlayerPrefs.GetString("PlayerName", DisplayName);
    }

}
