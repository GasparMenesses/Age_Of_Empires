using Library.Core;

namespace Library.Interfaces;

public interface IConstructionInfo
{
    // Propiedades básicas del edificio
    int WoodCost { get; set; }
    int StoneCost { get; set; }
    int ConstructionTime { get; }
    int TimeElapsed { get; }
    bool IsBuilt { get; }

    // Métodos para control de construcción
    void Construyendo(int seconds);
    bool CanBuild();
    bool Build();
}