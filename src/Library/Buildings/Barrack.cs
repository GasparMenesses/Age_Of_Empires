namespace Library.Buildings;

// Representa un edificio cuartel en el juego
//cumple con srp porque solo se encarga de la lógica del cuartel    
public class Barrack : Building
{
    // Símbolo que identifica al cuartel en el mapa
    public static string Symbol => "Bk";

    // Constructor del cuartel
    // posicion: coordenadas donde se construye el cuartel
    // resources: recursos necesarios para construirlo
    public Barrack((int x, int y) posicion, Resources resources)
        // Llama al constructor de Building con vida, defensa, costo y posición
        : base(resources, 500, 300, 180, posicion)
    {
    }
}