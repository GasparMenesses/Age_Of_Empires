using Library.Core;

namespace Library.Buildings;

// Representa una casa que incrementa el límite de población del jugador
public class House : Building
{
    // Constructor de la casa
    // resources: referencia a los recursos del jugador
    // position: coordenadas donde se construye la casa
    public House(Resources resources, (int x, int y) position)
        // Llama al constructor base con costo de 100 madera, 0 piedra, 60 segundos de construcción y la posición
        : base(resources, 100, 0, 60, position)
    {
    }

    // Aumenta el límite de población del jugador en la cantidad indicada
    public void AumentarPoblacionLimite(Player player, int cantidad)
    {
        player.PoblacionLimite += cantidad;
    }
}