namespace Library.Core;
using Library;
 
public class Player
{
     public string Nombre { get; set; }
     public Resources Resources { get; }
     public Player(string nombre)
     {
         this.Nombre = nombre;
         this.Resources = new Resources();
     }
}