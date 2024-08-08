using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IToTaskRepository
    { 
        Task UpdateAsync(ToTask task);
      
    }
}
