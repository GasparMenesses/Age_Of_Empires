using Library.Interfaces;

namespace Library.Buildings;
//clase base abstracta utilizada para definir los costos y tiempos de construccion de los edificios de almacenamientos
public class Building : IConstructionInfo, IBuildable
{
    //cumple con srp porque solo se encarga de la lógica de los edificios de almacenamiento
    public int WoodCost { get; set; }
    public   int StoneCost { get; set; }
    public int ConstructionTime { get; }

    public int TimeElapsed { get; private set; }

    // Indica si el edificio ya está completamente construido
    public bool IsBuilt => TimeElapsed >= ConstructionTime;
    public virtual string Symbol { get; set; } 
    public Dictionary<string, int> Position { get; set; }

    // Constructor protegido que inicializa los valores principales del edificio
    public Building((int x, int y) position, int woodCost, int stoneCost, int constructionTime)
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        ConstructionTime = constructionTime;
        TimeElapsed = 0;
        Position = new Dictionary<string, int>
        {
            { "x", position.x },
            { "y", position.y }
        };
    }

    // Avanza la construcción del edificio sumando segundos al tiempo transcurrido
    public void Construyendo(int seconds)
    {
        if (!IsBuilt) // Si el edificio no está construido aún, avanzamos con la construcción
        {
            TimeElapsed += seconds; // Se suma el tiempo transcurrido al progreso
            if (TimeElapsed > ConstructionTime)
            {
                // Si el tiempo transcurrido supera el tiempo de construcción, lo ajustamos
                TimeElapsed = ConstructionTime;
            }
        }
    }
}
