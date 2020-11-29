using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PokemonCatchable)), CanEditMultipleObjects]
    public class PokemonEditor : UnityEditor.Editor
    {
        private PokemonCatchable poke;
        private void OnEnable()
        {
             poke = (PokemonCatchable) target;
        }

        public override void OnInspectorGUI()
        {
            if (poke == null) return;
            if (GUILayout.Button("Respawn"))
            {
                poke.Respawn();
            }
            base.OnInspectorGUI();
        }
    }
}