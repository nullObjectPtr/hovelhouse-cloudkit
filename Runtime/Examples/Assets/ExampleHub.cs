﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExampleHub : MonoBehaviour
{
    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(() => SceneManager.LoadScene("Example1_HelloWorld"));
        buttons[1].onClick.AddListener(() => SceneManager.LoadScene("Example2_BasicDatabaseOperations"));
        buttons[2].onClick.AddListener(() => SceneManager.LoadScene("Example3_Querying"));
        buttons[3].onClick.AddListener(() => SceneManager.LoadScene("Example4_CKAsset"));
        buttons[4].onClick.AddListener(() => SceneManager.LoadScene("Example5_Zones"));
        buttons[5].onClick.AddListener(() => SceneManager.LoadScene("Example6_Progress"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}