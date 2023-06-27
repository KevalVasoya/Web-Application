using CommonModel.ViewModel;
using System.Collections.Generic;

namespace Services.Interface
{
    public interface IEmployeeServices
    {
        void AddEmployee(EmployeeDetailsViewModel employee);
        IList<EmployeeDetailsViewModel> GetAllData();
        EmployeeDetailsViewModel GetEmployeeDetails(int id);
        void UpdateEmployee(EmployeeDetailsViewModel employee);
        IList<EmployeeDesignationViewModel> GetDesignationData();
        void DeleteEmployee(int id);
    }
}
