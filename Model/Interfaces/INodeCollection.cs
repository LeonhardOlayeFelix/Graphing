using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model.Interfaces
{
    public interface INodeCollection<T>
    {
        public IList<INode<T>> Nodes { get; }

    }
}
