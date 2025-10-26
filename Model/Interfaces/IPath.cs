using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model.Interfaces
{
    public interface IRoute<T> : INodeCollection<T>
    {
        public int Cost { get; }

    public void Append(INode<T> node);
    }
    public interface IPath<T> : IRoute<T> { };
    public interface IWalk<T> : IRoute<T> { };
}
