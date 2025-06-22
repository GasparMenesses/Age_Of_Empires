using Library.Buildings;
using Library.Interfaces;
namespace Library.Core;
using Library;
 
public class Player
{
     public string Nombre { get; set; }
     public Resources Resources { get; }
     public List<Building> Buildings { get; }
     public Civilization Civilization { get; set; }
     public List<IUnit> Units { get; set; }
    
     public Villager Villager { get; set; }
     
     private Civilization _society;
     public  int PoblacionLimite  { get; set; }
     
     public Player(string nombre  , string civilization)
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
         this.Nombre = nombre;
         this.Buildings = new List<Building>();
         this.Resources = new Resources();
         this.Civilization = _society;
         this.Units = new List<IUnit>();
         this.PoblacionLimite = 10;
     }
}