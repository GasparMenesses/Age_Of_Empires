namespace Library.Interfaces;

public interface IBuildable
{ 
        bool IsBuilt { get; } // Indica si el edificio está construido 
        void Construyendo(int time); // Inicia el proceso de construcción del edificio, recibe el tiempo que se tarda en construirlo
      
}