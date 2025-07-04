﻿using Library.Core;
namespace Library.Buildings;

// Esta clase representa un edificio de tipo "Mill" (molino) en el juego, que almacena recursos de comida.
// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el molino y el almacenamiento de comida.

public class Mill : Building //herdea de la clase building
{
    public override string Symbol => "Ml";
    public int Food { get; private set; } //propiedad que define la cantidad de food almacenado
    public int Capacity { get; set; }
    public Player _player;
    

    public Mill(Player player,(int x, int y)position) :
        base(position, woodCost:25, stoneCost:55,constructionTime:30) //constructor que define los costos de construccion del almacén, gastando piedra y madera.
    //Tambien define el tiempo que demora
    {
        _player = player;
        Food = 0; //inicializa la cantidad de food almacenado en 0
        Capacity = 1000; //define la capacidad del almacén
        player.Resources.AddLimitResources(food: true); //aumenta el limite de food en 1000
        player.Buildings.Add(this); //agrega el edificio al jugador
    }
    public void AddFood(int food)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        if ((Food + food) > Capacity)
        {
            food = Capacity - Food;
            Food = Capacity;
        }
        else
            Food += food;
        _player.Resources.AddResources(food: food);
    }
}
