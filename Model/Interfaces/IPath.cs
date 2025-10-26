using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public interface IPath<T> : INodeCollection<T>
    {
        public int Cost { get; }
        public void Append(INode<T> node);

    }
}
