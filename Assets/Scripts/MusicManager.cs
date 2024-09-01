using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Player player;
    public AudioSource battleMusic;
    public AudioSource gameOverSFX;
    public AudioSource playerFallSFX;
    public AudioSource knockedSFX;
    public AudioSource victoryMusic;

    public GameObject victoryPanel;

    private bool knockedSoundPlayed = false;
    private bool victoryMusicPlayed = false;

    void Start()
    {
        knockedSFX.loop = false;

        if (!player.isDead)
        {
            battleMusic.Play();
        }
    }

    void Update()
    {
        if (player.isDead && !playerFallSFX.isPlaying && !gameOverSFX.isPlaying)
        {
            battleMusic.Stop();
            playerFallSFX.Play();
            gameOverSFX.Play();
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

        if (victoryPanel.activeSelf && !victoryMusicPlayed)
        {
            Debug.Log("Victory panel is active. Playing victory music.");
            battleMusic.Stop();
            victoryMusic.Play();
            victoryMusicPlayed = true;
        }
    }
}
