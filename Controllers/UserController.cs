using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Models;
using DotnetAPI.Data;
using DotnetAPI.Dtos;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
       Console.WriteLine(config.GetConnectionString("DefaultConnection"));
       _dapper = new DataContextDapper(config);
    }
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers")]
    //public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        string sql =@"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
        //return new string[] {"user1", "user2",};
        /*return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();*/
    }

    [HttpGet("GetUsers/{userId}")]
    //public IEnumerable<User> GetUsers()
    public User GetSingleUser(int userId)
    {
        string sql =@"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM TutorialAppSchema.Users
                WHERE UserId =" + userId.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName + 
            "', [LastName] = '" + user.LastName +
            "', [Email] = '" + user.Email +
            "', [Gender] = '" + user.Gender + 
            "', [Active] = '" + user.Active +
            "' WHERE UserId = " + user.UserId;

        Console.WriteLine(sql);
        
        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to update user");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user)
    {
        string sql = @"INSERT INTO TutorialAppSchema.Users(
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        ) VALUES (" +
            "'" + user.FirstName + 
            "', '" + user.LastName +
            "', '" + user.Email +
            "', '" + user.Gender + 
            "', '" + user.Active +
        "')";
        
        Console.WriteLine(sql);
        
        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to add user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql =@"
            DELETE FROM TutorialAppSchema.Users 
                WHERE UserId = " + userId.ToString();

        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to delete user");
    }




    [HttpGet("GetUserSalaries")]
    //public IEnumerable<User> GetUsers()
    public IEnumerable<UserSalary> GetUserSalaries()
    {
        string sql =@"
            SELECT UserSalary.UserId
                , UserSalary.Salary
            FROM TutorialAppSchema.UserSalary";

        IEnumerable<UserSalary> userSalaries = _dapper.LoadData<UserSalary>(sql);
        return userSalaries;
        
    }

    [HttpGet("GetUserSalary/{userId}")]
    //public IEnumerable<User> GetUsers()
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql =@"
            SELECT UserSalary.UserId
                , UserSalary.Salary
            FROM TutorialAppSchema.UserSalary
                WHERE UserId =" + userId.ToString();

        UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);
        return userSalary;
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {
        string sql = @"INSERT INTO TutorialAppSchema.UserSalary(
            UserId,
            Salary
        ) VALUES (" +
            "'" + userSalary.UserId + 
            "', '" + userSalary.Salary +
        "')";
        
        Console.WriteLine(sql);
        
        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to add user salary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET Salary = '" + userSalary.Salary +
            "' WHERE UserId = " + userSalary.UserId.ToString();

        Console.WriteLine(sql);
        
        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to update user salary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql =@"
            DELETE FROM TutorialAppSchema.UserSalary 
                WHERE UserId = " + userId.ToString();

        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to delete user salary");
    }



    [HttpGet("GetUserJobInfos")]
    //public IEnumerable<User> GetUsers()
    public IEnumerable<UserJobInfo> GetUserJobInfos()
    {
        string sql =@"
            SELECT  UserJobInfo.UserId
                    , UserJobInfo.JobTitle
                    , UserJobInfo.Department
            FROM  TutorialAppSchema.UserJobInfo";

        IEnumerable<UserJobInfo> userJobInfos = _dapper.LoadData<UserJobInfo>(sql);
        return userJobInfos;
        
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    //public IEnumerable<User> GetUsers()
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        string sql =@"
            SELECT  UserJobInfo.UserId
                    , UserJobInfo.JobTitle
                    , UserJobInfo.Department
            FROM  TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();

        UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);
        return userJobInfo;
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
    {
        string sql = @"INSERT INTO TutorialAppSchema.UserJobInfo (
                UserId,
                Department,
                JobTitle
            ) VALUES (" + userJobInfo.UserId
                + ", '" + userJobInfo.Department
                + "', '" + userJobInfo.JobTitle
                + "')";
        
        Console.WriteLine(sql);
        
        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to add user job info");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        string sql = @"
        UPDATE TutorialAppSchema.UserJobInfo SET Department='" 
            + userJobInfo.Department
            + "', JobTitle='"
            + userJobInfo.JobTitle
            + "' WHERE UserId=" + userJobInfo.UserId.ToString();

        Console.WriteLine(sql);
        
        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to update user job info");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql =@"
            DELETE FROM TutorialAppSchema.UserJobInfo 
                WHERE UserId = " + userId.ToString();

        if(_dapper.ExecuteSQL(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to delete user job info");
    }
}
