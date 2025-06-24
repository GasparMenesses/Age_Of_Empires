using Library.Core;


namespace Library.Tests;

public class Tests
{
    private Player player;
    [SetUp]
    public void Setup()
    {
        Map map = new Map();
        player = new Player("MiniMago", "Cordobeses");
    }

    [Test]
    public void BoardCreated()
    {
        int Length = Map.ReturnLength0() * Map.ReturnLength1();
        Assert.That(Length, Is.EqualTo(100 * 100));
    }
    [Test]
    public void PlayerCreated()
    {
        Assert.That(player.Nombre, Is.EqualTo("MiniMago"));
        Assert.That(player.Civilization.NombreCivilizacion, Is.EqualTo("Cordobeses"));
        Assert.That(player.Buildings.Count, Is.EqualTo(1));
        Assert.That(player.Resources.Wood, Is.EqualTo(0));
        Assert.That(player.Resources.Stone, Is.EqualTo(0));
        Assert.That(player.Resources.Food, Is.EqualTo(0));
        Assert.That(player.Resources.Gold, Is.EqualTo(0));
        Assert.That(player.Units.Count, Is.EqualTo(0));
        Assert.That(player.Actions, Is.Not.Null);
        Assert.That(player.Civilization, Is.Not.Null);
        Assert.That(player.PoblacionLimite, Is.EqualTo(10));
    }
    /// <summary>
    /// Este test verifica que el centro cívico del jugador se coloque en una posición aleatoria válida dentro del mapa
    /// con PlaceRandom.
    /// </summary>
    [Test]
    public void PlayerCivicCenterPositionIsInRange()
    {
        Map.PlaceRandom(player.Buildings[0].Symbol, player.Buildings[0]);
        Assert.That(player.Buildings[0].Position["x"], Is.Not.GreaterThanOrEqualTo(99));
        Assert.That(player.Buildings[0].Position["y"], Is.Not.GreaterThanOrEqualTo(99));
        Assert.That(player.Buildings[0].Position["x"], Is.Not.LessThanOrEqualTo(0));
        Assert.That(player.Buildings[0].Position["y"], Is.Not.LessThanOrEqualTo(0));
    }
    /// <summary>
    /// Este test verifica que el jugador pueda construir un edificio en una posición válida del mapa(se hace con barrack como ejemplo), 
    /// </summary>
    [Test]
    public void PlayerCanBuild()
    {
        int x = 10;
        int y = 10;
        player.Resources.Wood = 1000;
        player.Resources.Stone = 1000;
        int woodQuantity = player.Resources.Wood;
        int stoneQuantity = player.Resources.Stone;
        bool result = player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.True);
        Assert.That(Map.CheckMap(x,y), Is.EqualTo("Bk"));
        Assert.That(player.Buildings.Count, Is.EqualTo(2));
        Assert.That(player.Buildings[1].Symbol, Is.EqualTo("Bk"));
        Assert.That(player.Resources.Wood, Is.LessThan(woodQuantity));
        Assert.That(player.Resources.Stone, Is.LessThan(stoneQuantity));
    }
    [Test]
    public void PlayerCannotBuildOutsideMap()
    {
        int x = 101;
        int y = 101;
        bool result = player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(player.Buildings.Count, Is.EqualTo(1));
    }
    [Test]
    public void PlayerCannotBuildOnOccupiedSpace()
    {
        int x = 10;
        int y = 10;
        Map.ChangeMap((x, y),player.Buildings[0].Symbol, player.Buildings[0]);
        bool result = player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(player.Buildings.Count, Is.EqualTo(1));
    } 
}