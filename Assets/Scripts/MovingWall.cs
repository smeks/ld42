using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    
    public List<AudioClip> WallMoveSounds;

    private AudioSource _audioSource;

    // Use this for initialization
    void Start ()
    {
        _audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_audioSource.isPlaying)
	        return;

	    var randomMoveSound = Random.Range(0, WallMoveSounds.Count);

	    _audioSource.clip = WallMoveSounds[randomMoveSound];
	    _audioSource.Play(2);
    }
}
