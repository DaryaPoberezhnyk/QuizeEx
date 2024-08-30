using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.Collections.Generic;

namespace QuizApp.Controllers
{
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
            var currentQuestion = _quiz.GetCurrentQuestion();
            if (currentQuestion == null)
            {
                return RedirectToAction("Result");
            }

            ViewBag.CurrentQuestionIndex = _quiz.CurrentQuestionIndex + 1; // +1 for 1-based index
            ViewBag.TotalQuestions = _quiz.Questions.Count;

            return View(currentQuestion);
        }

        [HttpPost]
        public IActionResult Index(int selectedAnswer, string action)
        {
            if (_quiz.CurrentQuestionIndex >= 0 && _quiz.CurrentQuestionIndex < _quiz.Questions.Count)
            {
                _quiz.UserAnswers[_quiz.CurrentQuestionIndex] = selectedAnswer;
            }

            if (action == "Next")
            {
                _quiz.CurrentQuestionIndex++;
            }

            if (_quiz.CurrentQuestionIndex >= _quiz.Questions.Count || action == "Finish")
            {
                return RedirectToAction("Result");
            }

            var currentQuestion = _quiz.GetCurrentQuestion();
            if (currentQuestion == null)
            {
                return RedirectToAction("Result");
            }

            ViewBag.CurrentQuestionIndex = _quiz.CurrentQuestionIndex + 1;
            ViewBag.TotalQuestions = _quiz.Questions.Count;

            return View(currentQuestion);
        }
        public IActionResult Result()
        {
            var results = new List<(Question Question, int UserAnswer, bool IsCorrect)>();
            var score = 0;

            for (int i = 0; i < _quiz.Questions.Count; i++)
            {
                var isCorrect = _quiz.UserAnswers[i] == _quiz.Questions[i].CorrectAnswerIndex;
                if (isCorrect)
                {
                    score++;
                }
                results.Add((_quiz.Questions[i], _quiz.UserAnswers[i], isCorrect));
            }

            ViewBag.Results = results;
            ViewBag.Score = score;
            ViewBag.TotalQuestions = _quiz.Questions.Count;

            return View();
        }


        [HttpPost]
        public IActionResult Restart()
        {
            _quiz = new Quiz(); 
            return RedirectToAction("Index");
        }
    }
}
