using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PokemonSpawner)), CanEditMultipleObjects]
    public class PokemonSpawnerEditor : UnityEditor.Editor
    {
        private PokemonSpawner ps;
        public static List<PokemonSpawner.InstanceDetails> instanciated = new List<PokemonSpawner.InstanceDetails>();
        private static List<GameObject> problematics = new List<GameObject>();
        private Vector2 scrollPos;
        
        private void OnEnable()
        {
            ps = ((PokemonSpawner) target);
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(100));
            foreach (var go in problematics)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(go.name);
                if (GUILayout.Button("select", GUILayout.Width(50f)))
                {
                    Selection.activeObject = go;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();


            if (GUILayout.Button("Spawn"))
            {
                ps.StartCoroutine(Spawn());
            }

            if (GUILayout.Button("Find Problematic"))
            {
                problematics = FindProblematic();
            }

            if (GUILayout.Button("Create current pokemons (" + instanciated.Count + ")"))
            {
                Create();
            }

            if (GUILayout.Button("Reset"))
            {
                Reset();
            }

            base.OnInspectorGUI();
        }
        
        
    public IEnumerator<PokemonSpawner> Spawn()
    {
        if (ps.parent == null)
        {
            ps.parent = new GameObject("Pokemons").transform;
        }
        int b = ps.amount;
        while (ps.amount-- > 0)
        {
            if (!spawnOne())
                break;
            yield return null;
        }

        ps.amount = b;
    }

    public List<GameObject> FindProblematic()
    {
        var problems = new List<GameObject>();
        foreach (Transform child in ps.parent)
        {
            if ((child.eulerAngles.x > 5 && child.eulerAngles.x < 355) ||
                (child.eulerAngles.z > 5 && child.eulerAngles.z < 355))
            {
                problems.Add(child.gameObject);
            }
        }
        return problems;
    }

    private bool spawnOne()
    {
        GameObject prefab = ps.prefabs[Random.Range(0, ps.prefabs.Length)];
        PokemonCatchable poke = prefab.GetComponent<PokemonCatchable>();
        Vector2 circleRan;
        Vector3 pos= new Vector3();
        int i = 0;
        do
        {
            if (i > 1000)
            {
                Debug.Log("Stopped spawning pokemons at " + ps.amount + ". not enough space!");
                return false;
            }
            circleRan = ps.spawnRadius * Random.insideUnitCircle;
            pos = new Vector3(circleRan.x + ps.transform.position.x, ps.transform.position.y,
                circleRan.y + ps.transform.position.z);
            i++;
        } while (Physics.CheckSphere(pos, poke.radius+ps.distanceRadius, (1 << 8) + (1 << 9)));
        GameObject one = PrefabUtility.InstantiatePrefab(prefab, ps.parent) as GameObject;
        one.transform.position = pos;
        one.transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
        instanciated.Add(new PokemonSpawner.InstanceDetails(prefab, pos, one.transform.rotation));
        return true;
    }

    
    public void Create()
    {
        foreach (var p in instanciated)
        {
            GameObject one = PrefabUtility.InstantiatePrefab(p.prefab, ps.parent) as GameObject;
            one.transform.position = p.position;
            one.transform.rotation = p.rotation;
        }
    }

    public void Reset()
    {
        instanciated = new List<PokemonSpawner.InstanceDetails>();
    }
    }
}