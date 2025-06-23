using Library.Interfaces;

namespace Library.Buildings;
//clase base abstracta utilizada para definir los costos y tiempos de construccion de los edificios de almacenamientos
public abstract class Building : IConstructionInfo , IBuildable
{
    public int WoodCost { get; set; }
    public int StoneCost { get; set; }
    public int ConstructionTime { get; private set; }
    public int TimeElapsed { get; private set; }
    public bool IsBuilt => TimeElapsed >= ConstructionTime;
    public string Symbol { get; set; }
    public Dictionary<string, int> Position { get; set; }
    
    protected Building((int x, int y)position, int woodCost, int stoneCost,int constructionTime)
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

    public void Construyendo(int seconds)
    {
        if (!IsBuilt) // si el edificio no esta construido aún avanzamos con la construcción
        {
            TimeElapsed += seconds; // se suma el tiempo transcurrido al progreso, si es que hay
            if (TimeElapsed> ConstructionTime)
            {                                       // si el tiempo transcurrido pasa al tiempo de construcción, lo ajustamos 
                                                    //para que no sobrepase al tiempo total de construcción
                TimeElapsed = ConstructionTime;
            }
        }
    }
}   


