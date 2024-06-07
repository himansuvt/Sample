using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    bool isDead;
    bool isSinking = false;
    public AudioClip deathClip;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;

    Slider healthSlider; // Reference to the health bar slider
    Canvas healthCanvas; // Reference to the health bar canvas

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();

        // Find the Canvas and Slider components within the enemy's hierarchy
        healthCanvas = GetComponentInChildren<Canvas>();
        healthSlider = healthCanvas.GetComponentInChildren<Slider>();
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
    }

    void Update()
    {
        // Ensure the health bar always faces the camera
        if (isSinking)
        {
            transform.Translate ( -Vector3.up * sinkSpeed * Time.deltaTime);
        }
        if (healthCanvas != null && Camera.main != null)
        {
            healthCanvas.transform.rotation = Quaternion.LookRotation(healthCanvas.transform.position - Camera.main.transform.position);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        // Update the health bar Slider's value
        if (healthSlider != null)
        {
            healthSlider.value =currentHealth;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        

    }

    public void StartSinking()
    {  isSinking = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
