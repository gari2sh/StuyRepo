using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StuyvesantHighSchool.Data;
using StuyvesantHighSchool.Models;
using StuyvesantHighSchool.Models.StuyViewModels;

namespace StuyvesantHighSchool.Controllers
{
    public class TeachersController : Controller
    {
        private readonly StuyDbContext _context;

        public TeachersController(StuyDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Teachers.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? id, int? subjectID)
        {
            var viewModel = new TeacherIndexVM();
            viewModel.Teachers = await _context.Teachers
                  .Include(i => i.RoomAssignment)
                  .Include(i => i.SubjectAssignments)
                    .ThenInclude(i => i.Subject)
                        .ThenInclude(i => i.Enrollments)
                            .ThenInclude(i => i.Student)
                  .Include(i => i.SubjectAssignments)
                    .ThenInclude(i => i.Subject)
                        .ThenInclude(i => i.Department)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["TeacherID"] = id.Value;
                Teacher teacher = viewModel.Teachers.Where(
                    i => i.TeacherID == id.Value).Single();
                viewModel.Subjects = teacher.SubjectAssignments.Select(s => s.Subject);
            }

            if (subjectID != null)
            {
                ViewData["SubjectID"] = subjectID.Value;
                viewModel.Enrollments = viewModel.Subjects.Where(
                    x => x.SubjectID == subjectID).Single().Enrollments;
            }

            return View(viewModel);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            var teacher = new Teacher();
            teacher.SubjectAssignments = new List<SubjectAssignment>();
            PopulateRelatedSubject(teacher);
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,HireDate,LastName,RoomAssignment")] Teacher teacher, string[] selectedSubjects)
        {
            if (selectedSubjects != null)
            {
                teacher.SubjectAssignments = new List<SubjectAssignment>();
                foreach (var subject in selectedSubjects)
                {
                    var subjectToAdd = new SubjectAssignment { TeacherID = teacher.TeacherID, SubjectID = int.Parse(subject) };
                    teacher.SubjectAssignments.Add(subjectToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateRelatedSubject(teacher);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var teacher = await _context.Teachers.FindAsync(id);
            var teacher = await _context.Teachers
                .Include(i => i.RoomAssignment)
                .Include(i => i.SubjectAssignments).ThenInclude(i => i.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }
            PopulateRelatedSubject(teacher);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedSubjects)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherToUpdate = await _context.Teachers
                .Include(i => i.RoomAssignment)
                .Include(i => i.SubjectAssignments)
                .ThenInclude(i => i.Subject)
                .FirstOrDefaultAsync(s => s.TeacherID == id);

            if (await TryUpdateModelAsync<Teacher>(
                teacherToUpdate,
                "",
                i => i.FirstName, i => i.LastName, i => i.JoinDate, i => i.RoomAssignment))
            {
                if (String.IsNullOrWhiteSpace(teacherToUpdate.RoomAssignment?.Room))
                {
                    teacherToUpdate.RoomAssignment = null;
                }
                UpdateTeacherSubjects(selectedSubjects, teacherToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "ERROR: Your changes can not be saved." +
                        "Please try again");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateTeacherSubjects(selectedSubjects, teacherToUpdate);
            PopulateRelatedSubject(teacherToUpdate);
            return View(teacherToUpdate);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Teacher teacher = await _context.Teachers
        .Include(i => i.SubjectAssignments)
        .SingleAsync(i => i.TeacherID == id);

            var departments = await _context.Departments
                .Where(d => d.TeacherID == id)
                .ToListAsync();
            departments.ForEach(d => d.TeacherID = null);


            //var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherID == id);
        }

        private void PopulateRelatedSubject(Teacher teacher)
        {
            var allSubjects = _context.Subjects;
            var teacherSubjects = new HashSet<int>(teacher.SubjectAssignments.Select(c => c.SubjectID));
            var viewModel = new List<RelatedSubjectVM>();
            foreach (var subject in allSubjects)
            {
                viewModel.Add(new RelatedSubjectVM
                {
                    SubjectID = subject.SubjectID,
                    Title = subject.Title,
                    RelatedSubject = teacherSubjects.Contains(subject.SubjectID)
                });
            }
            ViewData["Subjects"] = viewModel;
        }

        private void UpdateTeacherSubjects(string[] selectedSubjects, Teacher teacherToUpdate)
        {
            if (selectedSubjects == null)
            {
                teacherToUpdate.SubjectAssignments = new List<SubjectAssignment>();
                return;
            }

            var selectedSubjectsHS = new HashSet<string>(selectedSubjects);
            var teacherSubjects = new HashSet<int>
                (teacherToUpdate.SubjectAssignments.Select(c => c.Subject.SubjectID));
            foreach (var subject in _context.Subjects)
            {
                if (selectedSubjectsHS.Contains(subject.SubjectID.ToString()))
                {
                    if (!teacherSubjects.Contains(subject.SubjectID))
                    {
                        teacherToUpdate.SubjectAssignments.Add(new SubjectAssignment { TeacherID = teacherToUpdate.TeacherID, SubjectID = subject.SubjectID });
                    }
                }
                else
                {

                    if (teacherSubjects.Contains(subject.SubjectID))
                    {
                        SubjectAssignment subjectToRemove = teacherToUpdate.SubjectAssignments.FirstOrDefault(i => i.SubjectID == subject.SubjectID);
                        _context.Remove(subjectToRemove);
                    }
                }
            }
        }
    }
}
