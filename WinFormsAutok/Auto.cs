using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAutok
{
    internal class Auto
    {
        int id;
        string rendszam;
        int ev;
        string szin;

        public int Id { get => id; set => id = value; }
        public string Rendszam { get => rendszam; set => rendszam = value; }
        public int Ev { get => ev; set => ev = value; }
        public string Szin { get => szin; set => szin = value; }

        public Auto(int id, string rendszam, int ev, string szin)
        {
            Id = id;
            Rendszam = rendszam;
            Ev = ev;
            Szin = szin;
        }

        public override string ToString()
        {
            return $"{rendszam} {szin} {ev}";
        }
    }
}
