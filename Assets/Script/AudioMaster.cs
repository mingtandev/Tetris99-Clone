using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{

    public AudioSource audioSrc;
    public AudioSource themeSong;
    public AudioClip rotate, down, fullLine;
    public static float VolumeValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        rotate = Resources.Load<AudioClip>("Music/Rotate");
        down = Resources.Load<AudioClip>("Music/Down");
        fullLine = Resources.Load<AudioClip>("Music/FullLine");
        themeSong.volume = VolumeValue;

    }

    // Update is called once per frame
   public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "Rotate":
                audioSrc.clip = rotate;
                audioSrc.PlayOneShot(rotate, VolumeValue);
                break;
            case "Down":
                audioSrc.clip = down;
                audioSrc.PlayOneShot(down, VolumeValue);

                break;
            case "FullLine":
                audioSrc.clip = fullLine;
                audioSrc.PlayOneShot(fullLine, VolumeValue);

                break;
        }
    }
}
