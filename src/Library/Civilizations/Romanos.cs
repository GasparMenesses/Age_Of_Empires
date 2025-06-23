namespace Library;

// Esta clase representa la civilización Romanos en el juego, con su nombre, tipo de unidad única y bonificaciones específicas.

public class Romanos : Civilization
{
    public Romanos()
    {
        NombreCivilizacion = "Romanos";
        TipoDeUnidadUnica = "JulioCesar";
        Bonificacion = new Tuple<int, int, int, int>(0, 0, 50, 0);
        DescripcionBonificacion = "los romanos tienen un bonus de 50 de oro al inicio de la partida";
    }
}
