using UnityEditor;
using UnityEngine;

namespace ItemSpawner.Editor
{
    [CustomEditor(typeof(MobSpawn))]
    public class ItemSpawnInspector : UnityEditor.Editor
    {
        private MobSpawn _itemSpawn;

        private void OnEnable()
        {_itemSpawn = (MobSpawn)target;} //selecciona la database actual

        public override void OnInspectorGUI()
        {
            //base.DrawDefaultInspector(); dibujo de inspector por default
            if (_itemSpawn)
            {

                EditorGUILayout.BeginVertical("box");
                GUILayout.Label("Mobs creados: " + _itemSpawn.MobsSpawned); //Conteo de Mobs creados
                GUILayout.Label("Mobs en escena: " + _itemSpawn.MobsEnEscena);  //Conteo de Mobs en escena
                EditorGUILayout.EndVertical();

                base.DrawDefaultInspector();
            }
        }

        public void DisplayItem(Database.Item item){

            //Definicion de estilo de etiqueta
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.wordWrap = true;

            //Definicion de estilo de etiqueta numerica
            GUIStyle ValueStyle = new GUIStyle(GUI.skin.label);
            ValueStyle.wordWrap = true;
            ValueStyle.alignment = TextAnchor.MiddleLeft;
            ValueStyle.fixedWidth = 50;
            ValueStyle.margin = new RectOffset(0 ,50 ,0 ,0);

        }
    }
}
