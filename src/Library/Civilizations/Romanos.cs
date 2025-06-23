namespace Library;

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
