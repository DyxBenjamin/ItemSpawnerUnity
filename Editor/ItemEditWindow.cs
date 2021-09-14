using UnityEditor;
using UnityEngine;

namespace ItemSpawner.Editor
{
    public class ItemEditWindow : EditorWindow
    {
        private static EditorWindow window;
        private static Database database;
        private static Database.Item newItem;
        private static Database.Item itemToEdit;
        private GUILayoutOption[] options = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(150.0f) };

        public static void ShowItemWindow(Database db, Database.Item item) {
            database = db;
            window = GetWindow<ItemEditWindow>();
            window.maxSize = new Vector2(300, 350);
            window.minSize = new Vector2(300, 350);
            newItem = new Database.Item();
            newItem.id = item.id;
            newItem.Nombre = item.Nombre;
            newItem.id = item.id;
            newItem.Descripcion = item.Descripcion;
            newItem.prefab = item.prefab;
            newItem.Probabilidad = item.Probabilidad;


        }

        public void OnGUI(){
            DisplayItem(newItem);

            if (GUILayout.Button("Editar")){modifyItem();} //boton editor de item
            if (GUILayout.Button("Cancelar")){window.Close();} //boton cancelar
        }

        //funcion edicion de item
        private void modifyItem() {
            //reasignacion de valores
            Undo.RecordObject(database,"Item Edit");
            itemToEdit = database.FindItem(newItem.id);
            itemToEdit.Nombre = newItem.Nombre;
            itemToEdit.Descripcion = newItem.Descripcion;
            itemToEdit.prefab = newItem.prefab;
            itemToEdit.Probabilidad = newItem.Probabilidad;
            EditorUtility.SetDirty(database);

            window.Close();

        }


        public void DisplayItem(Database.Item item)
        {
            //Definicion de estilo de area de texto
            GUIStyle TextAreaStyle = new GUIStyle(GUI.skin.textArea);TextAreaStyle.wordWrap = true; 

            //Definicion de estilo de etiqueta numerica
            GUIStyle ValueStyle = new GUIStyle(GUI.skin.label);
            ValueStyle.wordWrap = true;
            ValueStyle.alignment = TextAnchor.MiddleLeft;
            ValueStyle.fixedWidth = 50;
            ValueStyle.margin = new RectOffset(0, 50, 0, 0);

            EditorGUILayout.BeginVertical("box");
            //Campo de id
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("id: ");
            GUILayout.Label(item.id.ToString(), ValueStyle);
            EditorGUILayout.EndHorizontal();

            //Campo de nombre
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Nombre: ");
            item.Nombre = GUILayout.TextField(item.Nombre, TextAreaStyle,options);
            EditorGUILayout.EndHorizontal();

            //Campo de Objeto
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab: ");
            item.prefab = EditorGUILayout.ObjectField(item.prefab, typeof(GameObject),false,options);
            EditorGUILayout.EndHorizontal();

            //Campo de descripcion
            GUILayout.Label("Descripcion: ");
            item.Descripcion = EditorGUILayout.TextArea(item.Descripcion, TextAreaStyle, GUILayout.MinHeight(100));

            //Campo de probabilidad
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Probabilidad: ");
            item.Probabilidad = EditorGUILayout.FloatField(item.Probabilidad,TextAreaStyle, options);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

        }

    }
}

