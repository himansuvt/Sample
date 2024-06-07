using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;
    public int currentHealth;
    public float flashSpeed = 5f;
 

    public Image damageImage;
    public Slider healthSlider;
    public AudioClip deathClip;


    Animator animator;
    AudioSource Audio;
    PlayerMovement playerMovement;
    
    bool damaged;
    bool isDead;


    void Awake()
    {
        animator = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startHealth;
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = Color.red;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    void PlayerDeath()
    {
        isDead = true;


        animator.SetTrigger("Death");

        Audio.clip = deathClip;
        Audio.Play();

        playerMovement.enabled = false;
    }
    public void TakeDamage(int damage)
    {
        damaged = true;

        currentHealth -= damage;

        healthSlider.value = currentHealth;

        Audio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            PlayerDeath();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
