using System.Text.Json.Serialization;

namespace Library;

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