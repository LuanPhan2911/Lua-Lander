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
    [SerializeField] private GameObject fireworkGameObject;



    // Start is called before the first frame update

    private Action NextButtonClick;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            NextButtonClick();
        });
    }

    void Start()
    {

        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        Show();
        if (e.landedState == Lander.LandedState.Success)
        {
            titleTextMesh.text = "LANDING SUCCESSFUL!";
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
            titleTextMesh.text = "CRASH";
            nextButtonTextMesh.text = "RESTART";
            NextButtonClick = () =>
            {
                GameManager.Instance.RestartLevel();
            };

        }
        statTextMesh.text =
                 $"{Mathf.Round(e.landingSpeed) * 10}\n" +
                 $"{Mathf.Round(e.landingAngle * 100)}\n" +
                 $"x{e.multiplier}\n" +
                 $"{e.score}";


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
