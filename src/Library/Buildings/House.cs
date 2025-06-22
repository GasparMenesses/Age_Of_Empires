using Library.Core;

namespace Library.Buildings;

public class House: Building
{
    
    public House(Resources resources, (int x, int y) position) 
        : base(resources, 100, 0, 60, position)
    {
        
    }
    public void AumentarPoblacionLimite(Player player, int cantidad)
    {
        player.PoblacionLimite += cantidad;
    }

    
}