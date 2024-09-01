using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer darkBackgroundSprite;

    public TMP_Text skillCooldownText;
    [SerializeField] private bool canUseSkill = true;
    [SerializeField] private float skillCooldown;
    [SerializeField] private float timerCooldown;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] private PauseMenu pause;
    private bool isPaused;

    private int kill;
    public TMP_Text kills;
    private Animator anim;
    private SpriteRenderer sr;

    public SpriteRenderer healthbarRenderer;
    public Sprite[] healthSprites;

    [SerializeField] private int maxHealth;
    [SerializeField] private int healthRemaining;
    
    public float timeSurvived;
    public TMP_Text timer;
    public bool isKnocked;
    private bool canBeKnocked = true;
    public bool isDead;

    public bool isAttack;

    public AudioSource attackAudioSource;
    public AudioClip[] attackSound;

    public AudioSource typingAudioSource;
    public AudioClip[] typingSound;
    public AudioClip backspaceSound;

    public float attackSoundDelay = 0.2f;

    public EnemySpawner spawnEnemy;
    private List<Enemy> enemies = new List<Enemy>();
    private string currentInput = "";

    // Start is called before the first frame update
    void Start()
    {
        if (attackAudioSource == null)
        {
            attackAudioSource = GetComponent<AudioSource>();
        }
        timeSurvived = 0;
        kill = 0;
        timer.text = "Time survived: " + timeSurvived;
        kills.text = "Kills: " + kill;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        healthRemaining = maxHealth;
        timerCooldown = skillCooldown;
        Time.timeScale = 1;
            
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = pauseMenu.activeSelf;

        checkHealthBar();

        animationController();

        if (isDead)
            return;
        
        checkCooldown();

        if(healthRemaining <= 0)
        {
            isDead = true;
        }

        if(isDead && canBeKnocked)
        {
            die();
        }

        if (Input.anyKeyDown)
        {
            if (Input.inputString.Length > 0 && char.IsLetter(Input.inputString[0]))
            {
                PlayTypingSound();
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                PlayBackspaceSound();
            }
        }

        timeSurvived += Time.deltaTime;
        updateScore();

        checkInput();
        checkVictory();

    }

    private void checkVictory()
    {
        if(timeSurvived >= 300)
        {
            pause.Victory();
        }
    }

    private IEnumerator DarkenBackground()
    {
        if (darkBackgroundSprite != null)
        {
            Color darkColor = new Color(110 / 255f, 97 / 255f, 138 / 255f, 1);
            Color originalColor = darkBackgroundSprite.color;

            float fadeInDuration = 0.3f;
            float elapsedTimeFadeIn = 0f;


            while (elapsedTimeFadeIn < fadeInDuration)
            {
                elapsedTimeFadeIn += Time.deltaTime;
                float t = elapsedTimeFadeIn / fadeInDuration;

                darkBackgroundSprite.color = Color.Lerp(originalColor, darkColor, t);
                yield return null;
            }


            

            yield return new WaitForSeconds(0.35f);

            float fadeOutDuration = 0.2f;
            float elapsedTimeFadeOut = 0f;

            while (elapsedTimeFadeOut < fadeOutDuration)
            {
                elapsedTimeFadeOut += Time.deltaTime;
                float t = elapsedTimeFadeOut / fadeOutDuration;

                darkBackgroundSprite.color = Color.Lerp(darkColor, originalColor, t);
                yield return null;
            }

            darkBackgroundSprite.color = originalColor;
        }
    }

    public void useSkill()
    {
        canUseSkill = false;
        timerCooldown = skillCooldown;
        List<Enemy> enemiesToRemove = new List<Enemy>();

        for (int i = 0; i < enemies.Count; i++)
        {
            canUseSkill = false;
            isAttack = true;
            isKnocked = false;
            enemiesToRemove.Add(enemies[i]);
            kill += 1;
            Debug.Log("Total Kill: " + kill);
        }

        foreach (Enemy enemy in enemiesToRemove)
        {
            enemy.die();
            enemies.Remove(enemy);
        }
        StartCoroutine(PlayAttackSound());
        StartCoroutine(DarkenBackground());
        currentInput = "";
    }

    private void checkCooldown()
    {
        timerCooldown -= Time.deltaTime;
        if (timerCooldown <= 0 && canUseSkill == false)
        {
            skillCooldownText.text = "Skill ready: type \"abyss\"";
            canUseSkill = true;
        }
        else
        {
            if(canUseSkill == true)
            {
                skillCooldownText.text = "Skill ready: type \"abyss\"";
            }
            else
            {
                skillCooldownText.text = "Cooldown: " + timerCooldown.ToString("F1");
            }
        }
    }

    private void updateScore()
    {
        timer.text = "Time survived: " + timeSurvived.ToString("F1") + " / 300";
        kills.text = "Kills: " + kill;
    }

    private void checkHealthBar()
    {
        for(int i = 0; i < 5; i++)
        {
            if(i == healthRemaining)
            {
                healthbarRenderer.sprite = healthSprites[i];
            }
            else if(isDead == true)
            {
                healthbarRenderer.sprite = healthSprites[0];
            }
        }
    }

    private void CheckEnemyName(string input)
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();

        for (int i = 0; i < enemies.Count; i++)
        {

            if (enemies[i].enemyName.Equals(input, System.StringComparison.OrdinalIgnoreCase))
            {
                isAttack = true;
                isKnocked = false;
                enemiesToRemove.Add(enemies[i]);
                kill += 1;
                StartCoroutine(PlayAttackSound());
                Debug.Log("Total Kill: " + kill);
            }
        }

        foreach (Enemy enemy in enemiesToRemove)
        {
            enemy.die();
            enemies.Remove(enemy);
        }
    }


    public void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void knocked()
    {
        if(!canBeKnocked)
            return;

        healthRemaining -= 1;
        StartCoroutine(Invicibility());
        isKnocked = true;

    }

    private void die()
    {
        isKnocked = false;
        isDead = true;
        canBeKnocked = false;
        anim.SetBool("isDead", true);
        Debug.Log("Your Score is: " + timeSurvived); 
    }

    private IEnumerator Invicibility()
    {
        if (healthRemaining > 0)
        {
            Color originalColor = sr.color;
            Color darkenColor = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);

            canBeKnocked = false;

            sr.color = darkenColor;
            yield return new WaitForSeconds(.1f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.1f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.15f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.15f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.25f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.25f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.3f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.35f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.4f);

            sr.color = originalColor;

            canBeKnocked = true;

        }

    }

    private void cancelKnockback() => isKnocked = false;

    private void animationController()
    {
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isDead", isDead);
        anim.SetBool("isAttack", isAttack);
    }

    private void setGameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    private void checkInput()

    {
        if(isDead == true)
        {
            Invoke("setGameOver", 3f);
            return;
        }

        if (isPaused)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause.Pause();
        }

        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                if (currentInput.Length != 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    Debug.Log("Current input: " + currentInput);
                }
            }
            else if (c == '\n' || c == '\r')
            {
                if (currentInput.ToLower() == "abyss" && canUseSkill == true)
                {
                    useSkill();
                }
                else
                {
                    Debug.Log("Checking enemy name: " + currentInput);
                    CheckEnemyName(currentInput);
                    currentInput = "";
                }
            }
           
            else
            {
                currentInput += c;
                Debug.Log("Current input: " + currentInput);
            }
        }
    }

    private void readyToAttack()
    {
        isAttack = false;
    }

    private IEnumerator PlayAttackSound()
    {
        yield return new WaitForSeconds(attackSoundDelay);

        if (attackAudioSource != null && attackSound != null && attackSound.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, attackSound.Length);
            AudioClip chosenClip = attackSound[randomIndex];

            attackAudioSource.clip = chosenClip;
            attackAudioSource.Play();
        }
    }

    private void PlayTypingSound()
    {
        int randomIndex2 = UnityEngine.Random.Range(0, typingSound.Length);
        AudioClip chosenClip2 = typingSound[randomIndex2];

        typingAudioSource.PlayOneShot(chosenClip2);
    }

    private void PlayBackspaceSound()
    {
        typingAudioSource.PlayOneShot(backspaceSound);
    }
}
