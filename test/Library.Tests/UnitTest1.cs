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
        string symbol = player.Buildings[1].Symbol;
        string [,] Board = Map.ReturnBoard();
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

    
    /// <summary>
    /// Este test verifica que el jugador no puede construir un edificio si no tiene suficientes recursos.
    /// </summary>
    [Test]
    public void PlayerCannotBuildWithoutResources()
    {
        int x = 20;
        int y = 20;
        player.Resources.Wood = 0;
        player.Resources.Stone = 0;
        bool result = player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(player.Buildings.Count, Is.EqualTo(1));
    }
    
    
    /// <summary>
    /// Este test verifica que el jugador no puede construir un edificio con un nombre inválido.
    /// </summary>
    [Test]
    public void PlayerCannotBuildWithInvalidBuildingName()
    {
        int x = 15;
        int y = 15;
        player.Resources.Wood = 1000;
        player.Resources.Stone = 1000;
        bool result = player.Actions.Build("EdificioInexistente", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("EdificioInexistente"));
        Assert.That(player.Buildings.Count, Is.EqualTo(1));
    }
    
    /// <summary>
    /// Este test verifica que el jugador no puede construir dos edificios en la misma posición del mapa.
    /// </summary>
    [Test]
    public void PlayerCannotBuildTwoBuildingsInSamePosition()
    {
        int x = 25;
        int y = 25;
        player.Resources.Wood = 2000;
        player.Resources.Stone = 2000;
        // Construye el primer edificio
        bool firstBuild = player.Actions.Build("Barrack", (x, y)).Result;
        // Intenta construir otro edificio en la misma posición
        bool secondBuild = player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(firstBuild, Is.True);
        Assert.That(secondBuild, Is.False);
        Assert.That(player.Buildings.Count, Is.EqualTo(2));
    }
    
    /// <summary>
    /// Este test verifica que el jugador no puede construir un edificio en una posición negativa del mapa.
    /// </summary>
    [Test]
    public void PlayerCannotBuildWithNegativePosition()
    {
        int x = -5;
        int y = -10;
        player.Resources.Wood = 1000;
        player.Resources.Stone = 1000;
        bool result = player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(player.Buildings.Count, Is.EqualTo(1));
    }
  
    
    // <summary>
    /// Verifica que la civilización Cordobeses se inicializa con los valores correctos:
    /// nombre, tipo de unidad única, bonificación de recursos y descripción de bonificación.
    /// </summary>
    [Test]
    public void CordobesesInitializationTest()
    {
        // Act
        var cordobeses = new Cordobeses();

        // Assert
        Assert.That(cordobeses.NombreCivilizacion, Is.EqualTo("Cordobeses"));
        Assert.That(cordobeses.TipoDeUnidadUnica, Is.EqualTo("Ferneh"));
        Assert.That(cordobeses.Bonificacion, Is.EqualTo(new Tuple<int, int, int, int>(0, 0, 0, 100)));
        Assert.That(cordobeses.DescripcionBonificacion, Does.Contain("bonus de 100 de comida"));
    }
    
    /// <summary>
    /// Verifica que la civilización Romanos se inicializa con los valores correctos:
    /// nombre, tipo de unidad única, bonificación de recursos y descripción de bonificación.
    /// </summary>
    [Test]
    public void RomanosInitializationTest()
    {
        // Act
        var romanos = new Romanos();

        // Assert
        Assert.That(romanos.NombreCivilizacion, Is.EqualTo("Romanos"));
        Assert.That(romanos.TipoDeUnidadUnica, Is.EqualTo("JulioCesar"));
        Assert.That(romanos.Bonificacion, Is.EqualTo(new Tuple<int, int, int, int>(0, 0, 50, 0)));
        Assert.That(romanos.DescripcionBonificacion.ToLower(), Does.Contain("bonus de 50 de oro"));
    }
    
    /// <summary>
    /// Verifica que la civilización Vikingos se inicializa con los valores correctos:
    /// nombre, tipo de unidad única, bonificación de recursos y descripción de bonificación.
    /// </summary>
    [Test]
    public void VikingosInitializationTest()
    {
        // Act
        var vikingos = new Vikingos();

        // Assert
        Assert.That(vikingos.NombreCivilizacion, Is.EqualTo("Vikingos"));
        Assert.That(vikingos.TipoDeUnidadUnica, Is.EqualTo("Thor"));
        Assert.That(vikingos.Bonificacion, Is.EqualTo(new Tuple<int, int, int, int>(100, 0, 0, 0)));
        Assert.That(vikingos.DescripcionBonificacion.ToLower(), Does.Contain("bonus de 100 de madera"));
    }
    
    
    /// <summary>
    /// Verifica que ninguna civilización tenga campos nulos al ser creada.
    /// </summary>
    [Test]
    public void CivilizationsNoNullFields()
    {
        Civilization[] civis = new Civilization[]
        {
            new Cordobeses(),
            new Romanos(),
            new Vikingos()
        };

        foreach (var c in civis)
        {
            Assert.That(c.NombreCivilizacion, Is.Not.Null);
            Assert.That(c.TipoDeUnidadUnica, Is.Not.Null);
            Assert.That(c.Bonificacion, Is.Not.Null);
            Assert.That(c.DescripcionBonificacion, Is.Not.Null);
        }
    }
    
    
    
    /// <summary>
    /// Verifica que los valores de bonificación sean coherentes (entre 0 y 100).
    /// </summary>
    [Test]
    public void BonificacionesEnRangoValido()
    {
        Civilization[] civis = new Civilization[]
        {
            new Cordobeses(),
            new Romanos(),
            new Vikingos()
        };

        foreach (var c in civis)
        {
            Assert.That(c.Bonificacion.Item1, Is.InRange(0, 100));
            Assert.That(c.Bonificacion.Item2, Is.InRange(0, 100));
            Assert.That(c.Bonificacion.Item3, Is.InRange(0, 100));
            Assert.That(c.Bonificacion.Item4, Is.InRange(0, 100));
        }
    }
    
}
