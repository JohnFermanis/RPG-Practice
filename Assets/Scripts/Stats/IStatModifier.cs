using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IStatModifier
    {
        IEnumerable<float> GetStatAdders(Stat stat);
        IEnumerable<float> GetStatMultipliers(Stat stat);
    }


}
