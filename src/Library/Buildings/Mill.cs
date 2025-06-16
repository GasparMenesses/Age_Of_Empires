namespace Library.Buildings;
using Core;



public class Mill : Building 
{
    // Cantidad total de comida almacenada en el molino
    public int AlmacenaComida { get; set; }
    public int CreationCostWood { get; set; }
    public int CreationCostStone { get; set; }

    // Constructor: inicializa el molino con un jugador y pone la comida en 0.
    // Usa 'base(player)' para pasar el jugador al constructor de CivicCenter
    public Mill(Resources resources)
    : base(resources, woodCost: 150, stoneCost: 50, constructionTime: 20)
    
    {
        AlmacenaComida = 0; 
    }

    // Método que suma la cantidad de comida generada al almacenamiento
    public void AlmacenarComida(int cantidad)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        AlmacenaComida += cantidad;
    }
}