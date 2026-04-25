using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BriscolaPokemon.Core;

public class Carta
{
    public int Valore { get; set; }
    public Seme Seme { get; set; }


    public int GetPunti()
    {
        if (Valore == 1) return 11;
        if (Valore == 3) return 10;
        if (Valore == 10) return 4;
        if (Valore == 9) return 3;
        if (Valore == 8) return 2;
        return 0;
    }

    // Restituisce il nome del file immagine, es. "Coppe_1.png"
    public string GetNomeImmagine()
    {
        return Seme.ToString() + "_" + Valore.ToString() + ".png";
    }

    //per stampare la carta in console durante i test
    public override string ToString()
    {
        return Seme.ToString() + "_" + Valore.ToString();
    }
}