using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExampleHub : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var buttons = this.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => SceneManager.LoadScene("Example1_HelloWorld"));
        buttons[1].onClick.AddListener(() => SceneManager.LoadScene("Example2_BasicDatabaseOperations"));
        buttons[2].onClick.AddListener(() => SceneManager.LoadScene("Example3_Querying"));
        buttons[3].onClick.AddListener(() => SceneManager.LoadScene("Example4_CKAsset"));
        buttons[4].onClick.AddListener(() => SceneManager.LoadScene("Example5_Zones"));
        buttons[5].onClick.AddListener(() => SceneManager.LoadScene("Example6_Progress"));
        buttons[6].onClick.AddListener(() => SceneManager.LoadScene("Example7_AccountStatus"));
        buttons[7].onClick.AddListener(() => SceneManager.LoadScene("Example8_KeyValueStore"));
        buttons[8].onClick.AddListener(() => SceneManager.LoadScene("Example9_Subscriptions"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
