using Library.Core;
using Library.Buildings;
using Library.Farming;

namespace Library.Tests;

public class Tests
{
    private Player _player;
    private CivicCenter _civicCenter;
    [SetUp]
    public void Setup()
    {
        
        _player = new Player("MiniMago", "Cordobeses");
        _civicCenter  = (CivicCenter)_player.Buildings[0];
    }
    /// <summary>
    /// Verifica que el mapa se crea con las dimensiones correctas (100x100).
    /// </summary>

    [Test]
    public void BoardCreated()
    {
        int length = Map.ReturnLength0() * Map.ReturnLength1();
        Assert.That(length, Is.EqualTo(100 * 100));
    }
    
    /// <summary>
    /// Verifica que el jugador se crea correctamente con los valores iniciales esperados:
    /// Nombre, civilización, edificios, unidades, acciones y población límite.
    /// </summary>
    [Test]
    public void PlayerCreated()
    {
        Assert.That(_player.Nombre, Is.EqualTo("MiniMago"));
        Assert.That(_player.Civilization.NombreCivilizacion, Is.EqualTo("Cordobeses"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(1));
        Assert.That(_player.Units.Count, Is.EqualTo(0));
        Assert.That(_player.Actions, Is.Not.Null);
        Assert.That(_player.Civilization, Is.Not.Null);
        Assert.That(_player.PoblacionLimite, Is.EqualTo(10));
    }
    /// <summary>
    /// Este test verifica que el centro cívico del jugador se coloque en una posición aleatoria válida dentro del mapa
    /// con PlaceRandom.
    /// </summary>
    [Test]
    public void PlayerCivicCenterPositionIsInRange()
    {
        Map.PlaceRandom(_player.Buildings[0].Symbol, _player.Buildings[0]);
        Assert.That(_player.Buildings[0].Position["x"], Is.Not.GreaterThanOrEqualTo(99));
        Assert.That(_player.Buildings[0].Position["y"], Is.Not.GreaterThanOrEqualTo(99));
        Assert.That(_player.Buildings[0].Position["x"], Is.Not.LessThanOrEqualTo(0));
        Assert.That(_player.Buildings[0].Position["y"], Is.Not.LessThanOrEqualTo(0));
    }
    /// <summary>
    /// Este test verifica que el jugador pueda construir un edificio en una posición válida del mapa(se hace con barrack como ejemplo), 
    /// </summary>
    [Test]
    public void PlayerCanBuild()
    {
        int x = 10;
        int y = 10;
        _player.Resources.Wood = 1000;
        _player.Resources.Stone = 1000;
        int woodQuantity = _player.Resources.Wood;
        int stoneQuantity = _player.Resources.Stone;
        bool result = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.True);
        
        
        Assert.That(Map.CheckMap(x,y), Is.EqualTo("Bk"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(2));
        Assert.That(_player.Buildings[1].Symbol, Is.EqualTo("Bk"));
        Assert.That(_player.Resources.Wood, Is.LessThan(woodQuantity));
        Assert.That(_player.Resources.Stone, Is.LessThan(stoneQuantity));
    }
    
    /// <summary>
    /// Confirma que no se puede construir un edificio fuera de los límites del mapa.
    /// (posiciones mayores a 99
    /// </summary>
    [Test]
    public void PlayerCannotBuildOutsideMap()
    {
        int x = 101;
        int y = 101;
        bool result = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
    }
    /// <summary>
    /// Comprueba que el jugador no puede construir un edificio en una posición ya ocupada por otro edificio.
    /// </summary>
    [Test]
    public void PlayerCannotBuildOnOccupiedSpace()
    {
        int x = 10;
        int y = 10;
        Map.ChangeMap((x, y),_player.Buildings[0].Symbol, _player.Buildings[0]);
        bool result = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(1));
    }

    
    /// <summary>
    /// Este test verifica que el jugador no puede construir un edificio si no tiene suficientes recursos.
    /// </summary>
    [Test]
    public void PlayerCannotBuildWithoutResources()
    {
        int x = 20;
        int y = 20;
        _player.Resources.Wood = 0;
        _player.Resources.Stone = 0;
        bool result = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(1));
    }
    
    
    /// <summary>
    /// Este test verifica que el jugador no puede construir un edificio con un nombre inválido.
    /// </summary>
    [Test]
    public void PlayerCannotBuildWithInvalidBuildingName()
    {
        int x = 15;
        int y = 15;
        _player.Resources.Wood = 1000;
        _player.Resources.Stone = 1000;
        bool result = _player.Actions.Build("EdificioInexistente", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("EdificioInexistente"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(1));
    }
    
    /// <summary>
    /// Este test verifica que el jugador no puede construir dos edificios en la misma posición del mapa.
    /// </summary>
    [Test]
    public void PlayerCannotBuildTwoBuildingsInSamePosition()
    {
        int x = 25;
        int y = 25;
        _player.Resources.Wood = 2000;
        _player.Resources.Stone = 2000;
        // Construye el primer edificio
        bool firstBuild = _player.Actions.Build("Barrack", (x, y)).Result;
        // Intenta construir otro edificio en la misma posición
        bool secondBuild = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(firstBuild, Is.True);
        Assert.That(secondBuild, Is.False);
        Assert.That(_player.Buildings.Count, Is.EqualTo(2));
    }
    
    /// <summary>
    /// Este test verifica que el jugador no puede construir un edificio en una posición negativa del mapa.
    /// </summary>
    [Test]
    public void PlayerCannotBuildWithNegativePosition()
    {
        int x = -5;
        int y = -10;
        _player.Resources.Wood = 1000;
        _player.Resources.Stone = 1000;
        bool result = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.Not.EqualTo("Bk"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(1));
    }
  
    
    /// <summary>
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
    
    /// <summary>
    /// Verifica que se puedan agregar recursos al centro cívico del jugador y que se actualicen correctamente los valores de recursos.
    /// </summary>
    [Test]
    public void PlayerCanAddResourcesToCivicCenter()
    {
        int initialWood = _civicCenter.Wood;
        int initialStone = _civicCenter.Stone;
        int initialFood = _civicCenter.Food;
        int initialGold = _civicCenter.Gold;

        _civicCenter.AddWood(50);
        _civicCenter.AddStone(30);
        _civicCenter.AddFood(20);
        _civicCenter.AddGold(10);

        Assert.That(_civicCenter.Wood, Is.EqualTo(initialWood + 50));
        Assert.That(_civicCenter.Stone, Is.EqualTo(initialStone + 30));
        Assert.That(_civicCenter.Food, Is.EqualTo(initialFood + 20));
        Assert.That(_civicCenter.Gold, Is.EqualTo(initialGold + 10));
    }
    
    public class BuildingTests
    {
        private Building _building;

        [SetUp]
        public void Setup()
        {
            // Creamos un edificio en la posición (5,10) con costos y tiempo de construcción
            _building = new Building((5, 10), woodCost: 100, stoneCost: 50, constructionTime: 60);
        }

        
        /// <summary>
        ///Verifica que los valores iniciales del edificio sean correctos.
        /// se inicializa correctamente los costos de madera y piedra, el tiempo de construcción es el esperado
        /// la posicion es correcta y el estado de construcción es falso.
        /// el tiempo transcurrido es 0 
        /// </summary>
        [Test]
        public void Building_InitialValues_AreCorrect()
        {
            Assert.That(_building.WoodCost, Is.EqualTo(100));
            Assert.That(_building.StoneCost, Is.EqualTo(50));
            Assert.That(_building.ConstructionTime, Is.EqualTo(60));
            Assert.That(_building.TimeElapsed, Is.EqualTo(0));
            Assert.That(_building.IsBuilt, Is.False);
            Assert.That(_building.Position["x"], Is.EqualTo(5));
            Assert.That(_building.Position["y"], Is.EqualTo(10));
            // Como Symbol es virtual y no está inicializado, puede ser null o vacío (depende de implementación)
        }

        /// <summary>
        /// Comprueba que el metodo Construyendo incrementa el tiempo transcurrido correctamente
        /// que no se supere el tiempo de construcción y que el estado de construcción se actualice correctamente.
        /// (IsBuilt=true cuando TimeElapsed >= ConstructionTime)
        /// </summary>
        [Test]
        public void Construyendo_IncrementsTimeElapsed()
        {
            _building.Construyendo(30);
            Assert.That(_building.TimeElapsed, Is.EqualTo(30));
            Assert.That(_building.IsBuilt, Is.False);

            _building.Construyendo(20);
            Assert.That(_building.TimeElapsed, Is.EqualTo(50));
            Assert.That(_building.IsBuilt, Is.False);

            _building.Construyendo(15); // Suma 15 pero no debe superar ConstructionTime
            Assert.That(_building.TimeElapsed, Is.EqualTo(60));
            Assert.That(_building.IsBuilt, Is.True);
        }

        /// <summary>
        /// Asegura que si el edificio ya está construido, el tiempo transcurrido no se incremente más.
        /// el estado de construcción no cambia.
        /// </summary>
        [Test]
        public void Construyendo_DoesNotIncrementAfterBuilt()
        {
            _building.Construyendo(60);
            Assert.That(_building.IsBuilt, Is.True);

            _building.Construyendo(10);
            // TimeElapsed no debe aumentar porque ya está construido
            Assert.That(_building.TimeElapsed, Is.EqualTo(60));
        }
    }


    // Clase concreta para testeo
    public class TestRecolection : Recolection
    {
        public TestRecolection((int x, int y) position, int cantidadinicial, int tasarecoleccion)
            : base(position, cantidadinicial, tasarecoleccion)
        {
        }
    }

    public class RecolectionTests
    {
        private TestRecolection _recolection;

        [SetUp]
        public void Setup()
        {
            _recolection = new TestRecolection((10, 20), cantidadinicial: 100, tasarecoleccion: 15);
        }
        /// <summary>
        /// Verifica que los valores iniciales de la recolección, posicion,cantidad de recurso disponible y tasa de recoleccion se establezcan correctamente.
        /// 
        /// </summary>

        [Test]
        public void InitialValues_AreSetCorrectly()
        {
            Assert.That(_recolection.Position["x"], Is.EqualTo(10));
            Assert.That(_recolection.Position["y"], Is.EqualTo(20));
            Assert.That(Recolection.CantidadRecursoDisponible, Is.EqualTo(100));
            Assert.That(Recolection.TasaDeRecoleccion, Is.EqualTo(15));
        }
        /// <summary>
        /// Confirma que al recolectar menor o igual cantidad que la disponible, se retorne la tasa de recolección y se actualice la cantidad de recurso disponible.
        /// </summary>
        [Test]
        public void Recolectar_ReturnsCorrectAmount_WhenCantidadIsLessOrEqualAvailable()
        {
            int cantidadSolicitada = 10;

            int recolectado = _recolection.Recolectar(cantidadSolicitada);

            Assert.That(recolectado, Is.EqualTo(Recolection.TasaDeRecoleccion));
            Assert.That(Recolection.CantidadRecursoDisponible, Is.EqualTo(100 - recolectado));
        }
        /// <summary>
        /// Verifica que al recolectar más cantidad que la disponible, se retorne la cantidad restante y se actualice a 0.
        /// </summary>
        [Test]
        public void Recolectar_ReturnsRemaining_WhenCantidadIsMoreThanAvailable()
        {
            int cantidadSolicitada = 200; // Más que disponible

            int recolectado = _recolection.Recolectar(cantidadSolicitada);

            Assert.That(recolectado, Is.EqualTo(100)); // debería recolectar lo que queda
            Assert.That(Recolection.CantidadRecursoDisponible, Is.EqualTo(0));
        }
        /// <summary>
        /// Asegura que tras varias veces de recolección, la cantidad de recurso disponible nunca sea negativa.
        /// </summary>
        [Test]
        public void CantidadRecursoDisponible_NeverNegative()
        {
            // Recolectar más veces de lo que hay
            _recolection.Recolectar(50); // -15
            _recolection.Recolectar(50); // -15
            _recolection.Recolectar(50); // -15
            _recolection.Recolectar(50); // -15
            _recolection.Recolectar(50); // -15
            _recolection.Recolectar(50); // Debería quedar 100 - 15*6 = 10
            _recolection.Recolectar(50); // -10 restantes

            Assert.That(Recolection.CantidadRecursoDisponible, Is.GreaterThanOrEqualTo(0));
        }
    }
  
    
    public class WoodsTests
    {
        private Woods _woods;

        [SetUp]
        public void Setup()
        {
            _woods = new Woods((5, 10), cantidadinicial: 300);
        }
        /// <summary>
        /// Verifica que los valores iniciales del bosque se establezcan correctamente, que la tasa de recolección sea 120 y la cantidad de recurso disponible sea 300.
        /// </summary>
        [Test]
        public void Woods_InitialValues_AreSetCorrectly()
        {
            Assert.That(_woods.Position["x"], Is.EqualTo(5));
            Assert.That(_woods.Position["y"], Is.EqualTo(10));
            Assert.That(Recolection.CantidadRecursoDisponible, Is.EqualTo(300));
            Assert.That(Recolection.TasaDeRecoleccion, Is.EqualTo(120));
        }
        /// <summary>
        /// Confirma que el símbolo del bosque sea "Wd", como se espera.
        /// </summary>
        [Test]
        public void Woods_Symbol_ReturnsWd()
        {
            Assert.That(Woods.Symbol, Is.EqualTo("🌳🌳"));
        }
        /// <summary>
        /// Testea que el metodo Recolectar del bosque funcione correctamente, recolectando la tasa de recolección y actualizando la cantidad de recurso disponible.
        /// </summary>
        [Test]
        public void Recolectar_Woods_ReturnsCorrectAmount()
        {
            int cantidadSolicitada = 50;
            int recolectado = _woods.Recolectar(cantidadSolicitada);

            Assert.That(recolectado, Is.EqualTo(Recolection.TasaDeRecoleccion));
            Assert.That(Recolection.CantidadRecursoDisponible, Is.EqualTo(300 - recolectado));
        }
    }
    [Test]
    public void AddWood_AddsCorrectly_WhenUnderCapacity_AndBuilt()
    {
        var player = new Player("Tester", "Cordobeses");
        var storage = new WoodStorage(player, (10, 10));
    
        // Simular que el edificio está terminado
        storage.Construyendo(999);

        storage.AddWood(200);

        Assert.That(storage.Wood, Is.EqualTo(200));
        Assert.That(player.Resources.Wood, Is.GreaterThanOrEqualTo(200)); // depende de lógica en AddResources
    }
    [Test]
    public void AddWood_ThrowsException_IfNotBuilt()
    {
        var player = new Player("Tester", "Cordobeses");
        var storage = new WoodStorage(player, (5, 5));

        // No lo construyo, así que IsBuilt debe ser false
        var ex = Assert.Throws<InvalidOperationException>(() => storage.AddWood(100));
        Assert.That(ex.Message, Is.EqualTo("El almacén aún no está construido."));
    }
    [Test]
    public void AddWood_DoesNotExceedCapacity()
    {
        var player = new Player("Tester", "Cordobeses");
        var storage = new WoodStorage(player, (1, 1));
        storage.Construyendo(999); // simula edificio terminado

        storage.AddWood(1200); // mayor que capacidad

        Assert.That(storage.Wood, Is.EqualTo(1000));
    }
    


}






