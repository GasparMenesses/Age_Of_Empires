namespace Library.Actions;
using Core;
using Interfaces;
using Units;
using Buildings;
public class Actions
{
    private Player Player { get; set; }
    private Building building { get; set; }
    public Actions(Player player)
    {
        Player = player;
    }

    public void Build(string _building, (int x,int y) position)
    {
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != ".")
            return;
        switch (_building)
        { 
            case "Barrack":
                building = new Barrack(this.Player,position);
                break;
            case "GoldStorage":
                building = new GoldStorage(this.Player,position);
                break;
            case "Mill":
                building = new Mill(this.Player,position);
                break;
            case "StoneStorage":
                building = new StoneStorage(this.Player,position);
                break;
            case "WoodStorage":
                building = new WoodStorage(this.Player,position);
                break;
            default:
                return;
        }
        if (Player.Resources.Wood >= building.WoodCost && Player.Resources.Stone >= building.StoneCost) //verifica si hay disponible la cantidad de madera y piedra que requiere crear el almacen 
        {
            Player.Resources.RemoveResources(wood: building.WoodCost, stone: building.StoneCost);
            Player.Buildings.Add(building);
        }
    }
    public void Move(List<IUnit> units, (int x, int y) position)
    {
        foreach (IUnit unit in units)
        {
            if (!Player.Units.Contains(unit))
                return;
        }
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != ".")
            return;
        for (int i = 0; i < units.Count; i++)
        {
            IUnit actualUnit = units[i];
            actualUnit.Position["x"] = position.x;
            actualUnit.Position["y"] = position.y;
        }
        foreach (IUnit unit in units)
        {
            unit.Position["x"] = position.x;
            unit.Position["y"] = position.y;
        }
    }

    public void Farmear(Player player,Villager villager, string resource)
    {
        
    }
    
    
    
    
    
}