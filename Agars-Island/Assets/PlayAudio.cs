using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{

    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource source;

    public int audioToPlay = 0;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int audioIndex)
    {
        source.PlayOneShot(audioClips[audioIndex]);
    }
}
