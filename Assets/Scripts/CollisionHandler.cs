using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Defeat;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning){return;}
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly!");
                break;
            case "Finish":
                Debug.Log("Congrats you finished the level!");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Sorry you blew up!");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        //TODO Add particle effect on finish
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    void StartCrashSequence() 
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Defeat);
        //TODO add Particle Effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdex = currentSceneIndex + 1;
        if (nextSceneIdex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIdex = 0;
        }     
        SceneManager.LoadScene(nextSceneIdex);
    }
}
