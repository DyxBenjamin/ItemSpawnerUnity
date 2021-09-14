using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item spawner/Database")]
public class Database : ScriptableObject
{

    public List<Item> Items = new List<Item>();
    [System.Serializable]
    public class Item
    {
        public int id;
        public string Nombre;
        [TextArea(1,3)]public string Descripcion;
        public Object prefab;
        public float Probabilidad;
    }
  
    public Item FindItem(int id){foreach (Item item in Items){if (item.id == id){return item; }}return null;}

}

