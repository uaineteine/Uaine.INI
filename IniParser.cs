using System;
using System.Collections.Generic;
using System.IO;

namespace INI
{
    public class IniParser
    {
        private string fn = "config.ini";//default
        private IniDat theDat;
        public IniParser(string filename)
        {
            fn = filename;
            theDat = new IniDat();
        }
        public bool Save()
        {
            using (FileStream fs = new FileStream(fn, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < theDat.sections.Count; i++)
                    {
                        theDat.sections[i].Write(sw);
                    }
                }
            }
            return true;
        }

        public void SetKey(string mainsec, string curremotepre, string v)
        {
            theDat.SetKey(mainsec, curremotepre, v);
        }

        public bool Read()
        {
            bool worked = File.Exists(fn);
            string[] lines = File.ReadAllLines(fn);
            string curSec = "";
            List<string> SecLines = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (!IniSec.IsBlankLine(lines[i]))
                {
                    if (IniSec.IsSec(lines[i]) & lines[i] != curSec)
                    {
                        curSec = lines[i];
                        if (SecLines.Count > 0)
                        {
                            //new section so append the last bits
                            IniSec sec = new IniSec(curSec, SecLines.ToArray());
                            theDat.addSec(sec);
                        }
                        SecLines.Clear();
                    }
                    else
                    {
                        SecLines.Add(lines[i]);
                    }
                }
            }
            if (SecLines.Count > 0)
            {
                //new section so append the last bits
                IniSec sec = new IniSec(curSec, SecLines.ToArray());
                theDat.addSec(sec);
            }
            return worked;
        }

        public string getDatFromKey(string sec, string key, out bool success)
        {
            return theDat.get(sec, key, out success);
        }

        public bool getBoolFromKey(string sec, string key, out bool success)
        {
            string res = getDatFromKey(sec, key, out success);
            if (success == true)
            {
                return Convert.ToBoolean(res);
            }
            else
            {
                return false;
            }
        }

        public int getIntFromKey(string sec, string key, out bool success)
        {
            string res = getDatFromKey(sec, key, out success);
            if (success == true)
            {
                return Convert.ToInt32(res);
            }
            else
            {
                return 0;
            }
        }

        public float getFloatFromKey(string sec, string key, out bool success)
        {
            string res = getDatFromKey(sec, key, out success);
            if (success == true)
            {
                return Convert.ToSingle(res);
            }
            else
            {
                return 0;
            }
        }
    }
}
