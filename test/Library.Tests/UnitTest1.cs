using Library.Core;
using Library.Buildings;
using Library.Farming;
using Library.Units;
using Library;


namespace Library.Tests;

public class Tests
{
    private Player _player;
    private CivicCenter _civicCenter;
    private Engine _engine;

    [SetUp]
    public void Setup()
    {
    
        _player = new Player("MiniMago", "Cordobeses");
        _civicCenter = _player.Buildings.Keys.FirstOrDefault(b => b is CivicCenter) as CivicCenter;
        _engine = new Engine();
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
        var rand = new Random();
        int x, y;
        do
        {
            x = rand.Next(1, 100);
            y = rand.Next(1, 100);
        } while (Map.CheckMap(x,y) != "..");
        _player.Buildings[_player.Buildings.Keys.First()] = (x,y);
        Assert.That(_player.Buildings[_civicCenter].x, Is.Not.GreaterThanOrEqualTo(99));
        Assert.That(_player.Buildings[_civicCenter].y, Is.Not.GreaterThanOrEqualTo(99));
        Assert.That(_player.Buildings[_civicCenter].x, Is.Not.LessThanOrEqualTo(0));
        Assert.That(_player.Buildings[_civicCenter].y, Is.Not.LessThanOrEqualTo(0));
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

        Assert.That(Map.CheckMap(x, y), Is.EqualTo("🏯⚔️"));
        Assert.That(_player.Buildings.Count, Is.EqualTo(2));

        // Buscar el Barrack recién construido
        var barrack = _player.Buildings.Keys.FirstOrDefault(b => b is Barrack) as Barrack;
        Assert.That(barrack, Is.Not.Null);
        Assert.That(barrack.Symbol, Is.EqualTo("🏯⚔️"));

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
        var existingBuilding = _player.Buildings.Keys.First();
        Map.ChangeMap((10, 10), existingBuilding.Symbol);
        int originalBuildingCount = _player.Buildings.Count;
        bool result = _player.Actions.Build("Barrack", (x, y)).Result;
        Assert.That(result, Is.False);
        Assert.That(Map.CheckMap(x, y), Is.EqualTo(existingBuilding.Symbol));
        Assert.That(_player.Buildings.Count, Is.EqualTo(originalBuildingCount));
        var newBarrack = _player.Buildings.Keys.FirstOrDefault(b => b is Barrack);
        Assert.That(newBarrack, Is.Null);
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

        _civicCenter.AddWood(_player,10);
        _civicCenter.AddStone(_player,20);
        _civicCenter.AddFood(_player,30);
        _civicCenter.AddGold(_player,10);

        Assert.That(_civicCenter.Wood, Is.EqualTo(initialWood + 50));
        Assert.That(_civicCenter.Stone, Is.EqualTo(initialStone + 30));
        Assert.That(_civicCenter.Food, Is.EqualTo(initialFood + 20));
        Assert.That(_civicCenter.Gold, Is.EqualTo(initialGold + 10));
    }

    public class BuildingTests
    {
        private Building _building;
        private Engine _engine;
        private Player _player;
        [SetUp]
        public void Setup()
        {
            // Creamos un edificio en la posición (5,10) con costos y tiempo de construcción
            _player = new Player("MiniMago", "Cordobeses");
            _engine = new Engine();
            _building = new Building(100, 50, 60, 100);
        }


        /// <summary>
        ///Verifica que los valores iniciales del edificio sean correctos.
        /// se inicializa correctamente los costos de madera y piedra, el tiempo de construcción es el esperado
        /// la posicion es correcta y el estado de construcción es falso.
        /// el tiempo transcurrido es 0 
        /// </summary>
        /// <summary>
        /// Verifica que los valores iniciales del edificio sean correctos:
        /// costos de recursos, tiempo de construcción, estado inicial, y posición.
        /// </summary>
        [Test]
        public void Building_InitialValues_AreCorrect()
        {
            Assert.That(_building.WoodCost, Is.EqualTo(100));
            Assert.That(_building.StoneCost, Is.EqualTo(50));
            Assert.That(_building.ConstructionTime, Is.EqualTo(60));
            Assert.That(_building.TimeElapsed, Is.EqualTo(0));
            Assert.That(_building.IsBuilt, Is.False);

            var position = _player.Buildings[_building];
            Assert.That(position.x, Is.EqualTo(5));
            Assert.That(position.y, Is.EqualTo(10));
            
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

        [Test]
        public void Health_CanBeSetToDifferentValues()
        {
            var building = new Building(320, 150, 50, 100);
            Assert.That(building.Health, Is.EqualTo(150));

            building.Health = 80;
            Assert.That(building.Health, Is.EqualTo(80));
        }

        [Test]
        public void Construyendo_WithZeroSeconds_DoesNotChangeTimeElapsed()
        {
            var building = new Building(0, 100, 50, 60);
            building.Construyendo(0);
            Assert.That(building.TimeElapsed, Is.EqualTo(0));
            Assert.That(building.IsBuilt, Is.False);
        }

        [Test]
        public void Construyendo_WithNegativeSeconds_DoesNotDecreaseTimeElapsed()
        {
            var building = new Building(0, 100, 50, 60);
            building.Construyendo(30);
            building.Construyendo(-10); // Verifica que no disminuya el tiempo transcurrido

            Assert.That(building.TimeElapsed, Is.EqualTo(30));
        }
        [Test]
        public void Construyendo_DoesNotIncreaseAfterBuilt()
        {
            var building = new Building(0, 100, 60, 100);


            Assert.That(building.IsBuilt, Is.False);

            building.Construyendo(60);

            Assert.That(building.IsBuilt, Is.True);
            Assert.That(building.TimeElapsed, Is.EqualTo(60));

            building.Construyendo(10);

            Assert.That(building.TimeElapsed, Is.EqualTo(60));
        }




        private class TestBuilding : Building
        {
            public override string Symbol { get; set; } = "TB";

            public TestBuilding() : base(0, 0, 0, 1)
            {
            }
        }

        [Test]
        public void Symbol_CanBeOverridden()
        {
            var testBuilding = new TestBuilding();
            Assert.That(testBuilding.Symbol, Is.EqualTo("TB"));

            testBuilding.Symbol = "XX";
            Assert.That(testBuilding.Symbol, Is.EqualTo("XX"));
        }


    }



    public class TestRecolection : Recolection
    {
        public TestRecolection((int x, int y) position, int cantidadinicial, int tasarecoleccion)
            : base(10,120)
        {
        }
    }

    public class RecolectionTests
    {
        private Facade.Fachada _fachada;
        private TestRecolection _recolection;
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _fachada = new Facade.Fachada();
            _player = new Player("MiniMago", "Cordobeses");
            _recolection = new TestRecolection((10, 20), 100, 15);
        }

        /// <summary>
        /// Verifica que los valores iniciales de la recolección, posicion,cantidad de recurso disponible y tasa de recoleccion se establezcan correctamente.
        /// 
        /// </summary>

        [Test]
        public void InitialValues_AreSetCorrectly()
        {
            Assert.That(_fachada.recolection[_recolection].x, Is.EqualTo(10));
            Assert.That(_fachada.recolection[_recolection].y, Is.EqualTo(20));
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
        private Facade.Fachada _fachada;
        private TestRecolection _recolection;
        private Player _player;
        private Woods _woods;

        [SetUp]
        public void Setup()
        {
            _woods = new Woods((5, 10), cantidadinicial: 300);
            _player = new Player("MiniMago", "Cordobeses");
            _fachada = new Facade.Fachada();
            _recolection = new TestRecolection((5, 10), cantidadinicial: 300, tasarecoleccion: 120);
            
        }

        /// <summary>
        /// Verifica que los valores iniciales del bosque se establezcan correctamente, que la tasa de recolección sea 120 y la cantidad de recurso disponible sea 300.
        /// </summary>
        [Test]
        public void Woods_InitialValues_AreSetCorrectly()
        {
            Assert.That(_fachada.recolection[_recolection].x, Is.EqualTo(5));
            Assert.That(_fachada.recolection[_recolection].y, Is.EqualTo(10));
            Assert.That(Recolection.CantidadRecursoDisponible, Is.EqualTo(300));
            Assert.That(Recolection.TasaDeRecoleccion, Is.EqualTo(120));
        }

        /// <summary>
        /// Confirma que el símbolo del bosque sea "🌳🌳", como se espera.
        /// </summary>
        [Test]
        public void Woods_Symbol_ReturnsWd()
        {
            Assert.That(_woods.Symbol, Is.EqualTo("🌳🌳"));
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

    [TestFixture]
    public class WoodStorageTests
    {
        private Player _player;
        private WoodStorage _storage;
        private Facade.Fachada _fachada;

        [SetUp]
        public void Setup()
        {
            _storage = new WoodStorage(_player, (10, 10));
            _player = new Player("MiniMago", "Cordobeses");
            _fachada = new Facade.Fachada();
        }

        /// <summary>
        /// Verifica que el almacén se construya correctamente con los valores esperados.
        /// </summary>
        [Test]
        public void WoodStorage_ConstructedCorrectly()
        {
            Assert.That(_player.Buildings[_storage].x, Is.EqualTo(10));
            Assert.That(_player.Buildings[_storage].y, Is.EqualTo(10));
            Assert.That(_storage.Wood, Is.EqualTo(0));
            Assert.That(_storage.Capacity, Is.EqualTo(1000));
            Assert.That(_storage.Symbol, Is.EqualTo("🪵🏚️"));
            Assert.That(_player.Buildings.ContainsKey(_storage), Is.True);
        }

        /// <summary>
        /// Verifica que se pueda agregar madera correctamente si el almacén está construido y bajo la capacidad.
        /// </summary>
        [Test]
        public void AddWood_AddsCorrectly_WhenBuilt_AndUnderCapacity()
        {
            _storage.Construyendo(999); // simula edificio construido
            _storage.AddWood(500);

            Assert.That(_storage.Wood, Is.EqualTo(500));
            Assert.That(_player.Resources.Wood, Is.GreaterThanOrEqualTo(500));
        }

        /// <summary>
        /// Verifica que no se pueda agregar madera si el edificio no está construido.
        /// </summary>
        [Test]
        public void AddWood_ThrowsException_WhenNotBuilt()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _storage.AddWood(100));
            Assert.That(ex.Message, Is.EqualTo("El almacén aún no está construido."));
        }

        /// <summary>
        /// Verifica que no se exceda la capacidad máxima del almacén al agregar madera.
        /// </summary>
        [Test]
        public void AddWood_DoesNotExceedCapacity()
        {
            _storage.Construyendo(999); // Construido
            _storage.AddWood(1200);

            Assert.That(_storage.Wood, Is.EqualTo(1000)); // capacidad máxima
            Assert.That(_player.Resources.Wood, Is.GreaterThanOrEqualTo(1000));
        }

        /// <summary>
        /// Verifica que agregar 0 de madera no afecte el estado del almacén ni los recursos.
        /// </summary>
        [Test]
        public void AddWood_WithZero_DoesNothing()
        {
            _storage.Construyendo(999);
            int maderaAntes = _storage.Wood;
            int recursoAntes = _player.Resources.Wood;

            _storage.AddWood(0);

            Assert.That(_storage.Wood, Is.EqualTo(maderaAntes));
            Assert.That(_player.Resources.Wood, Is.EqualTo(recursoAntes));
        }
    }

    [TestFixture]
    public class HouseTests
    {
        private Player _player;
        private House _house;
        private Engine _engine;
        [SetUp]
        public void Setup()
        {
            _player = new Player("MiniMago", "Cordobeses");
            _engine = new Engine();
            _house = new House(_player, (15, 20));
        }

        /// <summary>
        /// Verifica que la casa se cree correctamente con los valores esperados.
        /// </summary>
        [Test]
        public void House_Constructed_Correctly()
        {
            
            Assert.That(_house.WoodCost, Is.EqualTo(0));
            Assert.That(_house.StoneCost, Is.EqualTo(0));
            Assert.That(_house.ConstructionTime, Is.EqualTo(60));
            Assert.That(_house.Symbol, Is.EqualTo("🏠🏠"));
        }

        /// <summary>
        /// Verifica que al usar AumentarPoblacionLimite, el jugador obtiene +4 de población límite.
        /// </summary>
        [Test]
        public void AumentarPoblacionLimite_AddsFourToPlayerLimit()
        {
            int poblacionAntes = _player.PoblacionLimite;

            _house.AumentarPoblacionLimite(_player);

            Assert.That(_player.PoblacionLimite, Is.EqualTo(poblacionAntes + 4));
        }

        /// <summary>
        /// Verifica que se pueda avanzar la construcción de la casa correctamente.
        /// </summary>
        [Test]
        public void Construyendo_AvancesTimeCorrectly()
        {
            _house.Construyendo(30);
            Assert.That(_house.TimeElapsed, Is.EqualTo(30));
            Assert.That(_house.IsBuilt, Is.False);

            _house.Construyendo(40); // excede el tiempo
            Assert.That(_house.TimeElapsed, Is.EqualTo(60));
            Assert.That(_house.IsBuilt, Is.True);
        }

        /// <summary>
        /// Verifica que no se incremente más el tiempo si la casa ya está construida.
        /// </summary>
        [Test]
        public void Construyendo_NoEffectAfterBuilt()
        {
            _house.Construyendo(60);
            Assert.That(_house.IsBuilt, Is.True);

            _house.Construyendo(20); // no debe cambiar
            Assert.That(_house.TimeElapsed, Is.EqualTo(60));
        }

    }

    [TestFixture]
    public class BarrackTests
    {
        private Player _player;
        private Barrack _barrack;
        private Engine _engine;
        private Facade.Fachada _fachada;

        [SetUp]
        public void Setup()
        {
            _player = new Player("MiniMago", "Cordobeses");
            _player.Resources.Food = 1000; // Para permitir entrenamiento
            _barrack = new Barrack(_player, (10, 10));
            _fachada = new Facade.Fachada();
        }

        /// <summary>
        /// Verifica que el cuartel se cree correctamente con su símbolo y posición.
        /// </summary>
        [Test]
        public void Barrack_Constructed_Correctly()
        {
            Assert.That(_player.Buildings[_barrack].x, Is.EqualTo(10));
            Assert.That(_player.Buildings[_barrack].y, Is.EqualTo(10));
            Assert.That(_barrack.WoodCost, Is.EqualTo(25));
            Assert.That(_barrack.StoneCost, Is.EqualTo(55));
            Assert.That(_barrack.ConstructionTime, Is.EqualTo(30));
            Assert.That(_barrack.Symbol, Is.EqualTo("🏯⚔️"));
            Assert.That(_barrack.Unit.ContainsKey("Archer"));
            Assert.That(_barrack.Unit.ContainsKey("Cavalry"));
            Assert.That(_barrack.Unit.ContainsKey("Infantry"));
        }

        /// <summary>
        /// Verifica que se entrenen correctamente unidades normales (Archer) y se resten los recursos de comida.
        /// </summary>
        [Test]
        public void TrainingUnit_ValidUnits_TrainsCorrectly()
        {
            int cantidad = 3;
            int costoUnitario = _barrack.Unit["Archer"].Cost;
            int comidaAntes = _player.Resources.Food;

            _barrack.TrainingUnit("Archer", cantidad);

            Assert.That(_player.Units.Count, Is.EqualTo(cantidad));
            Assert.That(_player.Resources.Food, Is.EqualTo(comidaAntes - (costoUnitario * cantidad)));
            Assert.That(_player.Units.All(u => u is Archer), Is.True);
        }

        /// <summary>
        /// Verifica que no se entrene ninguna unidad si no hay suficiente comida.
        /// </summary>
        [Test]
        public void TrainingUnit_NotEnoughFood_DoesNotTrain()
        {
            _player.Resources.Food = 0;

            _barrack.TrainingUnit("Cavalry", 2);

            Assert.That(_player.Units.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Verifica que se entrenen unidades únicas (ej: JulioCesar) correctamente según la civilización.
        /// </summary>
        [Test]
        public void TrainingUnit_UniqueUnits_TrainsCorrectly()
        {
            _player.Resources.Food = 1000;
            _barrack.TrainingUnit("JulioCesar", 2);

            Assert.That(_player.Units.Count, Is.EqualTo(2));
            Assert.That(_player.Units.All(u => u is JulioCesar), Is.True);
        }
    }

    [TestFixture]
    public class MillTests
    {
        private Player _player;
        private Mill _mill;
        private Engine _engine;
        private Facade.Fachada _fachada;

        [SetUp]
        public void Setup()
        {
            _mill = new Mill(_player, (15, 20));
            _player = new Player("MiniMago", "Cordobeses");
            _fachada = new Facade.Fachada();
        }

        /// <summary>
        /// Verifica que los valores iniciales del molino se asignen correctamente.
        /// </summary>
        [Test]
        public void Mill_InitialValues_AreCorrect()
        {
            Assert.That(_player.Buildings[_mill].x, Is.EqualTo(15));
            Assert.That(_player.Buildings[_mill].y, Is.EqualTo(20));
            Assert.That(_mill.Food, Is.EqualTo(0));
            Assert.That(_mill.Capacity, Is.EqualTo(1000));
            Assert.That(_player.Buildings.ContainsKey(_mill), Is.True);
            Assert.That(_mill.Symbol, Is.EqualTo("🌾🏠"));
        }

        /// <summary>
        /// Verifica que al agregar comida se actualicen los recursos del molino y del jugador correctamente.
        /// </summary>
        [Test]
        public void AddFood_AddsCorrectly_WhenBuilt()
        {
            _mill.Construyendo(999); // Simula edificio construido

            _mill.AddFood(300);

            Assert.That(_mill.Food, Is.EqualTo(300));
            Assert.That(_player.Resources.Food, Is.GreaterThanOrEqualTo(300));
        }

        /// <summary>
        /// Verifica que no se pueda agregar comida si el edificio no está construido.
        /// </summary>
        [Test]
        public void AddFood_ThrowsException_IfNotBuilt()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _mill.AddFood(100));
            Assert.That(ex.Message, Is.EqualTo("El almacén aún no está construido."));
        }

        /// <summary>
        /// Verifica que no se pueda exceder la capacidad máxima del molino.
        /// </summary>
        [Test]
        public void AddFood_DoesNotExceedCapacity()
        {
            _mill.Construyendo(999); // Construcción simulada

            _mill.AddFood(1200); // Más que la capacidad

            Assert.That(_mill.Food, Is.EqualTo(1000));
            Assert.That(_player.Resources.Food, Is.GreaterThanOrEqualTo(1000));
        }
    }

    [TestFixture]
    public class GoldStorageTests
    {
        private Player _player;
        private GoldStorage _goldStorage;
        private Facade.Fachada _fachada;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Tester", "Cordobeses");
            _goldStorage = new GoldStorage(_player, (3, 7));
            _fachada = new Facade.Fachada();
        }

        /// <summary>
        /// Verifica que los valores iniciales del almacén de oro se establezcan correctamente.
        /// </summary>
        [Test]
        public void GoldStorage_InitialValues_AreCorrect()
        {
            Assert.That(_player.Buildings[_goldStorage].x, Is.EqualTo(3));
            Assert.That(_player.Buildings[_goldStorage].y, Is.EqualTo(7));
            Assert.That(_goldStorage.Gold, Is.EqualTo(0));
            Assert.That(_goldStorage.Capacity, Is.EqualTo(1000));
            Assert.That(_player.Buildings.ContainsKey(_goldStorage), Is.True);
            Assert.That(_goldStorage.Symbol, Is.EqualTo("💰"));
        }

        /// <summary>
        /// Verifica que se pueda agregar oro correctamente si el edificio está construido.
        /// </summary>
        [Test]
        public void AddGold_AddsCorrectly_WhenBuilt()
        {
            _goldStorage.Construyendo(999); // Simula edificio construido

            _goldStorage.AddGold(450);

            Assert.That(_goldStorage.Gold, Is.EqualTo(450));
            Assert.That(_player.Resources.Gold, Is.GreaterThanOrEqualTo(450));
        }

        /// <summary>
        /// Verifica que lanzar una excepción si se intenta almacenar oro antes de que el edificio esté construido.
        /// </summary>
        [Test]
        public void AddGold_ThrowsException_IfNotBuilt()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _goldStorage.AddGold(100));
            Assert.That(ex.Message, Is.EqualTo("El almacén aún no está construido."));
        }

        /// <summary>
        /// Verifica que no se pueda exceder la capacidad máxima del almacén de oro.
        /// </summary>
        [Test]
        public void AddGold_DoesNotExceedCapacity()
        {
            _goldStorage.Construyendo(999); // Simula que ya está construido

            _goldStorage.AddGold(1200); // Excede capacidad

            Assert.That(_goldStorage.Gold, Is.EqualTo(1000));
            Assert.That(_player.Resources.Gold, Is.GreaterThanOrEqualTo(1000));
        }

        /// <summary>
        /// Verifica comportamiento al llegar justo a la capacidad con múltiples llamadas a AddGold.
        /// </summary>
        [Test]
        public void AddGold_ReachesExactCapacityWithMultipleCalls()
        {
            _goldStorage.Construyendo(999);

            _goldStorage.AddGold(600);
            _goldStorage.AddGold(400);

            Assert.That(_goldStorage.Gold, Is.EqualTo(1000));
            Assert.That(_player.Resources.Gold, Is.GreaterThanOrEqualTo(1000));
        }
    }

    [TestFixture]
    public class StoneStorageTests
    {
        private Player _player;
        private StoneStorage _stoneStorage;
        private Facade.Fachada _fachada;

        [SetUp]
        public void Setup()
        {
            _stoneStorage = new StoneStorage(_player, (4, 8));
            _player = new Player("MiniMago", "Cordobeses");
            _fachada = new Facade.Fachada();
            
        }

        /// <summary>
        /// Verifica que los valores iniciales del almacén de piedra se establezcan correctamente:
        /// posición, cantidad inicial de piedra, capacidad, símbolo y que se haya agregado al jugador.
        /// </summary>
        [Test]
        public void StoneStorage_InitialValues_AreCorrect()
        {
            Assert.That(_player.Buildings[_stoneStorage].x, Is.EqualTo(4));
            Assert.That(_player.Buildings[_stoneStorage].y, Is.EqualTo(8));
            Assert.That(_stoneStorage.Stone, Is.EqualTo(0));
            Assert.That(_stoneStorage.Capacity, Is.EqualTo(1000));
            Assert.That(_player.Buildings.ContainsKey(_stoneStorage), Is.True);
            Assert.That(_stoneStorage.Symbol, Is.EqualTo("🪨🏚️"));
            Assert.That(StoneStorage.StoneCost, Is.EqualTo(55));
        }

        /// <summary>
        /// Verifica que se pueda agregar piedra al almacén correctamente cuando ya está construido.
        /// </summary>
        [Test]
        public void AddStone_AddsCorrectly_WhenBuilt()
        {
            _stoneStorage.Construyendo(999); // Simula edificio construido

            _stoneStorage.AddStone(500);

            Assert.That(_stoneStorage.Stone, Is.EqualTo(500));
            Assert.That(_player.Resources.Stone, Is.GreaterThanOrEqualTo(500));
        }

        /// <summary>
        /// Verifica que se lance una excepción si se intenta agregar piedra antes de que el edificio esté construido.
        /// </summary>
        [Test]
        public void AddStone_ThrowsException_IfNotBuilt()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _stoneStorage.AddStone(100));
            Assert.That(ex.Message, Is.EqualTo("El almacén aún no está construido."));
        }

        /// <summary>
        /// Verifica que no se pueda exceder la capacidad máxima del almacén al agregar piedra.
        /// </summary>
        [Test]
        public void AddStone_DoesNotExceedCapacity()
        {
            _stoneStorage.Construyendo(999); // Simula que ya está construido

            _stoneStorage.AddStone(1200); // Excede capacidad

            Assert.That(_stoneStorage.Stone, Is.EqualTo(1000));
            Assert.That(_player.Resources.Stone, Is.GreaterThanOrEqualTo(1000));
        }

        /// <summary>
        /// Verifica que múltiples llamadas a AddStone no superen la capacidad y sumen correctamente.
        /// </summary>
        [Test]
        public void AddStone_ReachesExactCapacityWithMultipleCalls()
        {
            _stoneStorage.Construyendo(999);

            _stoneStorage.AddStone(600);
            _stoneStorage.AddStone(400);

            Assert.That(_stoneStorage.Stone, Is.EqualTo(1000));
            Assert.That(_player.Resources.Stone, Is.GreaterThanOrEqualTo(1000));
        }
    }

    [TestFixture]
    public class QuarryTests
    {
        private Player _player;
        private Facade.Fachada _fachada;
        private Quarry _quarry;
        /// <summary>
        /// Verifica que la propiedad estática Symbol devuelve el símbolo correcto.
        /// </summary>
        [Test]
        public void Symbol_ReturnsCorrectValue()
        {
            Assert.AreEqual("⛏️🪨", _quarry.Symbol);
        }

        /// <summary>
        /// Verifica que el constructor inicializa correctamente la posición y la cantidad inicial.
        /// </summary>
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var posicion = (3, 4);
            int cantidadInicial = 500;
            _quarry = new Quarry(posicion, cantidadInicial);
            _fachada = new Facade.Fachada();
            _player = new Player("MiniMago", "Cordobeses");

            Assert.IsNotNull(_fachada.recolection[_quarry].x);
            Assert.IsNotNull(_fachada.recolection[_quarry].y);
            Assert.AreEqual(3, _fachada.recolection[_quarry].x);
            Assert.AreEqual(4, _fachada.recolection[_quarry].y);
            // Si la clase base expone la cantidad, aquí se podría verificar también.
        }

        /// <summary>
        /// Verifica que la propiedad Position se puede establecer y recuperar correctamente.
        /// </summary>
        [Test]
        public void Position_SetAndGet_WorksCorrectly()
        {
            _quarry = new Quarry((1, 2), 100);
            var nuevaPosicion = (x: 10, y: 20);
            _fachada.recolection[_quarry] = nuevaPosicion;
            ;

            _fachada.recolection[_quarry] = nuevaPosicion;

            Assert.AreEqual(10, _fachada.recolection[_quarry].x);
            Assert.AreEqual(20, _fachada.recolection[_quarry].y);
        }
    }

    [TestFixture]
    public class GoldMineTests
    {
        private Player _player;
        private Facade.Fachada _fachada;
        private Recolection _goldMine;
        /// <summary>
        /// Verifica que la propiedad estática Symbol devuelve el símbolo correcto.
        /// </summary>
        [Test]
        public void Symbol_ReturnsCorrectValue()
        {
            Assert.AreEqual("⛏️💰", _goldMine.Symbol);
        }

        /// <summary>
        /// Verifica que el constructor inicializa correctamente la posición y la cantidad inicial.
        /// </summary>
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var posicion = (6, 7);
            int cantidadInicial = 400;
            _goldMine = new GoldMine(posicion, cantidadInicial);

            Assert.IsNotNull(_fachada.recolection[_goldMine].x);
            Assert.IsNotNull(_fachada.recolection[_goldMine].y);
            Assert.AreEqual(6, _fachada.recolection[_goldMine].x);
            Assert.AreEqual(7, _fachada.recolection[_goldMine].y);
            // Si la clase base expone la cantidad, aquí se podría verificar también.
        }

        /// <summary>
        /// Verifica que la propiedad Position se puede establecer y recuperar correctamente.
        /// </summary>
        [Test]
        public void Position_SetAndGet_WorksCorrectly()
        {
            var goldMine = new GoldMine((2, 3), 150);
            var nuevaPosicion = (x: 12, y: 21);

            _fachada.recolection[_goldMine] = nuevaPosicion;

            Assert.AreEqual(12, _fachada.recolection[_goldMine].x);
            Assert.AreEqual(21, _fachada.recolection[_goldMine].y);
        }
    }

    [TestFixture]
    public class FarmTests
    {
        
        private Player _player;
        private Facade.Fachada _fachada;
        private Farm _farm;
        /// <summary>
        /// Verifica que la propiedad estática Symbol devuelve el símbolo correcto.
        /// </summary>
        [Test]
        public void Symbol_ReturnsCorrectValue()
        {
            Assert.AreEqual("\ud83c\udf3e\ud83c\udf3e", _farm.Symbol);
        }

        /// <summary>
        /// Verifica que el constructor inicializa correctamente la posición y la cantidad inicial.
        /// </summary>
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.IsNotNull(_fachada.recolection[_farm]);
            Assert.AreEqual(8, _fachada.recolection[_farm].x);
            Assert.AreEqual(9, _fachada.recolection[_farm].y);
        }

      

    }
}
    








