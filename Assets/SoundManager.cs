using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SE[] _SEs;
    AudioSource _audioSource;
    public static SoundManager Instance;
    [System.Serializable]
    public struct SE
    {
        public string Name;
        public AudioClip AudioClip;
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Instance = this;
    }
    public void PlaySE(string SEName)
    {
        foreach (var se in _SEs)
        {
            if(se.Name == SEName)
            {
                _audioSource.PlayOneShot(se.AudioClip);
                return;
            }
        }
    }
}
