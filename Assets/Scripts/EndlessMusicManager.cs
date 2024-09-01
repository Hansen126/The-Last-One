using UnityEngine;

public class EndlessMusicManager : MonoBehaviour
{
    public EndlessPlayer player;
    public AudioSource[] battleMusic;
    public AudioSource gameOverSFX;
    public AudioSource playerFallSFX;
    public AudioSource knockedSFX;

    private bool knockedSoundPlayed = false;
    private int currentBattleMusicIndex = 0;
    private bool isLastMusicPlaying = false;
    private bool isMusicPlaying = false;
    private bool appHasFocus = true;

    // Start is called before the first frame update
    void Start()
    {
        knockedSFX.loop = false;

        StopAllBattleMusic();

        isMusicPlaying = false;
        isLastMusicPlaying = false;

        if (!player.isDead)
        {
            PlayNextBattleMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            if (!playerFallSFX.isPlaying && !gameOverSFX.isPlaying)
            {
                StopAllBattleMusic();
                playerFallSFX.Play();
                gameOverSFX.Play();
            }

            return;
        }

        if (player.isKnocked && !knockedSFX.isPlaying && !knockedSoundPlayed)
        {
            knockedSFX.Play();
            knockedSoundPlayed = true;
        }

        if (!player.isKnocked)
        {
            knockedSoundPlayed = false;
        }

        if (appHasFocus && !isLastMusicPlaying && isMusicPlaying && !battleMusic[currentBattleMusicIndex - 1].isPlaying)
        {
            isMusicPlaying = false;
            PlayNextBattleMusic();
        }
    }

    void PlayNextBattleMusic()
    {
        if (currentBattleMusicIndex < battleMusic.Length)
        {
            if (currentBattleMusicIndex == battleMusic.Length - 1)
            {
                battleMusic[currentBattleMusicIndex].loop = true;
                isLastMusicPlaying = true;
            }

            battleMusic[currentBattleMusicIndex].Play();
            isMusicPlaying = true;
            currentBattleMusicIndex++;
        }
    }

    void StopAllBattleMusic()
    {
        foreach (AudioSource music in battleMusic)
        {
            music.Stop();
        }

        isMusicPlaying = false;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        appHasFocus = hasFocus;

        if (!hasFocus)
        {
            foreach (AudioSource music in battleMusic)
            {
                if (music.isPlaying)
                {
                    music.Pause();
                }
            }
        }
        else
        {
            foreach (AudioSource music in battleMusic)
            {
                if (!music.isPlaying && isMusicPlaying)
                {
                    music.UnPause();
                }
            }
        }
    }
}
