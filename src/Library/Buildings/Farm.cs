namespace Library.Buildings;
using System.Timers;

public class Farm : Building
{
    private readonly Timer _timer; //se crea un temporizador para que se genere comida cada cierto intervalo
    private readonly Mill _mill;
    private int Comidagenerada = 10; //cantidad predeterminada de comida generada (puede ser cambiada)
    public event Action<int> GeneraciondeComida; 
    private void GenerateFood(object sender, ElapsedEventArgs e) 
    {
        
        _mill.AlmacenarComida(Comidagenerada);
        GeneraciondeComida?.Invoke(Comidagenerada); //avisa al jugador que se generó determinada cantidad de comida, si se 
        //elimina esta línea, seguiría generando la comida pero no nos enteraríamos
    }

    public Farm(Mill mill, Resources resources): base(resources,200,120,180) // este constructor configura la granja con el mill
    {
        _mill = mill;
        _timer = new Timer(30000); // crea un temporizador que cada 30s genera comida (se puede cambiar el intervalo)
        _timer.Elapsed += GenerateFood;
        _timer.AutoReset = true; // esto hace que el temporizador se repita indefinidamente (durante toda la partida)
        _timer.Enabled = true;
    }
    public void Start()
    {
        _timer.Start();     //metodo para que no empiece a generar comida antes de que este construida, en main hay que llamar a Start
    }
    public void Stop()
    {
        _timer.Stop();  //metodo para finalizar la producciion de comida, si se destruye la granja o finaliza la partida
    }
}   