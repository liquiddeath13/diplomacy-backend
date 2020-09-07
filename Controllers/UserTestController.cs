using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace diplomacy_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTestController : ControllerBase
    {
        private TestClass tc;
        private readonly ILogger<UserTestController> _logger;

        public UserTestController(ILogger<UserTestController> logger)
        {
            _logger = logger;
            tc = new TestClass();
        }

        private User getUserByID(int userID) => tc.UserList.Where(x => x.ID == userID).First();

        private Test getTestByID(int userID, int testID) => getUserByID(userID).OwnTests[testID - 1];

        private TestSolution getSolutionByID(int userID, int solutionID) => getUserByID(userID).OwnSolutions[solutionID];

        private bool validateUserID(int userID) => userID > 0 && userID <= tc.UserList.Count;

        private bool validateTestID(int userID, int testID) => testID > 0 && testID <= getUserByID(userID).OwnTests.Count;

        [HttpGet("tests")]
        public IEnumerable<Test> GetTestsList(int userID) => validateUserID(userID) ? getUserByID(userID).OwnTests : null;

        [HttpGet("solution")]
        public TestSolution GetTestSolution(int userID, int testID) => validateUserID(userID) && validateTestID(userID, testID) ? getSolutionByID(userID, testID) : null;

        [HttpGet("test")]
        public Test GetTest(int userID, int testID) => validateUserID(userID) && validateTestID(userID, testID) ? getTestByID(userID, testID) : null;

        [HttpGet("user")]
        public User GetUser(int userID) => validateUserID(userID) ? getUserByID(userID) : null;
    }
}
