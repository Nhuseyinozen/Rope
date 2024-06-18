using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{

    void Start()
    {
        Invoke("Load", 3f);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }


}
