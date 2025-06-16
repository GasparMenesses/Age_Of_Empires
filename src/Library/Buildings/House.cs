using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Library.Buildings;

public class House:Building
{
    public House(Resources resources) : base(resources, 500, 250, 180)
    {
        
    }
}