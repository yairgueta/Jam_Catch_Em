using System.Collections;
using UnityEngine;
public class BlinkingObject : MonoBehaviour
{
    public Material[] materials;
    public int eyeMaterialIndex;
    public float frameDelay = 63;
    public Vector2 range = new Vector2(2, 8);
    public PokemonCatchable target;
    [SerializeField] private float counter;
    private Renderer mr;
    private Material[] currentMaterials;

    // Start is called before the first frame update
    private void Start()
    {
        mr = GetComponent<Renderer>();
        currentMaterials = mr.materials;
        counter = Random.Range(range.x, range.y);
    }

    // Update is called once per frame
    private void Update()
    {
        counter -= Time.deltaTime;
        if (counter > 0) return;
        
        counter = Random.Range(range.x, range.y);
        if (!target.IsBeingCaught && !target.IsCaught) Blink();
    }

    public void Blink()
    {
        StartCoroutine(_blink());
    }
    
    private IEnumerator _blink()
    {
        foreach (var materialState in materials)
        {
            yield return new WaitForSeconds(frameDelay / 1000);
            if (target.IsBeingCaught) break;
            currentMaterials[eyeMaterialIndex] = materialState;
            mr.materials = currentMaterials;
        }
    }
}
