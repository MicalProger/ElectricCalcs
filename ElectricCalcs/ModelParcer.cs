using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCalcs
{
    public enum CalcMode
    {
        Resostor,
        Сapacitor
    }
    internal class ModelParcer
    {
        readonly List<string> strModel;
        public ModelParcer(string path)
        {
            strModel = File.ReadAllLines(path).ToList().Select(i => i.Replace("\t", String.Empty)).ToList();
        }
        public CombinedResistor ParceResistor()
        {
            var loc = strModel;
            loc.GetLast();
            var tp = loc.Get() switch
            {
                "new par" => ResistorType.Parallel,
                "new seq" => ResistorType.Sequential
            };
            return new CombinedResistor(ParceListRes(loc), tp);
        }
        List<Resistor> ParceListRes(List<string> list)
        {
            List<Resistor> ress = new List<Resistor>();
            var ls = list;
            while (ls.Count != 0)
            {
                Resistor rs;
                var read = ls.Get();
                switch (read)
                {
                    case "new par":
                        var locLs = new List<string>();
                        int a = 1;
                        while (a != 0)
                        {
                            var part = ls.Get();
                            locLs.Add(part);
                            if (part.StartsWith("new"))
                                a++;
                            else if (part.StartsWith("end"))
                                a--;
                        }
                        locLs.GetLast();
                        rs = new CombinedResistor(ParceListRes(locLs), ResistorType.Parallel);
                        break;
                    case "new seq":
                        var locLs1 = new List<string>();
                        int a1 = 1;
                        while (a1 != 0)
                        {
                            var part = ls.Get();
                            locLs1.Add(part);
                            if (part.StartsWith("new"))
                                a1++;
                            else if (part.StartsWith("end"))
                                a1--;
                        }
                        locLs1.GetLast();
                        rs = new CombinedResistor(ParceListRes(locLs1), ResistorType.Sequential);
                        break;
                    default:
                        var pts = read.Split(' ').ToList();
                        rs = new Resistor(pts.Get(1));
                        pts.Get();

                        while (pts.Count != 0)
                        {
                            switch (pts.Get(0))
                            {
                                case "R":
                                    rs.R = Convert.ToDouble(pts.Get());
                                    break;
                                case "U":
                                    rs.U = Convert.ToDouble(pts.Get());
                                    break;
                                case "I":
                                    rs.I = Convert.ToDouble(pts.Get());
                                    break;
                            }
                        }
                        break;
                }
                ress.Add(rs);
            }
            return ress;
        }
        public CombinedCapacitor ParceCapitor()
        {
            var loc = strModel;
            loc.GetLast();
            var tp = loc.Get() switch
            {
                "new par" => ResistorType.Parallel,
                "new seq" => ResistorType.Sequential
            };
                return new CombinedCapacitor(ParceListCap(loc), tp);
        }
        List<Capacitor> ParceListCap(List<string> list)
        {
            List<Capacitor> ress = new List<Capacitor>();
            var ls = list;
            while (ls.Count != 0)
            {
                Capacitor rs;
                var read = ls.Get();
                switch (read)
                {
                    case "new par":
                        var locLs = new List<string>();
                        int a = 1;
                        while (a != 0)
                        {
                            var part = ls.Get();
                            locLs.Add(part);
                            if (part.StartsWith("new"))
                                a++;
                            else if (part.StartsWith("end"))
                                a--;
                        }
                        locLs.GetLast();
                        rs = new CombinedCapacitor(ParceListCap(locLs), ResistorType.Parallel);
                        break;
                    case "new seq":
                        var locLs1 = new List<string>();
                        int a1 = 1;
                        while (a1 != 0)
                        {
                            var part = ls.Get();
                            locLs1.Add(part);
                            if (part.StartsWith("new"))
                                a1++;
                            else if (part.StartsWith("end"))
                                a1--;
                        }
                        locLs1.GetLast();
                        rs = new CombinedCapacitor(ParceListCap(locLs1), ResistorType.Sequential);
                        break;
                    default:
                        var pts = read.Split(' ').ToList();
                        rs = new Capacitor(pts.Get(1));
                        pts.Get();

                        while (pts.Count != 0)
                        {
                            switch (pts.Get(0))
                            {
                                case "C":
                                    rs.C = Convert.ToDouble(pts.Get());
                                    break;
                                case "U":
                                    rs.U = Convert.ToDouble(pts.Get());
                                    break;
                                case "Q":
                                    rs.Q = Convert.ToDouble(pts.Get());
                                    break;
                            }
                        }
                        break;
                }
                ress.Add(rs);
            }
            return ress;
        }
    }
}
