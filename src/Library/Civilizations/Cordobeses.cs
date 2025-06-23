namespace Library;

// Esta clase representa la civilización Cordobeses en el juego, con su nombre, tipo de unidad única y bonificaciones específicas.

public class Cordobeses : Civilization
{
    public Cordobeses()
    {
        NombreCivilizacion = "Cordobeses";
        TipoDeUnidadUnica = "Ferneh";
        Bonificacion = new Tuple<int, int, int, int>(0, 0, 0, 100);
        DescripcionBonificacion = "los cordobobeses te otorgan un bonus de 100 de comida al inicio de la partida.";
    }
}