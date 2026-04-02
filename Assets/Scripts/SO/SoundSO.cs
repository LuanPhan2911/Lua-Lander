using UnityEngine;


[CreateAssetMenu(fileName = "SoundSO", menuName = "ScriptableObjects/SoundSO")]
public class SoundSO : ScriptableObject
{

    public AudioClip audioClip;
    public SoundManager.SoundType type;
}
