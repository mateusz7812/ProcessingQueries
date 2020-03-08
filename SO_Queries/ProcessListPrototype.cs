using System;
using System.Collections.Generic;
using System.Linq;

namespace SO_Queries
{
    public class ProcessListPrototype<T> where T : ICloneable
    {
        private List<T> _list;

        public ProcessListPrototype() { }

        public void SetList(List<T> list)
        {
            _list = list;
        }

        public List<T> Clone()
        {
            if (_list == null)
            {
                throw new NullReferenceException();
            }

            return _list.Select(item => (T)item.Clone()).ToList();
        }

    }

}