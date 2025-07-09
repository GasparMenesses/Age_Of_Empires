namespace Library;

/// <summary>
/// Esta clase representa una civilización en el juego, con su nombre, tipo de unidad única y bonificaciones específicas.
/// </summary>
public abstract class Civilization
{
    /// <summary>S
    /// Nombre de la civilización.
    /// </summary>
    public string NombreCivilizacion { get; set; }

    /// <summary>
    /// Tipo de unidad única que posee la civilización.
    /// </summary>
    public string TipoDeUnidadUnica { get; set; }

    /// <summary>
    /// Bonificación representada como una tupla de cuatro enteros (ej. comida, madera, piedra, oro).
    /// </summary>
    public Tuple<int, int, int, int> Bonificacion { get; set; }

    /// <summary>
    /// Descripción textual de la bonificación otorgada por la civilización.
    /// </summary>
    public string DescripcionBonificacion { get; set; }
}