using System.Collections.Generic;

namespace QuizApp.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }

    public class Quiz
    {
        public List<Question> Questions { get; set; }
        public List<int> UserAnswers { get; set; }
        public int CurrentQuestionIndex { get; set; }

        public Quiz()
        {
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Which planet is known as the Red Planet?",
                    Options = new List<string> { "Earth", "Mars", "Jupiter", "Saturn" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the capital of France?",
                    Options = new List<string> { "Rome", "Madrid", "Paris", "Berlin" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "What is the largest ocean on Earth?",
                    Options = new List<string> { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean", "Pacific Ocean" },
                    CorrectAnswerIndex = 3
                },
                new Question
                {
                    Text = "Who wrote 'To Kill a Mockingbird'?",
                    Options = new List<string> { "Harper Lee", "J.K. Rowling", "Ernest Hemingway", "Mark Twain" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Which element has the chemical symbol 'O'?",
                    Options = new List<string> { "Oxygen", "Gold", "Osmium", "Opium" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "What is the speed of light in a vacuum?",
                    Options = new List<string> { "300,000 km/s", "150,000 km/s", "100,000 km/s", "50,000 km/s" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Who painted the Mona Lisa?",
                    Options = new List<string> { "Leonardo da Vinci", "Vincent van Gogh", "Pablo Picasso", "Claude Monet" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Which gas makes up most of the Earth's atmosphere?",
                    Options = new List<string> { "Oxygen", "Carbon Dioxide", "Nitrogen", "Hydrogen" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "In which year did the Titanic sink?",
                    Options = new List<string> { "1905", "1912", "1915", "1923" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the smallest prime number?",
                    Options = new List<string> { "0", "1", "2", "3" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which planet is closest to the sun?",
                    Options = new List<string> { "Venus", "Earth", "Mercury", "Mars" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "What is the main ingredient in guacamole?",
                    Options = new List<string> { "Tomato", "Pepper", "Avocado", "Onion" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which country is known as the Land of the Rising Sun?",
                    Options = new List<string> { "China", "Japan", "South Korea", "Thailand" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the chemical symbol for gold?",
                    Options = new List<string> { "Au", "Ag", "Pb", "Fe" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Who is known as the father of modern physics?",
                    Options = new List<string> { "Isaac Newton", "Albert Einstein", "Galileo Galilei", "Niels Bohr" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the hardest natural substance on Earth?",
                    Options = new List<string> { "Gold", "Diamond", "Iron", "Platinum" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the capital city of Australia?",
                    Options = new List<string> { "Sydney", "Melbourne", "Canberra", "Brisbane" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which element is represented by the symbol 'Na'?",
                    Options = new List<string> { "Neon", "Sodium", "Nickel", "Nitrogen" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the largest organ in the human body?",
                    Options = new List<string> { "Heart", "Brain", "Liver", "Skin" },
                    CorrectAnswerIndex = 3
                },
                new Question
                {
                    Text = "What is the boiling point of water?",
                    Options = new List<string> { "50°C", "75°C", "100°C", "125°C" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which famous scientist developed the theory of relativity?",
                    Options = new List<string> { "Isaac Newton", "Albert Einstein", "Charles Darwin", "Galileo Galilei" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the most abundant gas in the Earth's atmosphere?",
                    Options = new List<string> { "Oxygen", "Nitrogen", "Argon", "Carbon Dioxide" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the chemical symbol for silver?",
                    Options = new List<string> { "Au", "Ag", "Si", "Pb" },
                    CorrectAnswerIndex = 1
                },
            };
            UserAnswers = new List<int>(new int[Questions.Count]);
            CurrentQuestionIndex = 0;
        }

        public Question GetCurrentQuestion()
        {
            if (CurrentQuestionIndex < Questions.Count)
            {
                return Questions[CurrentQuestionIndex];
            }
            return null;
        }
    }
}
