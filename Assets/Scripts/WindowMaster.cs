using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowMaster : MonoBehaviour
{

    public static WindowMaster Instance { get; private set; } //Singleton pattern
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    [SerializeField] string dataSetPath; //Folder where each data sets files are

    public string DataSetPath { get { return dataSetPath; } }

    [SerializeField] List<Window> windows; //List all windows

    //Open the window via the selected button
    public void OnWindowOpen(Window window) 
    {
        for (int i = 0; i < windows.Count; i++) //Iterate through all windows, if it isn't the one asked to open, deactivate (maybe it's already open so it's needed to close)
        {
            if(windows[i] != window) windows[i].gameObject.SetActive(false);
            else windows[i].gameObject.SetActive(true); //Open the window
        }
    }

    //Just to make sure everything is closed
    public void OnWindowClose() 
    {
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].gameObject.SetActive(false);
        }
    }
}
