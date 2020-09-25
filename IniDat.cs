using System;
using System.Collections.Generic;
using System.Text;

namespace INI
{
    public class IniDat
    {
        public List<IniSec> sections;
        public IniDat()
        {
            sections = new List<IniSec>();
        }
        public void addSec(string sec, string[] l, string[] r)
        {
            addSec(new IniSec(sec, l, r));
        }
        public void addSec(IniSec toAdd)
        {
            sections.Add(toAdd);
        }
        public string get(string sec, string key, out bool success)
        {
            success = false;
            for (int i = 0; i < sections.Count; i++)
            {
                if (IniSec.makeSec(sections[i].SecName) == IniSec.makeSec(sec))
                    return sections[i].get(key, out success);
            }
            return "";//else it didn't find it
        }
        public IniSec getSec(string sec)
        {
            for (int i = 0; i < sections.Count; i++)
            {
                if (IniSec.makeSec(sections[i].SecName) == IniSec.makeSec(sec))
                    return sections[i];
            }
            return new IniSec("no sec", new string[1] { "nadah" });
        }
        public void SetKey(string mainsec, string curremotepre, string v)
        {
            int index = 0;
            for (int i = 0; i < sections.Count; i++)
            {
                if (IniSec.makeSec(sections[i].SecName) == IniSec.makeSec(mainsec))
                {
                    index = i;
                }
            }
            sections[index].Setkey(curremotepre, v);
        }
    }
}
