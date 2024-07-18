using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip failSound;
    [SerializeField] AudioClip finishSound;

    [SerializeField] ParticleSystem failParticle;
    [SerializeField] ParticleSystem finishParticle;


    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly");
                    break;
                case "Finish":
                    StartFinishSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(failSound);
        failParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delayTime);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;     
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
