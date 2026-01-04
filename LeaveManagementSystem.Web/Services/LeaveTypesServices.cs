using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services;

public class LeaveTypesServices(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypesServices
{
    //We are using primary constructor feature of C# 9 to reduce boilerplate code
    //public readonly ApplicationDbContext _context = context;
    //public readonly IMapper _mapper = mapper;

    //public LeaveTypesServices(ApplicationDbContext context, IMapper mapper)
    //{
    //    this.context = context;
    //    this.mapper = mapper;
    //}

    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        var data = await _context.LeaveTypes.ToListAsync();
        //Convert data into view model using AutoMapper
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
        //Code to use without AutoMapper
        /*var viewData = data.Select(x => new IndexVM
        {
            Id = x.Id,
            Name = x.Name,
            NumberOfDays = x.NumberOfDays
        }).ToList();*/
        return viewData;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data is null)
            return null;
        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Remove(int id)
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data is not null)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Edit(LeaveTypeEditVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.LeaveTypes.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task Create(LeaveTypeCreateVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.LeaveTypes.Add(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        name = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name));
    }

    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        var name = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name)
        && x.Id != leaveTypeEdit.Id);
    }
}
