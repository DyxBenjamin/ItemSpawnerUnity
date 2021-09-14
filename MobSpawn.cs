using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public bool Activo = true;
    public Transform[] Ubicaciones;
    [Range(1, 100)] public int ItemsPorCiclo;
    [Range(0, 400)] public int ItemsMaximos = 0;
    [Range(0, 50)] public float TiempoEntreItems = 2;
    [SerializeField] private Database MobsDatabase;

    private float IncrementoDeTiempo = 0,
                  TiempoSpawnMinimo = 0.65f,
                  timeWaitNew = 1;
    [HideInInspector]public float MobsSpawned = 0;
    [HideInInspector] public float MobsEnEscena = 0;


    private double rnd = 0;
    private int ranSpawn = 0;
    private double[] Acumulado;
    private int itemC = 2;
    private int tamaño = 0;


    private void Awake()
    {
        //Calculamos el tiempo de lanzamiento. 
        timeWaitNew = TiempoEntreItems + Random.Range(0, IncrementoDeTiempo);
    }

    void Start()
    {
        tamaño = MobsDatabase.Items.Count ;
        Acumulado = new double[tamaño];
       foreach (Database.Item Enemigo in MobsDatabase.Items) { if (Enemigo.id == 0) { Acumulado[Enemigo.id] = Enemigo.Probabilidad; } else { Acumulado[Enemigo.id] = Acumulado[Enemigo.id - 1] + Enemigo.Probabilidad; } }
    
    }

    void Update()
    {
        if (timeWaitNew <= 0 && (ItemsMaximos == 0 || ItemsMaximos > MobsSpawned) && Activo == true)
        {
            for (int i = 1; i <= ItemsPorCiclo && (ItemsMaximos == 0 || ItemsMaximos > MobsSpawned); i++) { lanza(); }

          //  if (IncreaseSpawn >= 0 && TiempoEntreItems > TiempoSpawnMinimo) { TiempoEntreItems -= ItemsPorCiclo; }

            timeWaitNew = TiempoEntreItems;

            if (IncrementoDeTiempo > 0f) { timeWaitNew += Random.Range(0, IncrementoDeTiempo); }

            if (timeWaitNew < TiempoSpawnMinimo) { timeWaitNew = TiempoSpawnMinimo; }

        }
        else { timeWaitNew -= Time.deltaTime; }
    }

    // Generar objetos
    void lanza()
    {
        //Analisis de variable discreta
        rnd = Random.Range(0, (float)Acumulado[tamaño - 1]);
        for (int j = 0; j <= tamaño - 1; j++) { if (rnd < Acumulado[j]) { itemC = j; break; } }

        ranSpawn = Random.Range(0, Ubicaciones.Length); //Seleccion de un punto de spawn

        Instantiate(MobsDatabase.FindItem(itemC).prefab, Ubicaciones[ranSpawn].position, Quaternion.identity); MobsSpawned += 1; MobsEnEscena += 1;
    } //instanciar un objeto
}




