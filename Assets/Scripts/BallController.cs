using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    public float speedOnCatching = 2f;
    public GameObject catchingEffect;
    public AudioClip catchingSound;
    
    private Rigidbody rb;
    private bool inCatchMode;
    private Animator animator;
    private int animatorCatchModeID;
    private Coroutine openingBallCoroutine;
    private AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        catchingEffect.SetActive(false);
        inCatchMode = false;
        animator = GetComponentInChildren<Animator>();
        animatorCatchModeID = Animator.StringToHash("InCatchMode");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = catchingSound;
        audioSource.loop = true;
        audioSource.Stop();
    }

    void FixedUpdate()
    {
        float currentSpeed = inCatchMode ? speedOnCatching : speed;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddRelativeForce(Vector3.right * currentSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * currentSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.forward * currentSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.back * currentSpeed);
        }
        rb.AddForce(GameManager.Instance.GetArenaForce(transform.position));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            openingBallCoroutine = StartCoroutine(EnterCatchMode());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ExitCatchMode();
        }
        
        
    }

    private IEnumerator EnterCatchMode()
    {
        animator.SetBool(animatorCatchModeID, true);
        yield return new WaitForSeconds(.2f);
        inCatchMode = true;
        catchingEffect.SetActive(inCatchMode);
        audioSource.Play();
    }
    
    private void ExitCatchMode()
    {
        inCatchMode = false;
        animator.SetBool(animatorCatchModeID, false);
        catchingEffect.SetActive(false);
        StopCoroutine(openingBallCoroutine);
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pokemon"))
        {
            PokemonManager.Instance.TryToCatchPokemon(other.gameObject, inCatchMode);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pokemon"))
        {
            PokemonManager.Instance.TryToCatchPokemon(other.gameObject, inCatchMode);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PokemonManager.Instance.NoneCatching();
    }
}
