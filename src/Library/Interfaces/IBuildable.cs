namespace Library.Interfaces;

// Esta interfaz define las operaciones básicas para un edificio en el juego, como la construcción y el estado de construcción.

public interface IBuildable
{ 
        bool IsBuilt { get; } // Indica si el edificio está construido 
        void Construyendo(int time); // Inicia el proceso de construcción del edificio, recibe el tiempo que se tarda en construirlo
      
}