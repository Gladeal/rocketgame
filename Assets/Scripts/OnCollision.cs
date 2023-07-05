using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    [SerializeField] float reachedEndDelayTime = 2f;
    [SerializeField] float onCrashDelayTime = 1f;

    [SerializeField] AudioClip reachedEndSound;
    [SerializeField] AudioClip onCrashSound;

    [SerializeField] ParticleSystem reachedEndParticles;
    [SerializeField] ParticleSystem onCrashParticles;

    AudioSource audioSource;
    ParticleSystem particleSys;

    bool isTransitioning = false;
    bool disableCollision = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        LoadButton();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || disableCollision) { return; }
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "End":
                    reachedEnd();
                    break;
                default:
                    OnCrash();
                    break;
            }
        }
    }

    void LoadButton()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            disableCollision = !disableCollision;
        }
    }

        void reachedEnd()
        {
            isTransitioning = true;
            audioSource.Stop();
            reachedEndParticles.Play(); //Audio Source gibi deðiþken oluþturarak kullanmadýk çünkü Particle System component deðil obje olarak yüklendi.
            audioSource.PlayOneShot(reachedEndSound);
            transform.GetComponent<PlayerMovement>().enabled = false;
            Invoke("LoadNextScene", reachedEndDelayTime);
        }

        void OnCrash()
        {
            isTransitioning = true;
            audioSource.Stop();
            onCrashParticles.Play(); //Audio Source gibi deðiþken oluþturarak kullanmadýk çünkü Particle System component deðil obje olarak yüklendi.
            audioSource.PlayOneShot(onCrashSound);
            transform.GetComponent<PlayerMovement>().enabled = false;
            Invoke("ReloadLevel", onCrashDelayTime);
        }

        void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

