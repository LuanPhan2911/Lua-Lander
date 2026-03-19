using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statTextMesh;
    [SerializeField] private Button restartButton;
    // Start is called before the first frame update

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
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
            titleTextMesh.text = "Landing Successful!";


        }
        else if (e.landedState == Lander.LandedState.Crash)
        {
            titleTextMesh.text = "Crash";

        }
        else if (e.landedState == Lander.LandedState.TooFast)
        {
            titleTextMesh.text = "Too Fast!";
        }
        else if (e.landedState == Lander.LandedState.SteepAngle)
        {
            titleTextMesh.text = "Steep Angle!";
        }
        else if (e.landedState == Lander.LandedState.OutOfFuel)
        {
            titleTextMesh.text = "Out of Fuel!";
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

    // Update is called once per frame

}
