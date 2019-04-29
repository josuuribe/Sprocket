using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RaraAvis.Sprocket.Parts.Interfaces
{
    /// <summary>
    /// Interface that denotes an IElement.
    /// </summary>
    public interface IElement
    {
        Guid Id { get; }
    }
}
