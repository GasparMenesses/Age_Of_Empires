namespace Library.Buildings;

public class Barrack:Building 
{
    
    public static string Symbol => "Bk";
 

    public Barrack((int x, int y) posicion, Resources resources ): base(resources, 500,300,180,posicion)
    {
       
    }

}