using System.Collections.Generic;

namespace JotBotNg2Core.Lib
{
    public class IonCollection<T>: IonResource
    {
        public IEnumerable<T> Items { get; set; }
        public int Elements { get; set; }
    }
}