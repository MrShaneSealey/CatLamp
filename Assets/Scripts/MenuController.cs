using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    //[SerializeField] private GameObject ccMenu;
    //[SerializeField] private GameObject particleMenu;
    //[SerializeField] private GameObject objectMenu;

    private bool toggle = false;

    void hidePanel()
    {
        menu.SetActive(false);

        Debug.Log("menu false");
    }

    void unhidePanel()
    {
        menu.SetActive(true);
        
        Debug.Log("menu true");

    }

    public void Toggle()
    {
        if (!toggle)
        {
            toggle = true;
            unhidePanel();
        }
        else if (toggle)
        {
            toggle = false;
            hidePanel();
        }
    }
}
