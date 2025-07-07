namespace Library.Core;
using Buildings;
using Interfaces;
using Actions;
 
// Esta clase representa un jugador en el juego, con su nombre, recursos, edificios, civilización y unidades.
// El jugador puede pertenecer a una civilización específica y tiene un límite de población, así como acciones disponibles.
// Además, el jugador comienza con un centro cívico y puede construir otros edificios y unidades a lo largo del juego.

public class Player
{
     public string Nombre { get; set; }
     public string Id { get; set; }
     public Resources Resources { get; }
     public Dictionary<Building, (int x, int y)> Buildings { get; }
     public Civilization Civilization { get; set; }
     public List<IUnit> Units { get; set; }
     
     private Civilization _society;
     public  int PoblacionLimite  { get; set; }
     public Actions Actions { get; set; }

     public Player(string nombre, string civilization, string id = null)
     {
         switch (civilization)
         {
             case "Cordobeses":
                 _society = new Cordobeses();
                 break;
             case "Romanos":
                 _society = new Romanos();
                 break;
             case "Vikingos":
                 _society = new Vikingos();
                 break;
             default:
                 throw new Exception("Civilización desconocida");
         }
         Id = id;
         Nombre = nombre;
         Buildings = new Dictionary<Building, (int x, int y)>();
         Resources = new Resources();
         Civilization = _society;
         Units = new List<IUnit>();
         Actions = new Actions(this);
         PoblacionLimite = 10;
         Buildings.Add(new CivicCenter(),(0, 0));    
     }
}