using Library.Interfaces;

namespace Library.Buildings;
//clase base abstracta utilizada para definir los costos y tiempos de construccion de los edificios de almacenamientos
public abstract class Building : IConstructionInfo,IBuildable
{
    public int WoodCost { get; set; }
    public int StoneCost { get; set; }
    public int ConstructionTime { get; private set; }
    public int TimeElapsed { get; private set; }
    public bool IsBuilt => TimeElapsed >= ConstructionTime;
    private readonly Resources _resources;

    protected Building( Resources resources, int woodCost, int stoneCost, int constructionTime)
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        ConstructionTime = constructionTime;
        TimeElapsed = 0;
        _resources = resources;

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
    public bool CanBuild()
    {           //verifica si hay disponible la cantidad de madera y piedra que requiere crear el almacen 
        return _resources.Wood[0] >= WoodCost && _resources.Stone[0] >= StoneCost;
    }
    
    public bool Build()
{                                  //se llama a canbuild para asegurarse de que hay recursos suficientes, sino los hay retorna false
                                    // si no retorna false, entonces procede a descontar la cantidad de recursos correspondiente
        if (!CanBuild())
            return false;
    
        return _resources.RemoveResources(wood: WoodCost, stone: StoneCost);
    }
}   


