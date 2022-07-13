using EmployeeManagement.DataAccess.Entities;
using Xunit;

namespace EmployeeManagement.Test;

public class CourseTests
{
    [Fact]
    public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
    {
        // Arrange
        // Act
        var course = new Course("New course");
        
        // Assert
        Assert.True(course.IsNew);
    }
}