using Library.Interfaces;

namespace Library.Buildings;
//clase base abstracta utilizada para definir los costos y tiempos de construccion de los edificios de almacenamientos
//cumple con srp porque solo se encarga de la lógica de los edificios de almacenamientos
public abstract class Building : IConstructionInfo, IBuildable
{

    public int WoodCost { get; set; }
    public int StoneCost { get; set; }
    public int ConstructionTime { get; private set; }

    public int TimeElapsed { get; private set; }

    // Indica si el edificio ya está completamente construido
    public bool IsBuilt => TimeElapsed >= ConstructionTime;

    // Referencia a los recursos disponibles del jugador
    private readonly Resources _resources;
    public (int x, int y) Posicion { get; set; }

// Constructor protegido que inicializa los valores principales del edificio
    protected Building(Resources resources, int woodCost, int stoneCost, int constructionTime, (int x, int y) posicion)
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        ConstructionTime = constructionTime;
        TimeElapsed = 0;
        _resources = resources;
        Posicion = posicion;

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

    // Verifica si hay suficientes recursos para construir el edificio
    public bool CanBuild()
    {
        // Verifica si hay disponible la cantidad de madera y piedra que requiere crear el almacén
        return _resources.Wood >= WoodCost && _resources.Stone >= StoneCost;
    }

    // Intenta construir el edificio descontando los recursos necesarios
    public bool Build()
    {
        // Se llama a CanBuild para asegurarse de que hay recursos suficientes, si no los hay retorna false
        if (!CanBuild())
            return false;

        // Si hay recursos suficientes, los descuenta y retorna true si la operación fue exitosa
        return _resources.RemoveResources(wood: WoodCost, stone: StoneCost);
    }
}    