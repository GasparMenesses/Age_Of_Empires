using Library.Buildings;
using Library.Interfaces;

namespace Library.Core;
using Library;
 
public class Player
{
     public string Nombre { get; set; }
     public Resources Resources { get; }
     public List<Building> Buildings { get; }
     public Player(string nombre)
     {
         this.Nombre = nombre;
         this.Buildings = new List<Building>();
         this.Resources = new Resources(); 
     }
}