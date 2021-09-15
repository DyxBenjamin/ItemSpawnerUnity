using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemSpawner
{
    [CreateAssetMenu(menuName = "Item spawner/Database")]
    public class Database : ScriptableObject
    {

        public List<Item> items = new List<Item>();
        [System.Serializable]
        public class Item
        {
            public int id;
            public string name;
            [TextArea(1,3)]public string description;
            public Object prefab;
            public float probability;
        }
  
        public Item FindItem(int id)
        {
            return items.FirstOrDefault(item => item.id == id);
        }

    }
}

