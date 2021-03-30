using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Directii
    {
       

        Stanga,
        Dreapta,
        Sus,
        Jos
    };

    class Setari
    {
        public static int Latime  , Inaltime,  Viteza,  Scor, Puncte;
        public static bool GameOver;
        public static Directii directie;

        public Setari()
        {
            
            Latime = 16; 
            Inaltime = 16; 
            Viteza = 20; 
            Scor = 0; 
            Puncte = 100; 
            GameOver = false; 
            directie = Directii.Jos; 
        }
    }
}
