using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Controllers
{
    public class QuizResult
    {
        public Question Question { get; set; }
        public int? UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSkipped { get; set; }
    }

    public class QuizController : Controller
    {
        private static Quiz _quiz;

        public QuizController()
        {
            if (_quiz == null)
            {
                _quiz = new Quiz();
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            _quiz.UpdateTime();
            var currentQuestion = _quiz.GetCurrentQuestion();
            if (currentQuestion == null)
            {
                return RedirectToAction("Result");
            }

            ViewBag.CurrentQuestionIndex = _quiz.CurrentQuestionIndex + 1;
            ViewBag.TotalQuestions = _quiz.Questions.Count;
            ViewBag.TimeRemaining = _quiz.TimeRemaining;

            return View(currentQuestion);
        }

        [HttpPost]
        public IActionResult Index(int? selectedAnswer, string action)
        {
            if (action == "Skip")
            {
                if (_quiz.CurrentQuestionIndex >= 0 && _quiz.CurrentQuestionIndex < _quiz.UserAnswers.Count)
                {
                    _quiz.UserAnswers[_quiz.CurrentQuestionIndex] = null; // Mark as skipped
                }
                _quiz.MoveToNextQuestion();
            }
            else if (selectedAnswer.HasValue)
            {
                if (_quiz.CurrentQuestionIndex >= 0 && _quiz.CurrentQuestionIndex < _quiz.UserAnswers.Count)
                {
                    _quiz.UserAnswers[_quiz.CurrentQuestionIndex] = selectedAnswer.Value;
                }

                if (action == "Next")
                {
                    _quiz.MoveToNextQuestion();
                }
            }

            if (_quiz.CurrentQuestionIndex >= _quiz.Questions.Count || action == "Finish")
            {
                return RedirectToAction("Result");
            }

            var updatedQuestion = _quiz.GetCurrentQuestion();
            if (updatedQuestion == null)
            {
                return RedirectToAction("Result");
            }

            _quiz.UpdateTime();

            ViewBag.CurrentQuestionIndex = _quiz.CurrentQuestionIndex + 1;
            ViewBag.TotalQuestions = _quiz.Questions.Count;
            ViewBag.TimeRemaining = _quiz.TimeRemaining;

            return View(updatedQuestion);
        }

        public IActionResult Result()
        {
            var results = new List<QuizResult>();
            var score = 0;

            for (int i = 0; i < _quiz.Questions.Count; i++)
            {
                // Переконатися, що індекс існує у UserAnswers
                var userAnswer = i < _quiz.UserAnswers.Count ? _quiz.UserAnswers[i] : (int?)null;
                var isCorrect = userAnswer.HasValue && userAnswer.Value == _quiz.Questions[i].CorrectAnswerIndex;
                var isSkipped = !userAnswer.HasValue;

                if (isCorrect)
                {
                    score++;
                }
                results.Add(new QuizResult
                {
                    Question = _quiz.Questions[i],
                    UserAnswer = userAnswer,
                    IsCorrect = isCorrect,
                    IsSkipped = isSkipped
                });
            }

            ViewBag.Results = results;
            ViewBag.Score = score;
            ViewBag.TotalQuestions = _quiz.Questions.Count;

            return View(results);
        }

        [HttpPost]
        public IActionResult Restart()
        {
            _quiz = new Quiz();
            return RedirectToAction("Index");
        }
    }

    public class Quiz
    {
        public List<Question> Questions { get; set; }
        public List<int?> UserAnswers { get; set; } // Список для зберігання відповідей користувачів
        public int CurrentQuestionIndex { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        private DateTime _startTime { get; set; }

        public Quiz()
        {
            InitializeQuestions();
        }

        public void InitializeQuestions()
        {
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Which fictional city is the home of Batman?",
                    Options = new List<string> { "Metropolis", "Gotham", "Star City", "Central City" },
                    CorrectAnswerIndex = 1
                },
                 new Question
                {
                    Text = "In which year did the first man land on the moon?",
                    Options = new List<string> { "1967", "1968", "1969", "1970" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which mythological creature is said to have a horse's body and a fish's tail?",
                    Options = new List<string> { "Griffin", "Centaur", "Hippocampus", "Phoenix" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "What is the capital of the fictional country of Wakanda?",
                    Options = new List<string> { "Shuri", "T'Challa", "Birnin Zana", "Zamunda" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which tech entrepreneur is known for founding SpaceX?",
                    Options = new List<string> { "Steve Jobs", "Elon Musk", "Jeff Bezos", "Mark Zuckerberg" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the rarest blood type in the world?",
                    Options = new List<string> { "O", "A", "B", "AB-Negative" },
                    CorrectAnswerIndex = 3
                },
                new Question
                {
                    Text = "Which country gifted the Statue of Liberty to the USA?",
                    Options = new List<string> { "United Kingdom", "Canada", "France", "Germany" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "In which movie did Leonardo DiCaprio famously say, 'I'm the king of the world!'?",
                    Options = new List<string> { "Inception", "Titanic", "The Revenant", "Catch Me If You Can" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the hardest substance in the human body?",
                    Options = new List<string> { "Bone", "Enamel", "Cartilage", "Tendon" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which planet in our solar system is the largest?",
                    Options = new List<string> { "Earth", "Jupiter", "Saturn", "Neptune" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which is the only mammal capable of true flight?",
                    Options = new List<string> { "Squirrel", "Bat", "Penguin", "Ostrich" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which word is used to describe an octopus escaping from a jar in just 60 seconds?",
                    Options = new List<string> { "Escapology", "Cephalopod", "Tentacology", "Houdini" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "What is the most spoken language in the world?",
                    Options = new List<string> { "Spanish", "English", "Mandarin", "Hindi" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which element is essential for the formation of steel?",
                    Options = new List<string> { "Carbon", "Oxygen", "Nitrogen", "Hydrogen" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Which artist is famous for his painting 'The Starry Night'?",
                    Options = new List<string> { "Claude Monet", "Vincent van Gogh", "Pablo Picasso", "Salvador Dalí" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which is the smallest planet in our solar system?",
                    Options = new List<string> { "Mars", "Venus", "Mercury", "Pluto" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which fruit is known as the 'king of fruits' in Southeast Asia?",
                    Options = new List<string> { "Mango", "Durian", "Banana", "Pineapple" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which famous landmark has the nickname 'The Big Ben'?",
                    Options = new List<string> { "Eiffel Tower", "Statue of Liberty", "Great Wall", "Elizabeth Tower" },
                    CorrectAnswerIndex = 3
                },
                new Question
                {
                    Text = "In which city would you find the ancient Acropolis?",
                    Options = new List<string> { "Rome", "Athens", "Cairo", "Istanbul" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the main ingredient in traditional Japanese sushi?",
                    Options = new List<string> { "Noodles", "Fish", "Rice", "Seaweed" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which continent is known for having the most species of reptiles?",
                    Options = new List<string> { "Asia", "Africa", "Australia", "South America" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which novel begins with the line 'Call me Ishmael'?",
                    Options = new List<string> { "1984", "Moby-Dick", "The Catcher in the Rye", "To Kill a Mockingbird" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which bird is known for its ability to mimic human speech?",
                    Options = new List<string> { "Eagle", "Parrot", "Owl", "Raven" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the capital city of New Zealand?",
                    Options = new List<string> { "Auckland", "Christchurch", "Wellington", "Dunedin" },
                    CorrectAnswerIndex = 2
                },
                // Додайте інші питання тут
            };

            UserAnswers = new List<int?>(new int?[Questions.Count]);
            CurrentQuestionIndex = 0;
            TimeRemaining = TimeSpan.FromMinutes(10);
            _startTime = DateTime.Now;
        }

        public Question GetCurrentQuestion()
        {
            if (CurrentQuestionIndex >= 0 && CurrentQuestionIndex < Questions.Count)
            {
                return Questions[CurrentQuestionIndex];
            }
            return null;
        }

        public void MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
            _startTime = DateTime.Now;
            if (CurrentQuestionIndex >= Questions.Count)
            {
                CurrentQuestionIndex = Questions.Count - 1;
            }
        }

        public void UpdateTime()
        {
            var elapsed = (DateTime.Now - _startTime).TotalSeconds;
            TimeRemaining = TimeSpan.FromMinutes(10) - TimeSpan.FromSeconds(elapsed);
            if (TimeRemaining <= TimeSpan.Zero)
            {
                MoveToNextQuestion();
            }
        }
    }
}
