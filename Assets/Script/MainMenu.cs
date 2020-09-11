using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


    public void OptionsMenu()
    {

    }


    public void ChangeVolume(Slider slider)
    {
        AudioMaster.VolumeValue = slider.value;
    }


}
