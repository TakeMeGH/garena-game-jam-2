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
        PlayerB_atk1,
        Base_attacked,
        PopThread,
        FlyingEnemy_atk,
        FlyingEnemy_glide,
        button_click,

    }

    public enum BGM
    {
        MainMenu,
        LoseCond,
        WinCond,
        Level_1,
        Level_2,
        Level_3
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
        [SerializeField] AudioSource Base_attacked;
        [SerializeField] AudioSource PopThread;
        [SerializeField] AudioSource FlyingEnemy_atk;
        [SerializeField] AudioSource FlyingEnemy_glide;
        [SerializeField] AudioSource Button_click;
        #endregion

        #region BGM
        [SerializeField] AudioSource MainMenu;
        [SerializeField] AudioSource LoseCond;
        [SerializeField] AudioSource WinCond;
        [SerializeField] AudioSource Level_1;
        [SerializeField] AudioSource Level_2;
        [SerializeField] AudioSource Level_3;
        #endregion

        void Start()
        {
            LoadMixer();
        }

        void LoadMixer()
        {
            Debug.Log(PlayerPrefs.HasKey(MASTER_VOLUME) + " DEBUG");
            if (!PlayerPrefs.HasKey(MASTER_VOLUME)) return;

            // HotFix
            float volume = PlayerPrefs.GetFloat(MASTER_VOLUME);
            volume = 1;
            _mixer.SetFloat(MASTER_VOLUME, Mathf.Log10(volume) * MULTIPILER);
            PlayerPrefs.SetFloat(MASTER_VOLUME, volume);

            volume = PlayerPrefs.GetFloat(SFX_VOLUME);
            volume = 1;
            _mixer.SetFloat(SFX_VOLUME, Mathf.Log10(volume) * MULTIPILER);
            PlayerPrefs.SetFloat(SFX_VOLUME, volume);

            volume = PlayerPrefs.GetFloat(BGM_VOLUME);
            volume = 1;
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
                case SFX.Base_attacked:
                    Base_attacked.Play();
                    break;
                case SFX.PopThread:
                    PopThread.Play();
                    break;
                case SFX.FlyingEnemy_atk:
                    FlyingEnemy_atk.Play();
                    break;
                case SFX.FlyingEnemy_glide:
                    FlyingEnemy_glide.Play();
                    break;
                case SFX.button_click:
                    Button_click.Play();
                    break;
                default:
                    Debug.LogWarning("Unknown SFX type");
                    break;
            }
        }

        public void PlayBGM(BGM bgmType)
        {
            StopAllBGM();

            switch (bgmType)
            {
                case BGM.MainMenu:
                    MainMenu.Play();
                    break;
                case BGM.LoseCond:
                    LoseCond.Play();
                    break;
                case BGM.WinCond:
                    WinCond.Play();
                    break;
                case BGM.Level_1:
                    Level_1.Play();
                    break;
                case BGM.Level_2:
                    Level_2.Play();
                    break;
                case BGM.Level_3:
                    Level_3.Play();
                    break;
                default:
                    Debug.LogWarning("Unknown BGM type");
                    break;
            }
        }
        private void StopAllBGM()
        {
            MainMenu.Stop();
            LoseCond.Stop();
            WinCond.Stop();
            Level_1.Stop();
            Level_2.Stop();
            Level_3.Stop();
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
