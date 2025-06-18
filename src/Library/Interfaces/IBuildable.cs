namespace Library.Interfaces;

public interface IBuildable
{ 
        bool IsBuilt { get; } 
        bool CanBuild();
        bool Build();
        void Construyendo(int time);
        int Limite { get; }
}