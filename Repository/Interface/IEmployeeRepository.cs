using CommonModel.DBModel;
using System.Collections.Generic;

namespace Repository.Interface
{
    public  interface IEmployeeRepository
   {
        List<EmployeeDetails> AddEmployee(EmployeeDetails employee);
        IList<EmployeeDetails> GetAllData();
        IList<EmployeeDesignation> GetDesignation();
        EmployeeDetails GetEmployeeById(int id);
        void UpdateEmployee(EmployeeDetails employee);
        void DeleteEmployee(int id);
    }
}
