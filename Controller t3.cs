using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserDataContext _context;

        public AdminController(UserDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.GetAll<User>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Forename,Surname,Email,IsActive,DateOfBirth")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Create(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.GetById<User>((int)id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Forename,Surname,Email,IsActive,DateOfBirth")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.GetById<User>((int)id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Delete<User>((int)id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ActiveUsers()
        {
            return View("Index", _context.GetActiveUsers());
        }

        public IActionResult InactiveUsers()
        {
            return View("Index", _context.GetInactiveUsers());
        }
    }
}