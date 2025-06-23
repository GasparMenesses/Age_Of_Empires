using Library.Core;

namespace Library.Buildings;



public class Barrack : Building
{
    // Símbolo que identifica al cuartel en el mapa
    public new static string Symbol => "Bk";

    public Barrack(Player player, (int x, int y) position) : base(position, woodCost: 25, stoneCost: 55, constructionTime: 30)
    {
        // Representa un edificio cuartel en el juego
        //cumple con srp porque solo se encarga de la lógica del cuartel    
    }

}