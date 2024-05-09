using BlazorHostingWithServer.Server.Data;
using BlazorHostingWithServer.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorHostingWithServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return await _db.Students.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _db.Students.Include(s => s.Addresses).FirstOrDefaultAsync(s => s.Id == id);
            if(student == null) { return NotFound(); }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Student student)
        {
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return student.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;
            foreach (var item in student.Addresses)
            {
                if (item.Id != 0)
                {
                    _db.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _db.Entry(item).State= EntityState.Added;
                }                
            }
            var idsOfAddress = student.Addresses.Select(s => s.Id).ToList();
            var addressToDelete = await _db.Addresses.Where(s => !idsOfAddress.Contains(s.Id) && s.StudentId == student.Id).ToListAsync();
            _db.RemoveRange(addressToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _db.Students.Include(s => s.Addresses).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            _db.Addresses.RemoveRange(student.Addresses);
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
