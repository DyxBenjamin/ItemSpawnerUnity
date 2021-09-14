using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace ItemSpawner.Editor
{
    [CustomEditor(typeof(Database))]
    public class DatabaseInspector : UnityEditor.Editor
    {
        private Database _dataBase;
        private string _searchString = null;
        private bool _shouldSearch;

        private void OnEnable()
        {_dataBase = (Database)target;} //selecciona la database actual

        public override void OnInspectorGUI()
        {
            //base.DrawDefaultInspector(); dibujo de inspector por default
            if (_dataBase)
            {
                //Conteo de items
                EditorGUILayout.BeginHorizontal("box");
                GUILayout.Label("Items guardados: " + _dataBase.Items.Count);
                EditorGUILayout.EndHorizontal();

                //Caja de busqueda
                if (_dataBase.Items.Count > 0) {
                    EditorGUILayout.BeginHorizontal("box");
                    GUILayout.Label("Search: "); _searchString = GUILayout.TextField(_searchString);
                    EditorGUILayout.EndHorizontal(); }

                if (GUILayout.Button("Nuevo item")){ItemWindow.ShowEmptyWindow(_dataBase); //boton de nuevo item
                }

                //busqueda de items
                if (System.String.IsNullOrEmpty(_searchString) == true)
                {_shouldSearch = false;}else {_shouldSearch = true;}

                foreach (Database.Item item in _dataBase.Items)
                {if (_shouldSearch == true){
                        if (item.Nombre == _searchString || item.Nombre.Contains(_searchString) || item.id.ToString() == _searchString)
                        { DisplayItem(item);}}
                    else{DisplayItem(item);}}

                if (_deletedItem != null) {_dataBase.Items.Remove(_deletedItem);} //Eliminar item
            }
        }

        private Database.Item _deletedItem;

        public void DisplayItem(Database.Item item){
            
            //Definicion de estilo de etiqueta
            GUIStyle tagStyle = new GUIStyle(GUI.skin.label);
            tagStyle.wordWrap = true;
            tagStyle.alignment = TextAnchor.MiddleLeft;
            tagStyle.fixedWidth = 100;

            //Definicion de estilo de etiqueta
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.wordWrap = true;
            labelStyle.alignment = TextAnchor.MiddleLeft;
         
            //Definicion de estilo de etiqueta de referencias
            GUIStyle valueStyle = new GUIStyle(GUI.skin.label);
            valueStyle.wordWrap = true;
            valueStyle.alignment = TextAnchor.MiddleLeft;
            //valueStyle.margin = new RectOffset(0 ,50 ,0 ,0);
            
            //Definicion de estilo de botones
            GUIStyle butonStyle = new GUIStyle(GUI.skin.button);
            butonStyle.wordWrap = true;
            butonStyle.alignment = TextAnchor.MiddleCenter;
            butonStyle.margin = new RectOffset(0 ,0 ,5 ,5);
            EditorGUILayout.BeginVertical("box"); //Organizacion Vertical, "Box" dibuja una caja

            //Campo de id
            EditorGUILayout.BeginHorizontal();//Organizacion Horizontal
            GUILayout.Label("id: ", tagStyle); GUILayout.Label(item.id.ToString(), valueStyle);
            EditorGUILayout.EndHorizontal();

            //Campo de nombre
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Nombre: ", tagStyle); GUILayout.Label(item.Nombre, labelStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        
            //Campo de descripcion
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Descripcion: ", tagStyle); GUILayout.Label(item.Descripcion, labelStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        
            // Campo de objeto
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab: ", tagStyle); GUILayout.Label(item.prefab.ToString(), labelStyle);
            EditorGUILayout.EndHorizontal(); 
        
            //Campo de probabilidad
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Probabilidad: ", tagStyle); GUILayout.Label(item.Probabilidad.ToString(CultureInfo.CurrentCulture), valueStyle);
            EditorGUILayout.EndHorizontal();
        
            EditorGUILayout.Space();
        
            //Botones de accion
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Editar", butonStyle)){ItemEditWindow.ShowItemWindow(_dataBase, item);} //Boton de edicion  
            if (GUILayout.Button("Borrar", butonStyle)){_deletedItem = item;} //Boton de borrado
            else {_deletedItem = null;}
            if (GUILayout.Button("Duplicar", butonStyle)){ItemEditWindow.ShowItemWindow(_dataBase, item);}
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();

        }
    }
}
