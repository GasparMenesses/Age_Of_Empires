namespace Library.Buildings;
using Core;
public class GoldStorage : CivicCenter
{
    
    public GoldStorage(Player player) : base(player) //base le pasa a CC el  player
    {
        Gold=0 ;
        
    }
    
}

