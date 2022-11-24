using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCalcs
{
    public enum ResistorType
    {
        Single,
        Parallel,
        Sequential
    }
    internal class CombinedResistor : Resistor
    {
        public List<Resistor> inside;
        public CombinedResistor(ResistorType type)
        {
            inside = new List<Resistor>();
            Type = type;
        }
        public CombinedResistor(List<Resistor> parts, ResistorType type)
        {
            inside = parts;
            Type = type;
        }
        public override string ToString()
        {
            return $"{Enum.GetName(Type)} resistor[{Id}] R:{Math.Round(R, 3)} U:{Math.Round(U, 3)} I:{Math.Round(I, 3)}";
        }
        public override bool IsFull => inside.All(x => x.IsFull) && base.IsFull;
        public override string Id => string.Join("", inside.Select(x => x.Id));
        public override void UpdateRUI()
        {
            if (!base.IsFull)
            {
                if (I != 0 && R != 0)
                {
                    U = I * R;
                    Console.WriteLine($"{Writer.ActionId})U{Id} = R{Id} * I{Id} = {Math.Round(R, 3)} * {Math.Round(I, 3)} = {Math.Round(U, 3)}");
                    return;
                }
                else if (U != 0 && R != 0)
                {
                    I = U / R;
                    Console.WriteLine($"{Writer.ActionId})I{Id} = U{Id} / R{Id} = {Math.Round(U, 3)} / {Math.Round(R, 3)} = {Math.Round(I, 3)}");
                }
            }
            inside.Where(i => i.IsFull == false).ToList().ForEach(x => x.UpdateRUI());
            if (inside.All(i => i.R != 0) && R == 0)
                switch (Type)
                {
                    case ResistorType.Parallel:
                        R = 1 / inside.Select(i => 1 / i.R).Sum();
                        Console.WriteLine($"{Writer.ActionId}) R{Id} = 1 / ({String.Join(" + ", inside.Select(i => $"1 / R{i.Id}"))}) = 1 / ({String.Join(" + ", inside.Select(i => $"1 / {Math.Round(i.R, 3)}"))}) = {Math.Round(R, 3)}");
                        break;
                    case ResistorType.Sequential:
                        R = inside.Sum(i => i.R);
                        Console.WriteLine($"{Writer.ActionId}) R{Id} = {String.Join(" + ", inside.Select(i => $"R{i.Id}"))} = {String.Join(" + ", inside.Select(i => $"{Math.Round(i.R, 3)}"))} = {Math.Round(R, 3)}");
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
                        U = inside.Select(i => i.U).Max();
                    if (U == 0)
                        break;
                    for (int i = 0; i < inside.Count; i++)
                    {
                        if (inside[i].U == 0)
                            Console.WriteLine($"{Writer.ActionId}) U{inside[i].Id} = U{Id} = {Math.Round(U, 3)}");
                        inside[i].U = U;
                    }

                    break;
                case ResistorType.Sequential:
                    if (I == 0)
                        I = inside.Select(i => i.I).Max();
                    if (I == 0)
                        break;
                    for (int i = 0; i < inside.Count; i++)
                    {
                        if (inside[i].I == 0)
                            Console.WriteLine($"{Writer.ActionId}) I{inside[i].Id} = I{Id} = {Math.Round(I, 3)}");
                        inside[i].I = I;
                    }
                    break;
            }
        }
        public override List<Resistor> GetInside()
        {
            List<Resistor> ins = new List<Resistor>();
            foreach (var lc in inside)
                ins.AddRange(lc.GetInside());
            return ins;
        }
    }
}
