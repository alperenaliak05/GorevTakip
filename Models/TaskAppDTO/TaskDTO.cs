using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.TaskAppDTO
{
    public class TaskDTO
    {
        public int Id { get; set; }              
        public string Title { get; set; }        
        public string Description { get; set; }   
        public int AssignedToUserId { get; set; }
        public int AssignedByUserId { get; set; } 
        public DateTime DueDate { get; set; }     
        public int Status { get; set; }        
    }
}
