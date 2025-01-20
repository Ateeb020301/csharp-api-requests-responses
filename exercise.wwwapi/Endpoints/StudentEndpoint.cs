using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class StudentEndpoint
    {
        public static void ConfigurePetEndpoint(this WebApplication app)
        {
            var pets = app.MapGroup("students");

            pets.MapGet("/gettAll", GetAllStudents);
            pets.MapGet("/get{FirstName}", GetStudent);
            pets.MapPost("/add", AddStudent);
            pets.MapDelete("/delete{Firstname}", DeleteStudent);
            pets.MapPut("/put{Firstname}", UpdateStudent);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetStudent(IRepository repository, string FirstName)
        {
            var student = repository.GetStudents().FirstOrDefault(x => x.FirstName == FirstName);
            return Results.Ok(student);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllStudents(IRepository repository)
        {
            var students = repository.GetStudents();
            return Results.Ok(students);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> AddStudent(IRepository repository, Student model)
        {
            Student student = new Student()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            repository.AddStudent(student);

            return Results.Ok(student);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> DeleteStudent(IRepository repository, string FirstName)
        {
            try
            {
                var model = repository.GetStudent(FirstName);
                if (repository.Delete(FirstName)) return Results.Ok(new { When = DateTime.Now, Status = "Deleted", FirstName = model.FirstName, LastName = model.LastName });
                return TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateStudent(IRepository repository, string FirstName, Student model)
        {
            try
            {
                var target = repository.GetStudent(FirstName);
                if (target == null) return Results.NotFound("Student not found");
                if (model.FirstName != null) target.FirstName = model.FirstName;
                if (model.LastName != null) target.LastName = model.LastName;
                await repository.UpdateStudent(target);
                return Results.Ok(target);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
