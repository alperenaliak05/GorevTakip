using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TaskApp_Web.Models
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public SelectList Departments { get; set; }
    }
}
