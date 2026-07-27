using UnityEngine;
using UnityEngine.UI;



public class SettingsManager : MonoBehaviour
{

    public static SettingsManager Instance { get; private set; }



    [Header("Slider")]

    [SerializeField]
    private Slider musicSlider;


    [SerializeField]
    private Slider sfxSlider;




    [Header("Settings Panel")]

    [SerializeField]
    private GameObject settingsPanel;




    private float musicVolume;
    private float sfxVolume;







    private void Awake()
    {

        if (Instance != null &&
           Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;

    }









    private void Start()
    {

        LoadSettings();

    }









    // =================================
    // LOAD SETTINGS
    // =================================


    private void LoadSettings()
    {

        if (SaveManager.Instance == null)
            return;



        musicVolume =
            SaveManager.Instance
            .GetBGMVolume();



        sfxVolume =
            SaveManager.Instance
            .GetSFXVolume();





        if (musicSlider != null)
        {

            musicSlider.value =
                musicVolume;


            musicSlider
            .onValueChanged
            .AddListener(
                SetMusicVolume
            );

        }






        if (sfxSlider != null)
        {

            sfxSlider.value =
                sfxVolume;


            sfxSlider
            .onValueChanged
            .AddListener(
                SetSFXVolume
            );

        }


    }









    // =================================
    // SLIDER
    // =================================



    public void SetMusicVolume(
        float value
    )
    {

        musicVolume = value;


        if (AudioManager.Instance != null)
        {
            AudioManager.Instance
            .SetMusicVolume(
                value
            );
        }

    }








    public void SetSFXVolume(
        float value
    )
    {

        sfxVolume = value;


        if (AudioManager.Instance != null)
        {
            AudioManager.Instance
            .SetSFXVolume(
                value
            );
        }

    }









    // =================================
    // APPLY BUTTON
    // =================================



    public void ApplySettings()
    {

        if (SaveManager.Instance == null)
            return;




        SaveManager.Instance
        .SaveBGMVolume(
            musicVolume
        );



        SaveManager.Instance
        .SaveSFXVolume(
            sfxVolume
        );



        Debug.Log(
            "Settings Saved"
        );

    }









    // =================================
    // RESET SAVE
    // =================================



    public void ResetSave()
    {

        if (SaveManager.Instance != null)
        {

            SaveManager.Instance
            .ResetProgress();


            Debug.Log(
                "Save berhasil direset"
            );

        }

    }









    // =================================
    // BACK BUTTON
    // =================================



    public void BackToMenu()
    {

        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
            .LoadScene(
                "MainMenu"
            );

        }

    }








    // =================================
    // CLOSE PANEL
    // =================================



    public void CloseSettings()
    {

        if (settingsPanel != null)
        {
            settingsPanel
            .SetActive(false);
        }

    }






    public void OpenSettings()
    {

        if (settingsPanel != null)
        {
            settingsPanel
            .SetActive(true);
        }

    }


}