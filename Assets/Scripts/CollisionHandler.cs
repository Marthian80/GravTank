using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLoad = 1f;
    [SerializeField] ParticleSystem explodeParticle;
    [SerializeField] AudioClip playerExplosion;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + "--collided with--" + collision.gameObject.name);

        switch (collision.gameObject.tag)
        {
            case "Terrain":
                Debug.Log("Collided into the terrain");
                break;
            case "Enemy":
                Debug.Log("Triggered contact with enemy ship");
                break;
            case "Bridge":
                Debug.Log("Triggered contact with bridge");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        explodeParticle.Play();
        audioSource.PlayOneShot(playerExplosion);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", delayLoad);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
