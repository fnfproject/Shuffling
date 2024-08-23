using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShufflingTest.Models;
using ShufflingTest.Dtos;

namespace ShufflingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShuffleController : ControllerBase
    {
        private readonly List<Question> _questions;

        public ShuffleController()
        {
            // Sample data, in a real scenario this would come from a database
            _questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "What is the capital of France?",
                    Options = new List<Option>
                    {
                        new Option { Id = 1, Text = "Paris" },
                        new Option { Id = 2, Text = "Berlin" },
                        new Option { Id = 3, Text = "Rome" },
                        new Option { Id = 4, Text = "Madrid" }
                    },
                    CorrectOptionId = 1
                },
                new Question
                {
                    Id = 2,
                    Text = "What is 2+2?",
                    Options = new List<Option>
                    {
                        new Option { Id = 1, Text = "3" },
                        new Option { Id = 2, Text = "4" },
                        new Option { Id = 3, Text = "5" },
                        new Option { Id = 4, Text = "6" }
                    },
                    CorrectOptionId = 2
                }
            };
        }

        [HttpGet("generate-shuffled-questions")]
        public ActionResult<List<ShuffledQuestionDto>> GenerateShuffledQuestions()
        {
            // Shuffle the order of questions
            var shuffledQuestions = _questions
                .OrderBy(q => Guid.NewGuid()) // Shuffle questions
                .Select(q => new ShuffledQuestionDto
                {
                    QuestionText = q.Text,
                    ShuffledOptions = q.Options
                        .OrderBy(o => Guid.NewGuid()) // Shuffle options within each question
                        .Select(o => o.Text)
                        .ToList()
                })
                .ToList();

            return Ok(shuffledQuestions);
        }
    }
}
