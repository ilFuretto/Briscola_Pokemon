using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BriscolaPokemon.Core;

public class Mazzo
{
    private List<Carta> _carte;

    public Mazzo()
    {
        _carte = new List<Carta>();

        foreach (Seme seme in Enum.GetValues(typeof(Seme)))
        {
            for (int v = 1; v <= 10; v++)
            {
                Carta c = new Carta();
                c.Valore = v;
                c.Seme = seme;
                _carte.Add(c);
            }
        }
    }

    public void Mischia()
    {
        Random rng = new Random();

        // Algoritmo Fisher-Yates: scorre il mazzo al contrario
        // e scambia ogni carta con una casuale prima di essa
        for (int i = _carte.Count - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            Carta temp = _carte[i];
            _carte[i] = _carte[j];
            _carte[j] = temp;
        }
    }

   
    public Carta Pesca()
    {
        if (_carte.Count == 0)
        {
            return null;
        }

        Carta prima = _carte[0];
        _carte.RemoveAt(0);
        return prima;
    }

    public void AggiungiFondo(Carta c)
    {
        _carte.Add(c);
    }
    public int GetCarteRimaste()
    {
        return _carte.Count;
    }
}