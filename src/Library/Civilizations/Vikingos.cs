using System.Text.Json.Serialization;

namespace Library;

// Esta clase representa la civilización Vikingos en el juego, con su nombre, tipo de unidad única y bonificaciones específicas.

public class Vikingos : Civilization
{
    public Vikingos()
    {
        NombreCivilizacion = "Vikingos";
        TipoDeUnidadUnica = "Thor";
        Bonificacion = new Tuple<int, int, int, int>(100, 0, 0, 0);
        DescripcionBonificacion = "los vikingos tienen un bonus de 100 de madera al inicio de la partida";
        
    }
}