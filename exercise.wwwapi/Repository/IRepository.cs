

namespace exercise.wwwapi.Repository;
using exercise.wwwapi.Models;
public interface IRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(string FirstName);
        bool Delete(string FirstName);
        Student AddStudent(Student entity);
        Task<Student> UpdateStudent(Student pet);

    IEnumerable<Language> GetLanguages();
        Language GetLanguage(string FirstName);
        bool DeleteL(string FirstName);
        Language AddLanguage(Language entity);
        Task<Language> UpdateLanguage(Language pet);


    IEnumerable<Book> GetBooks();
        Book GetBook(int id);
        bool DeleteBook(int id);
        Book AddBook(Book entity);

        Task<Book> UpdateBook(Book pet);
}
