using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public interface IDepartment
    {

        public List<Department> GetAllDepartments();
        public List<Department> GetAllDepartmentsWithEmps(); 
        public Department GetDepartmentById (int id); 
        
        public Department GetDepartmentByName (string name);

        public void AddDepartment (Department department);

        public Department UpdateDepartment (int id ,Department department);

        public void SaveChanges ();

    }
}
