using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public class DepartmentRepo : IDepartment
    {
        private ITIContext Context { get; set; }
        public DepartmentRepo(ITIContext iTIContext)
        { 
            Context = iTIContext;
        }
        public List<Department> GetAllDepartments()
        {
           return Context.Department.ToList();

        }

        public Department GetDepartmentById(int id)
        {
            return Context.Department.FirstOrDefault(Id => Id.Id == id);
        }


        public Department GetDepartmentByName(string name)
        {
            return Context.Department.FirstOrDefault(_Name => _Name.Name == name);
        }
        public void AddDepartment(Department department)
        {
            Context.Department.Add(department);
        }

        public Department UpdateDepartment(int id, Department DepartmentFromReq)
        {
            Department DepartmentFromDB = GetDepartmentById(id);
            if (DepartmentFromDB is null)
            {
                return null;
            }
            DepartmentFromDB.Name = DepartmentFromReq.Name;
            DepartmentFromDB.MangerName = DepartmentFromReq.MangerName;
            Context.Department.Update(DepartmentFromDB);
            return DepartmentFromDB;




        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public List<Department> GetAllDepartmentsWithEmps()
        {
            return Context.Department.Include(e=>e.Employees).ToList();
        }
    }
}
