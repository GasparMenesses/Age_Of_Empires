namespace Library;

public class Civilization
{
    public string Nombre { get; set; }
    public string TipoDeUnidadUnica { get; set; }
    public string Bonificacion { get; set; }

    public Civilization(string nombre, string tipoDeUnidadUnica, string bonificacion)
    {
        Nombre = nombre;
        TipoDeUnidadUnica = tipoDeUnidadUnica;
        Bonificacion = bonificacion;
    }

}