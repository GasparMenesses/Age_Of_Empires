namespace Library.Buildings;
using System.Timers;

public class Farm
{
    private readonly Timer _timer; //se crea un temporizador para que se genere comida cada cierto intervalo
    private readonly Mill _mill;
    private int Comidagenerada = 10; //cantidad predeterminada de comida generada (puede ser cambiada)
    public event Action<int> GeneraciondeComida; 
    private void GenerateFood(object sender, ElapsedEventArgs e) 
    {
        _mill.AlmacenarComida(Comidagenerada);
        GeneraciondeComida?.Invoke(Comidagenerada);
    }

    public Farm(Mill mill) // este constructor configura la granja con el mill
    {
        _mill = mill;
        _timer = new Timer(30000); // crea un temporizador que cada 30s genera comida (se puede cambiar el intervalo)
        _timer.Elapsed += GenerateFood;
        _timer.AutoReset = true; // esto hace que el temporizador se repita indefinidamente (durante toda la partida)
        _timer.Enabled = true;
    }
}   