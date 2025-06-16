using Library.Buildings;
using Library.Interfaces;

namespace Library.Core;
using Library;
 
public class Player
{
     public string Nombre { get; set; }
     public Resources Resources { get; }
     public List<Building> Buildings { get; }
     public Civilization Civilization { get; }
     private Civilization society;
     
     public Player(string nombre  , string civilization)
     {
         switch (civilization)
         {
             case "Cordobeses":
                 society = new Cordobeses();
                 break;
             case "Romanos":
                 society = new Romanos();
                 break;
             case "Vikingos":
                 society = new Vikingos();
                 break;
         }
         this.Nombre = nombre;
         this.Buildings = new List<Building>();
         this.Resources = new Resources();
         this.Civilization = society;
     }
}