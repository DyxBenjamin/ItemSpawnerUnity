using UnityEngine;

namespace ItemSpawner
{
    public class ItemSpawn : MonoBehaviour
    {
        public bool enable = true;
        public Transform spawnPoint;
        [Range(1, 100)] public int loopItems;
        [Range(0, 400)] public int maxItems = 0;
        [Range(0, 50)] public float timeBetweenItems = 2;
        [SerializeField] private Database itemsDatabase;

        private float IncrementoDeTiempo = 0,
            TiempoSpawnMinimo = 0.65f,
            timeWait = 1;

        [HideInInspector] public float itemsSpawned;
        [HideInInspector] public float itemsOnScene = 0;


        private double _rnd = 0;
        public int _ranSpawn = 0;
        private double[] _cumulativeProbability;
        private int _itemC = 2;
        private int _sizeDatabase = 0;


        private void Awake()
        {
            //Calculamos el tiempo de lanzamiento. 
            timeWait = timeBetweenItems + Random.Range(0, IncrementoDeTiempo);
        }

        private void Start()
        {
            _sizeDatabase = itemsDatabase.items.Count;
            _cumulativeProbability = new double[_sizeDatabase];
            foreach (var item in itemsDatabase.items)
            {
                _cumulativeProbability[item.id] =  (item.id == 0)  ? item.probability : _cumulativeProbability[item.id - 1] + item.probability;
            }
        }

        private void Update()
        {
            if (timeWait <= 0 && (maxItems == 0 || maxItems > itemsSpawned) && enable == true)
            {
                for (var i = 1; i <= loopItems && (maxItems == 0 || maxItems > itemsSpawned); i++)
                {
                    ItemInstance();
                }

                timeWait = timeBetweenItems;

                if (IncrementoDeTiempo > 0f)
                {
                    timeWait += Random.Range(0, IncrementoDeTiempo);
                }

                if (timeWait < TiempoSpawnMinimo)
                {
                    timeWait = TiempoSpawnMinimo;
                }
            }
            else
            {
                timeWait -= Time.deltaTime;
            }
        }

        // Generar objetos
        private void ItemInstance()
        {
            var itemIndex = 0;
            //Analisis de variable discreta
            _rnd = Random.Range(0, (float) _cumulativeProbability[_sizeDatabase - 1]);
            for (var j = 0; j <= _sizeDatabase - 1; j++)
            {
                if (!(_rnd < _cumulativeProbability[j])) continue;
                itemIndex = j;
                break;
            }

            //Seleccion de un punto de spawn

            Instantiate(itemsDatabase.FindItem(itemIndex).prefab, spawnPoint.position, Quaternion.identity);
            itemsSpawned += 1;
            itemsOnScene += 1;
        } //instanciar un objeto
    }
}