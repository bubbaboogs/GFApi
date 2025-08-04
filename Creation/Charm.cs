using System;
using System.Linq.Expressions;
using UnityEngine;

namespace GFApi.Creation
{
    public class ModCharm : Charm
    {
        public string nameSpace;
        new public CharmData charmData;
        new public int charmPower;

        private Action _useMethod;
        public void SetUseMethod(Action useMethod)
        {
            _useMethod = useMethod;
        }
    }

    public class Charms
    {
        public static ModCharm CreateCharm(string nameSpace, CharmData charmData, int charmPower, Action useMethod)
        {
            ModCharm charm = new ModCharm
            {
                nameSpace = nameSpace,
                charmData = charmData,
                charmPower = charmPower
            };

            charm.SetUseMethod(useMethod);
            return charm;
        }
    }
}
