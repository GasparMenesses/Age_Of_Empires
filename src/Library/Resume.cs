using System;
using System.Collections.Generic; 
using System.IO; //para escribir archivos txt
namespace Library;

public class Resume
{
    private List<string> events = new List<string>(); //lista para guardar todos los eventos registrados durante la partida
    private readonly string filepath = $"Resumen {DateTime.Now:yyyy MMMM dd}.txt"; 
    //ruta del archivo donde se guardarán los eventos, se le pone fecha y hora para no sobrescribir archivos anteriores

    /// <summary>
    ///   Agrega un evento a la lista y lo escribe en el archivo .txt
    /// </summary>
    public void AddEvent(string accion, string jugador)
    {
        string line = $"{DateTime.Now:HH:mm:ss} - ({jugador}) , {accion}";
        events.Add(line);
        File.AppendAllText(filepath, line + Environment.NewLine);
    }
    /// <summary>
    /// Devuelve el resumen completo como un solo string    
    /// </summary>
    
    public string GetSummary()
    {
        return string.Join(Environment.NewLine, events);
    }
    /// <summary>
    /// Borra los eventos de la lista, elimina el archivo .txt si existe. (por si se reinicia la partida)
    /// 
    /// </summary>
    public void Clear()
    {
        events.Clear();
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
    }
}