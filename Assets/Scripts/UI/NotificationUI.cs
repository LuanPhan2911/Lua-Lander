using TMPro;
using UnityEngine;
using static GameManager;

public class NotificationUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float showDuration;
    private float timer;

    private void Awake()
    {
        timer = showDuration;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            UpdateUI("");
        }

    }

    private void Start()
    {


        GameManager.Instance.OnMessageAdded += GameManager_OnMessageAdded;

    }

    private void GameManager_OnMessageAdded(object sender, OnMessageAddedEventArgs e)
    {

        UpdateUI(e.message);

    }

    private void UpdateUI(string newMessage)
    {


        textMesh.text = newMessage;
        timer = showDuration;


    }






}
