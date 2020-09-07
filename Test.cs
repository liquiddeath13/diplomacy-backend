using System;
using System.Collections.Generic;

namespace diplomacy_webapi
{
    public class TestClass
    {
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
        public List<User> UserList { get; set; }
        public TestClass()
        {
            UserList = new List<User>();
            User SampleUser = new User 
            {
                ID = 1,
                Name = "Test User",
                OwnSolutions = new List<TestSolution>(),
                OwnTests = new List<Test>()
            };
            for (int i = 0; i < 10; i++)
            {
                Test NewTest = new Test
                {
                    Name = $"Test #{i + 1}",
                    CreatedAt = DateTime.Now,
                    EditedAt = DateTime.Now,
                    Description = $"Automatically created test by Web API ({i + 1})",
                    TaskList = new List<Task>()
                };
                for (int j = 0; j < 15; j++)
                {
                    Task NewTask = new Task
                    {
                        Name = $"Answer #{j + 1}",
                        Type = TaskTypes.OneAnswer,
                        Description = $"Automatically created task by Web API ({j + 1})",
                        AnswerList = new List<Answer>()
                    };
                    int TrueAnswerIndex = GetRandomNumber(0, 4);
                    for (int k = 0; k < 4; k++)
                    {
                        NewTask.AnswerList.Add(new Answer
                        {
                            Description = $"Automatically created answer by Web API ({k + 1})",
                            Flag = k == TrueAnswerIndex
                        });
                    }
                    NewTest.TaskList.Add(NewTask);
                }
                SampleUser.OwnTests.Add(NewTest);
            }
            UserList.Add(SampleUser);
        }
    }
    public enum TaskTypes : int
    {
        OneAnswer,
        MultipleAnswer,
        Matching,
        OpenAnswer
    }
    public class Answer
    {
        public string Description { get; set; }
        public bool Flag { get; set; }
    }
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskTypes Type { get; set; }
        public List<Answer> AnswerList { get; set; }
    }
    public class Test
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }
        public List<Task> TaskList { get; set; }
    }
    public class TaskSolution
    {
        public Task Task { get; set; }
        public Answer UserAnswer { get; set; }
    }
    public class TestSolution
    {
        public string TestName { get; set; }
        public List<TaskSolution> TaskListSolution { get; set; }
        public DateTime CreatedAt { get; set; }
        public double SolutionMark { get; set; }
    }
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Test> OwnTests { get; set; }
        public List<TestSolution> OwnSolutions { get; set; }
    }
}