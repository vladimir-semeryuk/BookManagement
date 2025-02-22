using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Models.Abstractions;
public interface ISoftDeletable
{
    public bool IsDeleted { get; }
}
