using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExampleUI : MonoBehaviour
{
    public Text text;
    public Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(OnBackClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void OnBackClick()
    {
        SceneManager.LoadScene("_Examples_");
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        text.text += condition + "\n";
    }
}
