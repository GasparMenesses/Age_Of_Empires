namespace Library;

public class Fachada
{
    // La fachada es la clase que se encarga de la creacion de una nueva partida,
    // llama todas las funciones necesarias para la creacion de un entorno
    // verificando por ejemplo, al momento de crear algo, que no haya ya algo en ese punto del mapa
    
    // QUE HACE?
    // Agrupa subsistemas complejos.
    // Oculta detalles internos como qué clase hace qué.
    // Permite a quien use el sistema (controlador, UI, inteligencia artificial) trabajar con un punto de acceso simple.
    
    // DE QUE SE ENCARGA?
    // Coordina llamadas entre distintos sistemas del juego.
    // Reduce el acoplamiento entre el código del "jugador" y las clases internas.
    // Hace que el código sea más limpio, fácil de mantener y de testear.


    public void CreateNewGame()
    {
        
    }
}