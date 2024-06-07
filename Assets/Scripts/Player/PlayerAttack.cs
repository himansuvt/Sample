using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float timer;
    public int GiveDamage = 25;
   
 
    Ray ray;
    RaycastHit Hit;
    int shootableLayer;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
 


    void Awake()
    {
        gunLight = GetComponent<Light>();
        gunAudio = GetComponent<AudioSource>();
        shootableLayer = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= 0.15f && Time.timeScale != 0)
        {
            BulletFire();
        }

        if (timer >= 0.03f)
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }
    }



    void BulletFire()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out Hit, 100f, shootableLayer))
        {
            EnemyHealth enemyHealth = Hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(GiveDamage, Hit.point);
            }
            gunLine.SetPosition(1, Hit.point);
        }
        else
        {
            gunLine.SetPosition(1, ray.origin + ray.direction * 100f);
        }
    }
}