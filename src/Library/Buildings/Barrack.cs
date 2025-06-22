namespace Library.Buildings;

using Core;
public class Barrack : Building
{
    public new static string Symbol => "Bk";

    public Barrack(Player player,(int x, int y) position) : base(position, woodCost:25, stoneCost:55,constructionTime:30)
    {
        // Aquí puedes agregar lógica de inicialización si es necesario
    }
}