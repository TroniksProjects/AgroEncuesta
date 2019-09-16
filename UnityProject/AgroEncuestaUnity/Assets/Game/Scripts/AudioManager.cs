using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioSource LobbyBGM;

    [SerializeField]
    private AudioSource LobbyGameMusic;

    [SerializeField]
    private AudioSource WinBGM;

    [SerializeField]
    private AudioSource LoseBGM;

    [SerializeField]
    private AudioSource UICLick;

    private void Awake()
    {
        Instance = this;
        LobbyBGM.volume = 0.5f;
        LobbyGameMusic.volume = 0.5f;
    }

    public void PlayLobbyMusic()
    {
        LobbyBGM.Play();
        LeanTween.value(gameObject, 0f, 0.5f, 1.5f).setOnUpdate((float val) =>
        {
            LobbyBGM.volume = val;
        });
    }

    public void PlayGameMusic()
    {
        LobbyGameMusic.Play();
        LeanTween.value(gameObject, 0f, 0.5f, 1.5f).setOnUpdate((float val) =>
        {
            LobbyGameMusic.volume = val;
        });

        LeanTween.value(gameObject, 0.5f, 0.0f, 1.5f).setOnUpdate((float val) =>
        {
            LobbyBGM.volume = val;
        }).setOnComplete(() => { LobbyBGM.Stop(); });
    }

    public void StopGameMusic()
    {
        LeanTween.value(gameObject, 0.5f, 0.0f, 1.5f).setOnUpdate((float val) =>
        {
            LobbyGameMusic.volume = val;
        }).setOnComplete(() => { LobbyGameMusic.Stop(); });
    }

    public void PlayWinMusic ()
    {
        WinBGM.Play();
        LeanTween.value(gameObject, 0f, 0.5f, 1.5f).setOnUpdate((float val) =>
        {
            WinBGM.volume = val;
        });
    }

    public void PlayLoseMusic()
    {
        LoseBGM.Play();
        LeanTween.value(gameObject, 0f, 0.5f, 1.5f).setOnUpdate((float val) =>
        {
            LoseBGM.volume = val;
        });
    }

    public void StopWinMusic ()
    {
        LeanTween.value(gameObject, 0.5f, 0.0f, 1.5f).setOnUpdate((float val) =>
        {
            WinBGM.volume = val;
        }).setOnComplete(() => { WinBGM.Stop(); });
    }

    public void StopLoseMusic()
    {
        LeanTween.value(gameObject, 0.5f, 0.0f, 1.5f).setOnUpdate((float val) =>
        {
            LoseBGM.volume = val;
        }).setOnComplete(() => { LoseBGM.Stop(); });
    }

    public void PlayUIButton()
    {
        UICLick.Play();
    }
}