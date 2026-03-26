using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statTextMesh;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] private Button loadOnSavePointButton;
    [SerializeField] private GameObject fireworkGameObject;



    // Start is called before the first frame update

    private Action NextButtonClick;


    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            NextButtonClick();
        });

        loadOnSavePointButton.onClick.AddListener(() =>
        {
            LoadSavePoint();

        });
    }

    void Start()
    {

        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnSavePointReached += Lander_OnSavePointReached;
        loadOnSavePointButton.interactable = false;

        Hide();
    }

    private void Lander_OnSavePointReached(object sender, Lander.OnSavePointReachedEventArgs e)
    {
        loadOnSavePointButton.interactable = GameManager.Instance.IsHaveSavePoint();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        Show();
        if (e.landedState == Lander.LandedState.Success)
        {
            titleTextMesh.text = "<wave><palette>LANDING SUCCESSFUL!</palette></wave>";
            nextButtonTextMesh.text = "NEXT";

            GameObject firework = Instantiate(fireworkGameObject, Lander.Instance.transform.position - 10 * Vector3.up,
                 Quaternion.identity);

            NextButtonClick = () =>
            {
                GameManager.Instance.LoadNextLevel();
                Destroy(firework);

            };
        }
        else
        {
            titleTextMesh.text = "<shake>CRASH</shake>";
            titleTextMesh.color = Color.red;
            nextButtonTextMesh.text = "RESTART";
            NextButtonClick = () =>
            {
                GameManager.Instance.RestartLevel();
            };

        }
        statTextMesh.text =
                 $"{GameManager.Instance.GetLevelNumber()}\n" +
                 $"{GameManager.Instance.GetTimeFormatted()}\n" +
                 $"{GameManager.Instance.GetScore()}";


    }

    private void LoadSavePoint()
    {
        GameManager.Instance.LoadSavePointLevel();
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
