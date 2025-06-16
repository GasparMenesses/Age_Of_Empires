namespace Library.Interfaces;
using Library;
public interface IBuildable
{
        bool IsBuilt { get; }
        bool CanBuild();
        bool Build();
        void Construyendo(int time);
    
}