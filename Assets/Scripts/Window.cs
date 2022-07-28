using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Window : MonoBehaviour
{
    Transform scrollContent;
    string filePath;

    [SerializeField] string dataSetFileName;
    [SerializeField] GameObject windowBtn;
    [SerializeField] Text windowContent;

    RectTransform scrollRect;
    Vector2 offMin, offMax;


    private void Awake()
    {
        scrollContent = transform.GetChild(0).GetChild(0).GetChild(0);
        scrollRect = scrollContent.GetComponent<RectTransform>();
        //Set two vectors, which represents ScrollRect at top
        offMax = scrollRect.offsetMax;
        offMin = scrollRect.offsetMin;
    }

    void SafeInit()
    {
        scrollContent = transform.GetChild(0).GetChild(0).GetChild(0);
        scrollRect = scrollContent.GetComponent<RectTransform>();
        //Set two vectors, which represents ScrollRect at top
        offMax = scrollRect.offsetMax;
        offMin = scrollRect.offsetMin;
    }

    void Start()
    {
        filePath = WindowMaster.Instance.DataSetPath + dataSetFileName;
        SetupButtons();
        windowContent.text = "";
    }

    //Creating one button for each data in the list of Items
    void SetupButtons() 
    {
        List<Item> items = JsonDeserializer.Deserialize<List<Item>>(filePath); //If this projet had to build, use persistantDataPath or StreamingBuildAssets
        for (int i = 0; i < items.Count; i++)
        {
            GameObject go = Instantiate(windowBtn, scrollContent); //Copy a prefab already setup, and put it in the ScrollRect
            go.transform.GetChild(0).GetComponent<Text>().text = items[i].title;
            string txt = items[i].content;
            go.GetComponent<Button>().onClick.AddListener(delegate { OnClickButton(txt); });
        }
    }

    //Ask the master to open the window
    public void ShowWindow()
    {
        if (gameObject.activeSelf) return; //If this window is already open, don't do anything
        //When opening window, set ScrollRect at the top
        if (scrollRect == null) SafeInit();
        else
        {
            scrollRect.offsetMax = offMax;
            scrollRect.offsetMin = offMin;
        }
        WindowMaster.Instance.OnWindowOpen(this);
        windowContent.text = "";
    }

    //Ask the master to close the window with the X button clicked
    public void CloseWindow()
    {
        windowContent.text = ""; //Make sure there's no content left on the window.
        WindowMaster.Instance.OnWindowClose();
    }

    //Show a content whena button in the ScrollRect is clicked
    //Didn't put the method body here in case we have to implement other things
    void OnClickButton(string content)
    {
        ShowContent(content);
    }

    void ShowContent(string content)
    {
        windowContent.text = content;
    }

    
}
