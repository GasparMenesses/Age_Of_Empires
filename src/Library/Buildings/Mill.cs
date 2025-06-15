namespace Library.Buildings;
using Core;


// Hereda de CivicCenter, por lo que también tiene acceso a recursos como Food, Gold, etc.
public class Mill : CivicCenter
{
    // Cantidad total de comida almacenada en el molino
    public int AlmacenComida { get; set; }

    // Constructor: inicializa el molino con un jugador y pone la comida en 0.
    // Usa 'base(player)' para pasar el jugador al constructor de CivicCenter
    public Mill(Player player) : base(player)
    {
        Food = 0; // Inicializa la comida del CivicCenter (heredado) en 0
    }

    // Método que suma la cantidad de comida generada al almacenamiento
    public void AlmacenarComida(int cantidad)
    {
        AlmacenComida += cantidad;
    }
}