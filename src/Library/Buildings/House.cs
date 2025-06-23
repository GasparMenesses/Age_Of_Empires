using Library.Core;

namespace Library.Buildings;

// Representa una casa que incrementa el límite de población del jugador
// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con la casa y el aumento de población
public class House : Building
{

    public House(Player player, (int x, int y) position)
        : base(position, 0, 0, 60)
    {
    }
    
    // Aumenta el límite de población del jugador en la cantidad indicada
    public void AumentarPoblacionLimite(Player player)
    {
        player.PoblacionLimite += 4;
    }
}