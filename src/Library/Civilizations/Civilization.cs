namespace Library;

// Esta clase representa una civilización en el juego, con su nombre, tipo de unidad única y bonificaciones específicas.

public class Civilization
{
    public string NombreCivilizacion { get; set; }
    public string TipoDeUnidadUnica { get; set; }
    public Tuple<int,int,int,int> Bonificacion { get; set; }
    public string DescripcionBonificacion { get; set; }
    
}