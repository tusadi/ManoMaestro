using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleUIRow : MonoBehaviour
{
    #region Singleton
    private static ToggleUIRow _instance;
    public static ToggleUIRow Instance
    {
        get
        {
            return _instance;
        }

    }
    #endregion


    [SerializeField]
    Button[] backgroundModeIcons;

    [SerializeField]
    Button backgroundSelectorIcon;


    public bool backgroundMenuIsOpen;



    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        for (int i = 0; i < backgroundModeIcons.Length; i++)
        {
            backgroundModeIcons[i].onClick.AddListener(ClickedBackgroundMode);
        }
        backgroundSelectorIcon.onClick.AddListener(ClickedMenuButoon);
        backgroundMenuIsOpen = true;
    }



    /// <summary>
    /// Toggles the boolean value of menu being open everytime the user clicks the menu button.
    /// </summary>
    void ClickedMenuButoon()
    {
        backgroundMenuIsOpen = !backgroundMenuIsOpen;
    }

    /// <summary>
    /// Sets the boolean value of background menu being open to false.
    /// </summary>
    void ClickedBackgroundMode()
    {
        backgroundMenuIsOpen = false;
    }
}
