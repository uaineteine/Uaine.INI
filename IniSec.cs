using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace INI
{
    public class IniSec
    {
        public string SecName;
        string[] LHS;
        string[] RHS;
        int n;
        public IniSec(string sec, string[] l, string[] r)
        {
            SecName = makeSec(sec);
            LHS = l;
            RHS = r;
            n = l.Length;
        }
        public IniSec(string sec, string[] lines)
        {
            SecName = makeSec(sec);
            n = lines.Length;
            LHS = new string[n];
            RHS = new string[n];
            for (int i = 0; i < n; i++)
            {
                SeperateKey(lines[i], out LHS[i], out RHS[i]);
            }
        }
        public void Write(StreamWriter sw)
        {
            sw.WriteLine(IniSec.makeSec(SecName));
            for (int i = 0; i < n; i++)
            {
                sw.WriteLine(LHS[i] + "=" + RHS[i]);
            }
        }
        public string get(string key, out bool success)
        {
            success = false;
            for (int i = 0; i < n; i++)
            {
                if (LHS[i] == key)
                {
                    success = true;
                    return RHS[i];
                }
            }
            return "";
        }
        public static string makeSec(string text)
        {
            //check if it isn't already a section
            if (IsSec(text))
                return text;//already fine
            else
                return "[" + text + "]";
        }
        public static bool IsSec(string text)
        {
            if (text[0] == '[' & text[text.Length - 1] == ']')
                return true;
            else
                return false;
        }
        public static bool IsBlankLine(string text)
        {
            if (text == "")
                return true;
            else
                return false;
        }
        public static bool SeperateKey(string line, out string l, out string r)
        {
            bool worked;
            string[] split = line.Split('=');
            l = "";
            r = "";
            if (split.Length > 1)//has it
            {
                worked = true;
                l = split[0];
                r = split[1];
            }
            else
                worked = false;
            return worked;
        }

        internal void Setkey(string curremotepre, string v)
        {
            for (int i = 0; i < n; i++)
            {
                if (LHS[i] == curremotepre)
                    RHS[i] = v;
            }
        }
    }
}
