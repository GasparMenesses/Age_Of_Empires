using Library.Interfaces;

namespace Library.Buildings;

/// <summary>
/// Clase base utilizada para definir los costos y tiempos de construcción de los edificios de almacenamiento.
/// Cumple con SRP porque solo se encarga de la lógica de los edificios de almacenamiento.
/// </summary>
public class Building : IConstructionInfo, IBuildable
{
    /// <summary>
    /// Costo en madera del edificio.
    /// </summary>
    public int WoodCost { get; set; }

    /// <summary>
    /// Costo en piedra del edificio.
    /// </summary>
    public int StoneCost { get; set; }

    /// <summary>
    /// Tiempo total de construcción del edificio, en segundos.
    /// </summary>
    public int ConstructionTime { get; }

    /// <summary>
    /// Tiempo transcurrido desde que comenzó la construcción.
    /// </summary>
    public int TimeElapsed { get; private set; }

    /// <summary>
    /// Vida del edificio. Por defecto es 100.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Indica si el edificio ya está completamente construido.
    /// </summary>
    public bool IsBuilt => TimeElapsed >= ConstructionTime;

    /// <summary>
    /// Símbolo que representa gráficamente al edificio en el mapa.
    /// </summary>
    public virtual string Symbol { get; set; }

    /// <summary>
    /// Constructor protegido que inicializa los valores principales del edificio.
    /// </summary>
    /// <param name="woodCost">Costo en madera.</param>
    /// <param name="stoneCost">Costo en piedra.</param>
    /// <param name="constructionTime">Tiempo de construcción en segundos.</param>
    /// <param name="health">Vida inicial del edificio (por defecto 100).</param>
    public Building(int woodCost, int stoneCost, int constructionTime, int health = 100)
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        ConstructionTime = constructionTime;
        TimeElapsed = 0;
        Health = health; 
    }

    /// <summary>
    /// Avanza la construcción del edificio sumando segundos al tiempo transcurrido.
    /// </summary>
    /// <param name="segundos">Cantidad de segundos a sumar al progreso de construcción.</param>
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
