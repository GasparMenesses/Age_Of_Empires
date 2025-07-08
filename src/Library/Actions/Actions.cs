using Library.Farming;
using Library.Core;
using Library.Interfaces;
using Library.Units;
using Library.Buildings;

namespace Library.Actions;

/// <summary>
/// Esta clase maneja las acciones que un jugador puede realizar en el juego, como construir edificios, mover unidades y recolectar recursos.
/// Cumple con SRP porque se encarga exclusivamente de las acciones del jugador, separando la lógica de negocio de otras responsabilidades como la gestión de recursos o el mapa.
/// </summary>
public class Actions
{
    /// <summary>
    /// Referencia al jugador que realiza las acciones.
    /// </summary>
    private Player Player { get; set; }

    /// <summary>
    /// Referencia al edificio en construcción.
    /// </summary>
    private Building Building { get; set; }

    /// <summary>
    /// Constructor de la clase Actions.
    /// </summary>
    /// <param name="player">Jugador que realizará las acciones.</param>
    public Actions(Player player)
    {
        Player = player;
    }

    /// <summary>
    /// Intenta construir un edificio en una posición dada.
    /// </summary>
    /// <param name="_building">Nombre del edificio a construir.</param>
    /// <param name="position">Posición (x, y) donde se construirá.</param>
    /// <returns>True si se construyó con éxito, False en caso contrario.</returns>
    public async Task<bool> Build(string _building, (int x, int y) position)
    {
        if (position.x >= 100 || position.x < 0 || position.y >= 100 || position.y < 0 || Map.CheckMap(position.x, position.y) != "..")
            return false;
        if (_building == "Barrack")
            Building = new Barrack(Player, position);
        else if (_building == "GoldStorage")
            Building = new GoldStorage(Player, position);
        else if (_building == "Mill")
            Building = new Mill(Player, position);
        else if (_building == "StoneStorage")
            Building = new StoneStorage(Player, position);
        else if (_building == "WoodStorage")
            Building = new WoodStorage(Player, position);
        else
            return false;

        if (Player.Resources.Wood >= Building.WoodCost && Player.Resources.Stone >= Building.StoneCost)
        {
            Player.Resources.RemoveResources(wood: Building.WoodCost, stone: Building.StoneCost);
            Player.Buildings.Add(Building, position);
            Map.ChangeMap(position, Building.Symbol);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Mueve una lista de unidades a una nueva posición.
    /// </summary>
    /// <param name="units">Lista de unidades a mover.</param>
    /// <param name="position">Nueva posición (x, y).</param>
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

    /// <summary>
    /// Realiza una acción de recolección de recursos con un aldeano.
    /// </summary>
    /// <param name="villager">Aldeano que recolecta.</param>
    /// <param name="resource">Tipo de recurso a recolectar (madera, piedra, oro, comida).</param>
    public async Task Farmear(Villager villager, string resource)
    {
        await Task.Delay(5000); // Simula el tiempo de recolección
        string res = resource.ToLower();
        switch (resource)
        {
            case "madera":
                Player.Resources.AddResources(wood:Woods.TasaDeRecoleccion);
                break;
            case "piedra":  
                Player.Resources.AddResources(stone:Quarry.TasaDeRecoleccion);
                break;
            case "oro":
                Player.Resources.AddResources(gold:GoldMine.TasaDeRecoleccion);
                break;
            case "comida":
                Player.Resources.AddResources(food:Farm.TasaDeRecoleccion);
                break;
        }
    }

    /// <summary>
    /// Ataca unidades enemigas en la misma posición que los atacantes.
    /// </summary>
    /// <param name="atacantes">Lista de unidades que atacan.</param>
    /// <param name="atacados">Lista de unidades objetivo.</param>
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
                        Map.ChangeMap((atacado.Position["x"], atacado.Position["y"]), "..");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Ataca edificios enemigos en la misma posición que los atacantes.
    /// </summary>
    /// <param name="atacantes">Lista de unidades atacantes.</param>
    /// <param name="edificios">Lista de edificios objetivo.</param>
    public void AtacarEdificios(List<IUnit> atacantes, List<Building> edificios)
    {
        foreach (var atacante in atacantes)
        {
            foreach (var edificio in edificios.ToList())
            {
                if (Player.Buildings[edificio].x == atacante.Position["x"] &&
                    Player.Buildings[edificio].y == atacante.Position["y"])
                {
                    edificio.Health -= atacante.Attack;
                    if (edificio.Health <= 0)
                    {
                        edificios.Remove(edificio);
                    }
                }
            }
        }
    }
}
