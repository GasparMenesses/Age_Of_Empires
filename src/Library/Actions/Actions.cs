namespace Library.Actions;
using Core;
using Interfaces;
using Units;
public class Actions
{
    public void Move(Player player, List<IUnit> units, (int x, int y) position)
    {
        foreach (IUnit unit in units)
        {
            if (!player.Units.Contains(unit))
                return;
        }
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != ".")
            return;
        for (int i = 0; i < units.Count; i++)
        {
            IUnit actualUnit = units[i];
            actualUnit.Position[0] = position.x;
            actualUnit.Position[1] = position.y;
        }
    }

    public void Farmear(Player player,Villager villager, string resource)
    {
        
        
    }
    
    
    
    
    
}