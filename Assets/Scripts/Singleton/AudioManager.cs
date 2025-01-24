using UnityEngine;
using UnityEngine.Audio;

namespace TKM
{
    public enum SFX
    {
        Player_missedatk,
        PlayerA_atk1,
        PlayerA_atk2,
        PlayerA_atk3,
        PlayerB_atk1
    }

    public enum BGM
    {
        ChatPopUp
    }

    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] AudioMixer _mixer;
        const string MASTER_VOLUME = "MasterVolume";
        const string SFX_VOLUME = "SFXVolume";
        const string BGM_VOLUME = "BGMVolume";
        const float MULTIPILER = 20;

        #region SFX
        [SerializeField] AudioSource Player_missedatk;
        [SerializeField] AudioSource PlayerA_atk1;
        [SerializeField] AudioSource PlayerA_atk2;
        [SerializeField] AudioSource PlayerA_atk3;
        [SerializeField] AudioSource PlayerB_atk1;
        #endregion

        #region BGM
        // Add your BGM AudioSources if needed
        #endregion

        void Start()
        {
            LoadMixer();
        }

        void LoadMixer()
        {
            if (!PlayerPrefs.HasKey(MASTER_VOLUME)) return;

            float volume = PlayerPrefs.GetFloat(MASTER_VOLUME);
            _mixer.SetFloat(MASTER_VOLUME, Mathf.Log10(volume) * MULTIPILER);
            PlayerPrefs.SetFloat(MASTER_VOLUME, volume);

            volume = PlayerPrefs.GetFloat(SFX_VOLUME);
            _mixer.SetFloat(SFX_VOLUME, Mathf.Log10(volume) * MULTIPILER);
            PlayerPrefs.SetFloat(SFX_VOLUME, volume);

            volume = PlayerPrefs.GetFloat(BGM_VOLUME);
            _mixer.SetFloat(BGM_VOLUME, Mathf.Log10(volume) * MULTIPILER);
            PlayerPrefs.SetFloat(BGM_VOLUME, volume);
        }

        public void PlaySFX(SFX sfxType)
        {
            switch (sfxType)
            {
                case SFX.Player_missedatk:
                    Player_missedatk.Play();
                    break;
                case SFX.PlayerA_atk1:
                    PlayerA_atk1.Play();
                    break;
                case SFX.PlayerA_atk2:
                    PlayerA_atk2.Play();
                    break;
                case SFX.PlayerA_atk3:
                    PlayerA_atk3.Play();
                    break;
                case SFX.PlayerB_atk1:
                    PlayerB_atk1.Play();
                    break;
                default:
                    Debug.LogWarning("Unknown SFX type");
                    break;
            }
        }

        void Update()
        {
            // Map F1-F12 to specific sound effects
            if (Input.GetKeyDown(KeyCode.F1))
            {
                PlaySFX(SFX.Player_missedatk); // Example: Play missed attack on F1
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                PlaySFX(SFX.PlayerA_atk1); // Example: Play Player A attack 1 on F2
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                PlaySFX(SFX.PlayerA_atk2); // Example: Play Player A attack 2 on F3
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                PlaySFX(SFX.PlayerA_atk3); // Example: Play Player A attack 3 on F4
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                PlaySFX(SFX.PlayerB_atk1); // Example: Play Player B attack 1 on F5
            }
            // Add additional mappings for F6-F12 if needed
            else if (Input.GetKeyDown(KeyCode.F6))
            {
                Debug.Log("F6 key pressed"); // Example placeholder
            }
            else if (Input.GetKeyDown(KeyCode.F7))
            {
                Debug.Log("F7 key pressed"); // Example placeholder
            }
            // Continue for F8 to F12 as needed
        }
    }
}
