using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    public int GetHealth() => health;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
       Debug.Log("Health trigger collider between : " +  gameObject.name + " and " + other.name);

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
                TakeDamage(damageDealer.GetDamage());
                PlayHitEffect();
                audioPlayer.PlayDamageClip();
                ShakeCamera();
                damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage({damage}) & health = {health}");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die() {
        Debug.Log("Destroy player game object : " +  gameObject.name);

        if (!isPlayer)
        {
            scoreKeeper.IncreaseScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }

        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            var instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
