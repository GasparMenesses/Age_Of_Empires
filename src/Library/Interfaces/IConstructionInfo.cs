namespace Library.Interfaces;

// Esta interfaz define las propiedades y métodos necesarios para manejar la información de construcción de un edificio en el juego.

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