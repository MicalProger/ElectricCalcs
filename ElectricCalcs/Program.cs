using System.Runtime.CompilerServices;

namespace ElectricCalcs
{
    public static class Writer
    {
        static int id = 0;
        public static int ActionId => ++id;
        public static T Get<T>(this List<T> ls, int index = 0)
        {
            var val = ls[index];
            ls.RemoveAt(index);
            return val;
        }
        public static T GetLast<T>(this List<T> ls)
        {
            var val = ls.Last();
            ls.RemoveAt(ls.Count - 1);
            return val;
        }
        public static void Next(string act)
        {
            Console.WriteLine($"{ActionId}) {act}");
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            ModelParcer parcer = new ModelParcer("sheme2.txt");
            var model = parcer.ParceResistor();
            //while (model.IsFull == false)
            //    model.UpdateRUI();
            //Console.WriteLine(model);
            //Console.WriteLine("----------Total----------");
            //Console.WriteLine($"Total R = {Math.Round(model.R, 3)}");
            //Console.WriteLine("----------Сurrent----------");
            //var counted = model.GetInside();
            //counted.ForEach(i => Console.WriteLine($"I{i.Id} = {Math.Round(i.I, 3)}"));
            //Console.WriteLine($"Total I = {Math.Round(model.I, 3)}");
            //Console.WriteLine("----------Voltage----------");
            //counted.ForEach(i => Console.WriteLine($"U{i.Id} = {Math.Round(i.U, 3)}"));
            //Console.WriteLine($"Total U = {Math.Round(model.I, 3)}");
            //Console.WriteLine("----------Power----------");
            //counted.ForEach(i => Console.WriteLine($"P{i.Id} = U{i.Id} * I{i.Id} = {Math.Round(i.U, 3)} * {Math.Round(i.I, 3)} = {Math.Round(i.P, 3)}"));
            //Console.WriteLine($"Total P = {Math.Round(model.P, 3)}");
            //Console.ReadLine();
        }
    }
}