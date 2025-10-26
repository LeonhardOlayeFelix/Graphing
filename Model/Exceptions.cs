using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.Model
{
    public class NeighbourNotRegisteredException : InvalidOperationException
    {
        public int ID1 { get; }
        public int ID2 { get; }

        public NeighbourNotRegisteredException()
        {
        }

        public NeighbourNotRegisteredException(int id1, int id2)
            : base($"Node with ID {id1} is not a neighbour of Node with ID {id2}.")
        {
            ID1 = id1;
            ID2 = id2;
        }
    }
    public class NodeDoesNotExistException : InvalidOperationException
    {
        public int ID { get; }

        public NodeDoesNotExistException(int id) : base($"Node with ID {id} does not exist")
        {
            ID = id;
        }
    }

    public class InvalidGraphOperationException : InvalidOperationException 
    {

        public InvalidGraphOperationException(string msg)
            : base(msg)
        {
            
        }
    }

}
