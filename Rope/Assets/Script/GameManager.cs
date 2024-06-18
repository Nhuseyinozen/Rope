using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Panels; // Buton islemeri.
    [SerializeField] private GameObject[] Rope_Centers; // ip merkezleri.
    [SerializeField] private AudioSource[] Sounds;
    [SerializeField] private TextMeshProUGUI[] levelTxt;
    [SerializeField] private ParticleSystem rope_Effect;
    public int avaibleBallCount; // mevcut top sayýsý.
    public int targetBlockCount; // düþmesi gereken engel sayýsý.

    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButton(0))
            {
                
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Center1"))
                    {
                        RopeTechnical(hit, "Center1");
                    }
                    else if (hit.collider.CompareTag("Center2"))
                    {
                        RopeTechnical(hit, "Center2");
                    }
                    else if (hit.collider.CompareTag("Center3"))
                    {
                        RopeTechnical(hit, "Center3");
                    }
                    else if (hit.collider.CompareTag("Center4"))
                    {
                        RopeTechnical(hit, "Center4");
                    }
                }
            }
        }

    }

    public void butonPanelTechnical(string butonValue)
    {
        if (butonValue == "Paused")
        {
            Time.timeScale = 0;
            Panels[0].SetActive(true);
        }
        else if (butonValue == "Resume")
        {
            Time.timeScale = 1;
            Panels[0].SetActive(false);
        }
        else if (butonValue == "Quit")
        {
            Application.Quit();
        }
        else if (butonValue == "Next")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 1);
        }
        else if (butonValue == "Replay")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }

    }

    void Completed()
    {
        Time.timeScale = 0;
        Panels[1].SetActive(true);
        Sounds[2].Play();
        levelTxt[0].text = "LEVEL : " + (SceneManager.GetActiveScene().buildIndex).ToString();
    }

    void Failed()
    {
        Time.timeScale = 0;
        Panels[2].SetActive(true);
        Sounds[1].Play();
        levelTxt[1].text = "LEVEL : " + (SceneManager.GetActiveScene().buildIndex).ToString();
    }

    void RopeTechnical(RaycastHit2D hit, string name)
    {
        hit.collider.gameObject.SetActive(false);

        rope_Effect.transform.position = hit.transform.position;
        rope_Effect.Play();

        Sounds[0].Play();

        foreach (var item in Rope_Centers)
        {
            if (item.GetComponent<Rope>().JointName == name)
            {
                foreach (var item2 in item.GetComponent<Rope>().ConnectionPool)
                {
                    item2.SetActive(false);
                }
            }
        }
    }

    public void BallDown()
    {
        // Top düþtü.
        avaibleBallCount--;
        if (avaibleBallCount == 0)
        {
            if (targetBlockCount > 0)
            {
                Failed();
            }
            else if (targetBlockCount == 0)
            {
                Completed();
            }
        }
        else if (targetBlockCount == 0)
        {
            Completed();
        }
    }

    public void BlockDown()
    {
        //Engel düþtü.
        targetBlockCount--;

        if (targetBlockCount == 0)
        {
            Completed();
        }
        else if (avaibleBallCount == 0 && targetBlockCount > 0)
        {
            Failed();
        }

    }

}
