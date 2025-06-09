using System.Net.Sockets;
using System.Xml;
using Library.Interfaces;

namespace Library.Buildings;
using Core;

public class WoodStorage : CivicCenter
{
    public WoodStorage(Player player) : base(player) //base le pasa a CC el  player
    {
        
    }

}