using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] AudioSource[] tracks;
    int trackIndex;
    // Start is called before the first frame update
    void Start()
    {
        trackIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNextTrack()
    {
        tracks[trackIndex].Play();
        trackIndex++;

        if (trackIndex >= tracks.Length) { trackIndex = 0; }
    }
}
