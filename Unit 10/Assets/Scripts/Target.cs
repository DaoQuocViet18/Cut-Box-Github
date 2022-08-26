using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 10f;
    private float maxSpeed = 14f;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -0.5f;

    private GameManager gameManager;
    public int pointValue;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomForce ()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque ()
    {
        return Random.Range(-maxTorque, maxTorque);
    } 

    private Vector3 RandomSpawnPos ()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive && !gameManager.paused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int minusLive = 1;
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad") && gameManager.live <= 0)
        {
            gameManager.GameOver();
        }
        else if(!gameObject.CompareTag("Bad"))
        {
            gameManager.Live(minusLive);
        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
}
