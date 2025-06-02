using Library.Core;

namespace Library.Actions;
using Interfaces;
public class Actions
{
    public void Move(List<IUnit> unit, (int x, int y) position)
    {
        if ((position.x > 100) || (position.x < 0) || (position.y > 100) || (position.y < 0))
            return;
        for (int i = 0; i < unit.Count; i++)
        {
            IUnit actualUnit = unit[i];
            actualUnit.Position[0] = position.x;
            actualUnit.Position[1] = position.y;
        }
    }

    public void Farmear(Player player,Villager villager, string resource)
    {
    }
}