using Library.Interfaces;

namespace Library.Buildings;
//clase base abstracta utilizada para definir los costos y tiempos de construccion de los edificios de almacenamientos
public abstract class Building : IConstructionInfo, IBuildable
{
    //cumple con srp porque solo se encarga de la lógica de los edificios de almacenamiento
    public int WoodCost { get; set; }
    public   int StoneCost { get; set; }
    public int ConstructionTime { get; }
    public int TimeElapsed { get; private set; }
    public int Health { get; set; } // Vida del edificio, por defecto 100

    // Indica si el edificio ya está completamente cdonstruido
    public bool IsBuilt => TimeElapsed >= ConstructionTime;
    public virtual string Symbol { get; set; }

    // Constructor protegido que inicializa los valores principales del edificio
    public Building(int woodCost, int stoneCost, int constructionTime, int health = 100)
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        ConstructionTime = constructionTime;
        TimeElapsed = 0;
        Health = health; 
    }

    // Avanza la construcción del edificio sumando segundos al tiempo transcurrido
    public void Construyendo(int segundos)
    {
        if (IsBuilt || segundos <= 0)
            return;

        TimeElapsed += segundos;
        if (TimeElapsed >= ConstructionTime)
        {
            TimeElapsed = ConstructionTime;
           
        }
    }
}
