namespace Library.Interfaces;

public interface IConstructionInfo
{
    // Propiedades básicas del edificio
    int WoodCost { get; set; }
    int StoneCost { get; set; }
    int ConstructionTime { get; }
    int TimeElapsed { get; }

    // Métodos para control de construcción
    void Construyendo(int seconds);
}