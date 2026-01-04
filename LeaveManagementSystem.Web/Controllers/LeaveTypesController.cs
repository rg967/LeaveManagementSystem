using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using LeaveManagementSystem.Web.Services;

namespace LeaveManagementSystem.Web.Controllers;

public class LeaveTypesController(ILeaveTypesServices _leaveTypesServices) : Controller
{
    
    private const string NameExistsValidationMessage ="This leave type already exists in database.";
    //private readonly ILeaveTypesServices _leaveTypesServices = leaveTypesServices;

    // GET: LeaveTypes
    public async Task<IActionResult> Index()
    {
        var viewData = await _leaveTypesServices.GetAll();
        return View(viewData);
    }

    // GET: LeaveTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _leaveTypesServices.Get<LeaveTypeReadOnlyVM>(id.Value);
        
        if (leaveType == null)
        {
            return NotFound();
        }


        return View(leaveType);
    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
    public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
    {
        //Adding custom validation for model field that can't be handled by using Data Annotations
        if (await _leaveTypesServices.CheckIfLeaveTypeNameExists(leaveTypeCreate.Name))
        {
            ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);
        }
        if (leaveTypeCreate.Name.Contains("vacation")) 
        {
            ModelState.AddModelError(nameof(leaveTypeCreate.Name), "Not a valid name");
        }
        if (ModelState.IsValid)
        {
            var leaveType = _leaveTypesServices.Create(leaveTypeCreate);
            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeCreate);
    }

    

    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        

        var leaveType = await _leaveTypesServices.Get<LeaveTypeEditVM>(id.Value);
        if (leaveType == null)
        {
            return NotFound();
        }
        return View(leaveType);
    }

    // POST: LeaveTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
    public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
    {
        if (id != leaveTypeEdit.Id)
        {
            return NotFound();
        }
        //Adding custom validation for model field that can't be handled by using Data Annotations
        if (await _leaveTypesServices.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
        {
            ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _leaveTypesServices.Edit(leaveTypeEdit);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_leaveTypesServices.LeaveTypeExists(leaveTypeEdit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeEdit);
    }

    // GET: LeaveTypes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _leaveTypesServices.Get<LeaveTypeReadOnlyVM>(id.Value);
            
        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveTypesServices.Remove(id);
        return RedirectToAction(nameof(Index));
    }


    
}
