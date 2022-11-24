using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCalcs
{
    internal class CombinedCapacitor : Capacitor
    {
        public List<Capacitor> inside;
        public CombinedCapacitor(ResistorType type)
        {
            inside = new List<Capacitor>();
            Type = type;
        }
        public CombinedCapacitor(List<Capacitor> parts, ResistorType type)
        {
            inside = parts;
            Type = type;
        }
        public override string ToString()
        {
           
            return $"{Enum.GetName(Type)} capacitor[{Id}] C:{Math.Round(C, 3)} U:{Math.Round(U, 3)} C:{Math.Round(Q, 3)}";
        }
        public override bool IsFull => inside.All(x => x.IsFull) && base.IsFull;
        public override string Id => string.Join("", inside.Select(x => x.Id));
        public override void UpdateRUI()
        {
            if (!base.IsFull)
            {
                if (Q != 0 && C != 0)
                {
                    U = Q * C;
                    Console.WriteLine($"{Writer.ActionId})U{Id} = C{Id} * Q{Id} = {Math.Round(C, 3)} * {Math.Round(Q, 3)} = {Math.Round(U, 3)}");
                    return;
                }
                else if (U != 0 && C != 0)
                {
                    Q = U / C;
                    Console.WriteLine($"{Writer.ActionId})Q{Id} = U{Id} / C{Id} = {Math.Round(U, 3)} / {Math.Round(C, 3)} = {Math.Round(Q, 3)}");
                }
            }
            inside.Where(Q => Q.IsFull == false).ToList().ForEach(x => x.UpdateRUI());
            if (inside.All(Q => Q.C != 0) && C == 0)
                switch (Type)
                {
                    case ResistorType.Parallel:
                        C = 1 / inside.Select(Q => 1 / Q.C).Sum();
                        Console.WriteLine($"{Writer.ActionId}) C{Id} = 1 / ({String.Join(" + ", inside.Select(Q => $"1 / C{Q.Id}"))}) = 1 / ({String.Join(" + ", inside.Select(Q => $"1 / {Math.Round(Q.C, 3)}"))}) = {Math.Round(C, 3)}");
                        break;
                    case ResistorType.Sequential:
                        C = inside.Sum(Q => Q.C);
                        Console.WriteLine($"{Writer.ActionId}) C{Id} = {String.Join(" + ", inside.Select(Q => $"C{Q.Id}"))} = {String.Join(" + ", inside.Select(Q => $"{Math.Round(Q.C, 3)}"))} = {Math.Round(C, 3)}");
                        break;
                }
            UprateConstansParam();
        }
        public void UprateConstansParam()
        {
            switch (Type)
            {
                case ResistorType.Parallel:
                    if (U == 0)
                        U = inside.Select(Q => Q.U).Max();
                    if (U == 0)
                        break;
                    for (int Q = 0; Q < inside.Count; Q++)
                    {
                        if (inside[Q].U == 0)
                            Console.WriteLine($"{Writer.ActionId}) U{inside[Q].Id} = U{Id} = {Math.Round(U, 3)}");
                        inside[Q].U = U;
                    }

                    break;
                case ResistorType.Sequential:
                    if (Q == 0)
                        Q = inside.Select(Q => Q.Q).Max();
                    if (Q == 0)
                        break;
                    for (int Q = 0; Q < inside.Count; Q++)
                    {
                        if (inside[Q].Q == 0)
                            Console.WriteLine($"{Writer.ActionId}) Q{inside[Q].Id} = Q{Id} = {Math.Round((decimal)Q, 3)}");
                        inside[Q].Q = Q;
                    }
                    break;
            }
        }
        public override List<Capacitor> GetInside()
        {
            List<Capacitor> ins = new List<Capacitor>();
            foreach (var lc in inside)
                ins.AddRange(lc.GetInside());
            return ins;
        }
    }
}
