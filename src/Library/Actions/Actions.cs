using Library.Farming;
using Library.Core;
using Library.Interfaces;
using Library.Units;
using Library.Buildings;

namespace Library.Actions;

// Esta clase maneja las acciones que un jugador puede realizar en el juego, como construir edificios, mover unidades y recolectar recursos.
// Cumple con SRP porque se encarga exclusivamente de las acciones del jugador, separando la lógica de negocio de otras responsabilidades como la gestión de recursos o el mapa.
public class Actions
{
    private Player Player { get; set; }
    private Building building { get; set; }
    public Actions(Player player)
    {
        Player = player;
    }

    public async Task<bool> Build(string _building, (int x, int y) position)
    {
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != "..")
            return false;

        if (_building == "Barrack")
            building = new Barrack(Player, position);
        else if (_building == "GoldStorage")
            building = new GoldStorage(Player, position);
        else if (_building == "Mill")
            building = new Mill(Player, position);
        else if (_building == "StoneStorage")
            building = new StoneStorage(Player, position);
        else if (_building == "WoodStorage")
            building = new WoodStorage(Player, position);
        else
            return false;

        if (Player.Resources.Wood >= building.WoodCost && Player.Resources.Stone >= building.StoneCost)
        {
            await Task.Delay(10000);
            Player.Resources.RemoveResources(wood: building.WoodCost, stone: building.StoneCost);
            Player.Buildings.Add(building);
            Map.ChangeMap(position, building.Symbol, building);
            return true;
        }

        return false;
    }
    public void Move(List<IUnit> units, (int x, int y) position)
    {
        foreach (IUnit unit in units)
        {
            if (!Player.Units.Contains(unit))
                return;
        }
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != "..")
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

    public async Task Farmear(Villager villager, string resource)
    {
        if (!Player.Units.Contains(villager))
            return;
        await Task.Delay(5000); // Simula el tiempo de recolección

        string res = resource.ToLower();
        if (res == "oro")
            Player.Resources.Gold += GoldMine.TasaDeRecoleccion;
        else if (res == "piedra")
            Player.Resources.Stone += Quarry.TasaDeRecoleccion;
        else if (res == "madera")
            Player.Resources.Wood += Woods.TasaDeRecoleccion;
        else if (res == "comida")
            Player.Resources.Food += Farm.TasaDeRecoleccion;
    }

    public void AtacarUnidades(List<IUnit> atacantes, List<IUnit> atacados)
    {
        foreach (var atacante in atacantes)
        {
            foreach (var atacado in atacados)
            {
                if (atacado.Position["x"] == atacante.Position["x"] && atacado.Position["y"] == atacante.Position["y"])
                {
                    atacado.Life -= atacado.Attack;
                    if (atacado.Life <= 0)
                    {
                        Player.Units.Remove(atacado);
                        Map.ChangeMap((atacado.Position["x"], atacado.Position["y"]), "..", null);
                    }
                }
            }
        }
    }
    public void AtacarEdificios()
    {
        
    }
    
}