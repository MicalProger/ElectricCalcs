using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCalcs
{
    internal class Capacitor
    {
        public ResistorType Type;
        public virtual string Id => id;
        readonly string id = "noID";
        public double C, U, Q;
        public Capacitor()
        {
            C = U = Q = 0;
            Type = ResistorType.Single;
        }
        public Capacitor(string id)
        {
            C = U = Q = 0;
            Type = ResistorType.Single;
            this.id = id;
        }
        public Capacitor(double r, double u, double i, string id)
        {
            this.id = id;
            this.C = r;
            this.U = u;
            this.Q = i;
            Type = ResistorType.Single;
        }
        public virtual void UpdateRUI()
        {
           
            if (IsFull)
                return;
            if (Q != 0 && C != 0)
            {
                U = Q * C;
                Console.WriteLine($"{Writer.ActionId}) U{Id} = R{Id} * Q{Id} = {Math.Round(C, 3)} * {Math.Round(Q, 3)} = {Math.Round(U, 3)}");
                return;
            }
            if (U != 0 && C != 0)
            {
                Q = U / C;
                Console.WriteLine($"{Writer.ActionId}) Q{Id} = U{Id} / R{Id} = {Math.Round(U, 3)} / {Math.Round(C, 3)} = {Math.Round(Q, 3)}");
                return;
            }
            if (Q != 0 && U != 0)
            {
                C = U / Q;
                Console.WriteLine($"{Writer.ActionId}) R{Id} = U{Id} / Q{Id} = {Math.Round(U, 3)} / {Math.Round(Q, 3)} = {Math.Round(C, 3)}");
                return;
            }
        }
        public double W => (Q* U) / 2;
        public virtual bool IsFull => C * U * Q != 0;
        public override string ToString()
        {
            return $"Resistor[{Id}] R:{Math.Round(C, 3)} U:{Math.Round(U, 3)} Q:{Math.Round(Q, 3)}";
        }
        public virtual List<Capacitor> GetInside() => new List<Capacitor>() { this };
    }
}
