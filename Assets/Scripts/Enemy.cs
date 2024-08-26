using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float fadeDuration = 1f;
    private Material enemyMaterial;

    private Animator anim;
    private Renderer enemyRenderer;
    public float moveSpeed;
    public string enemyName;

    private bool isAttack;
    private bool isDead;
    public Player mainCharacter;
    private bool isFacingRight = true;

    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyRenderer = GetComponent<Renderer>();
        enemyMaterial = enemyRenderer.material;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            mainCharacter = playerObject.GetComponent<Player>();
            Debug.Log("Player object found: " + playerObject.name);
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player object has the tag 'Player'.");
        }
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !isDead)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        checkForFlip();
        checkLayer();
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = enemyMaterial.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration); // Lerp alpha dari 0 ke 1
            enemyMaterial.color = new Color(color.r, color.g, color.b, alpha); // Set alpha material
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemyMaterial.color = new Color(color.r, color.g, color.b, 1); // Pastikan alpha benar-benar 1 di akhir
    }

    private void checkLayer()
    {
        if (player != null)
        {
            if (transform.position.y < player.position.y)
            {
                enemyRenderer.sortingLayerName = "EnemyBawah";
            }
            else
            {
                enemyRenderer.sortingLayerName = "EnemyAtas";
            }
        }
    }

    private void checkForFlip()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            if (direction.x < 0 && isFacingRight == true)
            {
                flip();
                isFacingRight = false;
            }
            else if (direction.x > 0 && isFacingRight == false)
            {
                flip();
                isFacingRight = true;
            }
        }
    }

    public void setTarget(Transform target)
    {
        this.player = target;
    }

    public string getName()
    {
        return enemyName;
    }


    public void die()
    {
        isDead = true;
        moveSpeed = 0;
        anim.SetBool("isDead", isDead);
        Invoke("destroyEnemy", 3f);
    }

    public void destroyEnemy()
    {
        Debug.Log("Enemy " + enemyName + " destroyed.");
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            moveSpeed = 0;
            isAttack = true;
            anim.SetBool("isAttack", isAttack);
            if (isDead)
            {
                return;
            }
            else
            {
                mainCharacter.knocked();
                Debug.Log("player Knocked");
            }
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}