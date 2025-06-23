namespace Library.Core;
using Buildings;
using Interfaces;
using Actions;
 
public class Player
{
     public string Nombre { get; set; }
     public Resources Resources { get; }
     public List<Building> Buildings { get; }
     public Civilization Civilization { get; set; }
     public List<IUnit> Units { get; set; }
     
     private Civilization _society;
     public  int PoblacionLimite  { get; set; }
     public Actions Actions { get; set; }
     
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
         Nombre = nombre;
         Buildings = new List<Building>();
         Buildings.Add(new CivicCenter(this));
         Resources = new Resources();
         Civilization = _society;
         Units = new List<IUnit>();
         Actions = new Actions(this);
         PoblacionLimite = 10;
     }
}