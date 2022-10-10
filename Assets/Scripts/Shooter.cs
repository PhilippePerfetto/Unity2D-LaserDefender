using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI = false;
    [SerializeField] float firingTimeVariance = 0.6f;
    [SerializeField] float minimumFiringTime = 0.2f;

    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isFiring = useAI;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            var instance = Instantiate(projectilePrefab, 
                                        transform.position,
                                        Quaternion.identity);

            var rigidBody = instance.GetComponent<Rigidbody2D>();
            rigidBody.velocity = transform.up * projectileSpeed;

            Destroy(instance, projectileLifetime);

            float rate = Random.Range(firingRate - firingTimeVariance,
                                          firingRate + firingTimeVariance);

            rate = Mathf.Clamp(firingRate, minimumFiringTime, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(rate);
        }
    }
}
