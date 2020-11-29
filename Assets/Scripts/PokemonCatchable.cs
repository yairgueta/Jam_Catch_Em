using System.Collections;
using UnityEngine;

public class PokemonCatchable : MonoBehaviour
{
    public Material[] catchingMaterials;
    public float timeToCatch = 2f;
    public float radius = 1f;
    public float disappearingDuration = 2f;
    public bool IsBeingCaught => isBeingCaught;

    public bool IsCaught => isCaught;

    private bool isBeingCaught;
    private bool isCaught;
    private float timer;
    private Renderer meshRenderer;
    private Material[] originalMaterials;
    private int dissolveEffectID;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        dissolveEffectID = Shader.PropertyToID("Vector1_FEFF47F1");
        meshRenderer = GetComponentInChildren<Renderer>();
        originalMaterials = meshRenderer.materials;
        timer = 0;
        isBeingCaught = false;
        isCaught = false;
    }

    public void Respawn()
    {
        // gameObject.SetActive(true);
        // float r, a;
        // Vector3 newPos = new Vector3(-1,-1,-1);
        // int i = 0;
        // do
        // {
        //     if (i > 500) break;  // try to avoid collisions with other up to 500 times...
        //     r = 5;
        //     a = Random.RangColor(32,0.376470596,0.125490203,0)e(0, 2 * Mathf.PI);
        //     newPos = new Vector3(Mathf.Cos(a) * r, height, Mathf.Sin(a) * r);
        //     i++;
        // } while (Physics.CheckSphere(newPos, radius, 1 << 8));
        //
        // gameObject.transform.position = newPos;
        // gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(135f, 290f), 0);
        // isBeingCaught = false;
        // meshRenderer.materials = originalMaterials;
    }

    private void Update()
    {
        if (!isBeingCaught)
        {
            timer = 0;
            return;
        }
        timer += Time.deltaTime;
        
        if (timer > timeToCatch && !IsCaught)
            StartCoroutine(Disappear());
    }

    public void TryToCatch()
    {
        isBeingCaught = true;
        meshRenderer.materials = catchingMaterials;
    }

    public void ExitCatch()
    {
        isBeingCaught = false;
        meshRenderer.materials = originalMaterials;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public IEnumerator Disappear()
    {
        if (!isBeingCaught)
        {
            Debug.LogWarning("Tried to vanish a pokemon that is not being caught right now: "+ gameObject.name);
            yield break;
        }

        isCaught = true;
        audioSource.PlayOneShot(PokemonManager.Instance.successfulCatchClip);
        PokemonManager.Instance.CatchPokemon(this);

        float timePassed = 0;
        while (timePassed < disappearingDuration)
        {
            meshRenderer.materials = catchingMaterials;
            foreach (var mat in meshRenderer.materials)
                mat.SetFloat(dissolveEffectID, Mathf.Lerp(0, 1, (timePassed / disappearingDuration)));
            timePassed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
