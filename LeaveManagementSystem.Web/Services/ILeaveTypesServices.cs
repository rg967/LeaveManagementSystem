using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services
{
    public interface ILeaveTypesServices
    {
        Task Create(LeaveTypeCreateVM model);
        Task Edit(LeaveTypeEditVM model);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyVM>> GetAll();
        Task Remove(int id);
        Task<bool> CheckIfLeaveTypeNameExists(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit);
        bool LeaveTypeExists(int id);

    }
}