namespace Library.Actions;
using Core;
using Interfaces;
public class Actions
{
    public void Move(Player player, List<IUnit> units, (int x, int y) position)
    {
        bool playerHas = true;
        foreach (IUnit unit in units)
        {
            if (!player.Units.Contains(unit))
                playerHas = false;
        }
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != "." || playerHas == false)
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