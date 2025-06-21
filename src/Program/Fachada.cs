using System;

public class Fachada
{
    private Engine motor;

    public Fachada()
    {
        motor = new Engine();
    }

    public void CrearJugadores()
    {
        motor.CrearJugadores();
    }

    public void IniciarJuego()
    {
        motor.CreateNewGameMap();
        motor.EmpezarLoop();
    }
}