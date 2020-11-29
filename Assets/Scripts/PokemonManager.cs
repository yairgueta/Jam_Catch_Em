using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PokemonManager : MonoBehaviour
{
    public static PokemonManager Instance;

    public float respawnDelay = 4.5f;
    public AudioClip successfulCatchClip;
    public ParticleSystem smoke;
    private float respawnTimer = 0;
    private Queue<PokemonCatchable> caughtPokemons;
    private int caughtNumber;
    private PokemonCatchable previousBeingCaught;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        caughtPokemons = new Queue<PokemonCatchable>();
        caughtNumber = 0;
    }

    private void Update()
    {
        if (caughtPokemons.Count > 0)
        {
            respawnTimer += Time.deltaTime;
            if (!(respawnTimer > respawnDelay)) return;
            respawnTimer = 0;
            var toBeRespawned = caughtPokemons.Dequeue();
            toBeRespawned.Respawn();
        }
        else
        {
            respawnTimer = 0;
        }
    }
    
    public void TryToCatchPokemon(GameObject pokemon, bool isBeingCaught)
    {
        if (!isBeingCaught)
        {
            NoneCatching();
            return;
        }
        
        var currentPoke = pokemon.GetComponent<PokemonCatchable>();
        if (previousBeingCaught != null)
        {
            Assert.IsTrue(previousBeingCaught.Equals(currentPoke));
        }
        else
        {
            currentPoke.TryToCatch();
        }

        previousBeingCaught = currentPoke;

    }
    
    public void NoneCatching()
    {
        if (!ReferenceEquals(previousBeingCaught, null))
        {
            previousBeingCaught.ExitCatch();
        }
        previousBeingCaught = null;
    }

    public void CatchPokemon(PokemonCatchable pokemon)
    {
        caughtPokemons.Enqueue(pokemon);
        smoke.gameObject.transform.position = pokemon.gameObject.transform.position;
        smoke.Play();
        
        previousBeingCaught = null;
        UIManager.Instance.SetScore(++caughtNumber);
    }
}
