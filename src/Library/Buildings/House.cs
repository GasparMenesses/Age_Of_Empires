using Library.Core;

namespace Library.Buildings;

/// <summary>
/// Representa una casa que incrementa el límite de población del jugador.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con la casa y el aumento de población.
/// </summary>
public class House : Building
{
    /// <summary>
    /// Símbolo que representa la casa en el mapa.
    /// </summary>
    public override string Symbol => "🏠🏠";

    /// <summary>
    /// Constructor de la clase House.
    /// </summary>
    /// <param name="player">Jugador propietario de la casa.</param>
    /// <param name="position">Posición de la casa en el mapa.</param>
    public House(Player player, (int x, int y) position)
        : base(0, 0, 60)
    {
    }

    /// <summary>
    /// Aumenta el límite de población del jugador en una cantidad fija.
    /// </summary>
    /// <param name="player">Jugador al que se le aumenta el límite de población.</param>
    public void AumentarPoblacionLimite(Player player)
    {
        player.PoblacionLimite += 4;
    }
}