namespace Library.Interfaces;

public interface IBuildable
{ 
        bool IsBuilt { get; } // Indica si el edificio está construido 
        bool CanBuild();// Indica si se puede construir el edificio si se tienen los recursos necesarios
        bool Build();   // Construye el edificio, devuelve true si se construyó correctamente
        void Construyendo(int time); // Inicia el proceso de construcción del edificio, recibe el tiempo que se tarda en construirlo
      
}