using Library.Core;

namespace Library.Buildings;

public class House: Building
{
    
    public House(Resources resources, (int x, int y) position) 
        : base(position, 0, 0, 60)
    {
        
    }
    public void AumentarPoblacionLimite(Player player, int cantidad)
    {
        player.PoblacionLimite += cantidad;
    }

    
}